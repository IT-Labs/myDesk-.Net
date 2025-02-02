﻿using AutoMapper;
using MyDesk.Data.Interfaces.BusinessLogic;
using MyDesk.Data.Interfaces.Repository;
using MyDesk.Data.DTO;
using MyDesk.Data.Entities;
using MyDesk.Data.Exceptions;
using System.Transactions;

namespace MyDesk.BusinessLogicLayer
{
    public class DeskService : IDeskService
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IDeskRepository _deskRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;

        public DeskService(IOfficeRepository officeRepository,
            IDeskRepository deskRepository,
            ICategoriesRepository categoriesRepository,
            IMapper mapper)
        {
            _officeRepository = officeRepository;
            _deskRepository = deskRepository;
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }

        public List<DeskDto> GetOfficeDesks(int id, int? take = null, int? skip = null)
        {
            List<Desk> desks = _deskRepository.GetOfficeDesks(id, includeCategory: true, includeReservations: true, includeEmployees: true, take: take, skip: skip);
            return _mapper.Map<List<DeskDto>>(desks);
        }

        public void Create(int officeId, int numberOfInstancesToCreate)
        {
            Office existinOffice = _officeRepository.Get(officeId);
            if (existinOffice == null)
            {
                throw new NotFoundException($"Office with ID: {officeId} not found.");
            }

            int highestIndex = _deskRepository.GetHighestDeskIndexForOffice(officeId);

            List<Desk> desksToInsert = new List<Desk>();

            for (int i = 0; i < numberOfInstancesToCreate; i++)
            {
                Desk desk = new Desk()
                {
                    OfficeId = officeId,
                    IsDeleted = false,
                    Categories = "regular",
                    IndexForOffice = highestIndex + i + 1
                };

                desksToInsert.Add(desk);
            }

            _deskRepository.BulkInsert(desksToInsert);
        }

        public void Update(List<DeskDto> desks)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (DeskDto deskToUpdate in desks)
                {
                    Desk desk = _deskRepository.Get(deskToUpdate.Id.Value);

                    if (desk == null)
                    {
                        continue;
                    }

                    Category existingDeskCategory = _categoriesRepository.Get(deskToUpdate.Category.DoubleMonitor, deskToUpdate.Category.NearWindow,
                        deskToUpdate.Category.SingleMonitor, deskToUpdate.Category.Unavailable);
                    if (existingDeskCategory != null)
                    {
                        desk.CategorieId = existingDeskCategory.Id;
                        _deskRepository.Update(desk);
                    }
                    else
                    {
                        Category category = new Category()
                        {
                            DoubleMonitor = deskToUpdate.Category.DoubleMonitor,
                            SingleMonitor = deskToUpdate.Category.SingleMonitor,
                            NearWindow = deskToUpdate.Category.NearWindow,
                            Unavailable = deskToUpdate.Category.Unavailable
                        };

                        _categoriesRepository.Insert(category);

                        desk.CategorieId = category.Id;
                        _deskRepository.Update(desk);
                    }
                }

                transaction.Complete();
            }
        }

        public void Delete(int id)
        {
            Desk desk = _deskRepository.Get(id, includeReservations: true, includeReviews: true);

            if (desk == null)
            {
                throw new NotFoundException($"Desk with ID: {id} not found.");
            }

            foreach (Reservation reservation in desk.Reservations)
            {
                reservation.IsDeleted = true;
                foreach (Review review in reservation.Reviews)
                {
                    review.IsDeleted = true;
                }
            }

            _deskRepository.SoftDelete(desk);
        }
    }
}
