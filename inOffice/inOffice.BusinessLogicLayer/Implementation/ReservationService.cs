﻿using AutoMapper;
using inOffice.BusinessLogicLayer.Interface;
using inOffice.BusinessLogicLayer.Requests;
using inOffice.Repository.Interface;
using inOfficeApplication.Data.DTO;
using inOfficeApplication.Data.Entities;
using inOfficeApplication.Data.Exceptions;
using inOfficeApplication.Data.Utils;

namespace inOffice.BusinessLogicLayer.Implementation
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDeskRepository _deskRepository;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository reservationRepository,
            IEmployeeRepository employeeRepository,
            IDeskRepository deskRepository,
            IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _employeeRepository = employeeRepository;
            _deskRepository = deskRepository;
            _mapper = mapper;
        }

        public void CancelReservation(int id)
        {
            Reservation reservationToDelete = _reservationRepository.Get(id, includeReviews: true);
            if (reservationToDelete == null)
            {
                throw new NotFoundException($"Reservation with ID: {id} not found.");
            }

            foreach (Review review in reservationToDelete.Reviews)
            {
                review.IsDeleted = true;
            }

            _reservationRepository.SoftDelete(reservationToDelete);
        }

        public List<ReservationDto> EmployeeReservations(string employeeEmail)
        {
            Employee employee = _employeeRepository.GetByEmail(employeeEmail);
            if (employee == null)
            {
                throw new NotFoundException($"Employee with email: {employeeEmail} not found.");
            }

            List<Reservation> futureReservations = new List<Reservation>();

            List<Reservation> employeeReservations = _reservationRepository.GetEmployeeReservations(employee.Id, includeDesk: true, includeConferenceRoom: true, includeOffice: true);
            foreach (Reservation employeeReservation in employeeReservations)
            {
                if (DateTime.Compare(employeeReservation.StartDate, DateTime.Now) > 0)
                {
                    futureReservations.Add(employeeReservation);
                }
            }

            return _mapper.Map<List<ReservationDto>>(futureReservations);
        }

        public List<ReservationDto> PastReservations(string employeeEmail)
        {
            Employee employee = _employeeRepository.GetByEmail(employeeEmail);
            if (employee == null)
            {
                throw new NotFoundException($"Employee with email: {employeeEmail} not found.");
            }

            List<Reservation> pastReservations = new List<Reservation>();

            List<Reservation> employeeReservations = _reservationRepository.GetEmployeeReservations(employee.Id, includeDesk: true, includeConferenceRoom: true, includeOffice: true, includeReviews: true);

            foreach (Reservation employeeReservation in employeeReservations)
            {
                if (DateTime.Compare(employeeReservation.StartDate, DateTime.Now) < 0 && DateTime.Compare(employeeReservation.EndDate, DateTime.Now) < 0)
                {
                    pastReservations.Add(employeeReservation);
                }
            }

            return _mapper.Map<List<ReservationDto>>(pastReservations);
        }

        public PaginationDto<ReservationDto> AllReservations(int? take = null, int? skip = null)
        {
            Tuple<int?, List<Reservation>> result = _reservationRepository.GetAll(includeEmployee: true, includeDesk: true, includeOffice: true, take: take, skip: skip);

            return new PaginationDto<ReservationDto>()
            {
                Values = _mapper.Map<List<ReservationDto>>(result.Item2),
                TotalCount = result.Item1.HasValue ? result.Item1.Value : result.Item2.Count()
            };
        }

        public void CoworkerReserve(ReservationRequest request)
        {
            Desk desk = _deskRepository.Get(request.DeskId);
            if (desk == null)
            {
                throw new NotFoundException($"Desk with ID: {request.DeskId} not found.");
            }

            Employee employee = _employeeRepository.GetByEmail(request.EmployeeEmail);
            if (employee == null)
            {
                throw new NotFoundException($"Employee with email: {request.EmployeeEmail} not found.");
            }

            List<Reservation> deskReservations = _reservationRepository.GetDeskReservations(request.DeskId);

            Reservation newReservation = new Reservation()
            {
                StartDate = DateTime.ParseExact(request.StartDate, "dd-MM-yyyy", null),
                EndDate = DateTime.ParseExact(request.EndDate, "dd-MM-yyyy", null),
                EmployeeId = employee.Id,
                DeskId = request.DeskId
            };

            // Check if there are existing reservations for that desk in that time-frame
            foreach (Reservation reservation in deskReservations)
            {
                if (reservation.StartDate.IsInRange(newReservation.StartDate, newReservation.EndDate) || reservation.EndDate.IsInRange(newReservation.StartDate, newReservation.EndDate) ||
                    newReservation.StartDate.IsInRange(reservation.StartDate, reservation.EndDate) || newReservation.EndDate.IsInRange(reservation.StartDate, reservation.EndDate))
                {
                    throw new ConflictException($"Reservation for that time period already exists for desk {desk.Id}");
                }
            }

            // Check if there are existing reservations for that employee in that time-frame for current office
            List<Reservation> employeeReservations = _reservationRepository.GetEmployeeReservations(employee.Id, includeDesk: true, includeConferenceRoom: true);
            foreach (Reservation reservation in employeeReservations)
            {
                if (desk.OfficeId == GetOfficeId(reservation) && (reservation.StartDate.IsInRange(newReservation.StartDate, newReservation.EndDate) || reservation.EndDate.IsInRange(newReservation.StartDate, newReservation.EndDate) ||
                    newReservation.StartDate.IsInRange(reservation.StartDate, reservation.EndDate) || newReservation.EndDate.IsInRange(reservation.StartDate, reservation.EndDate)))
                {
                    throw new ConflictException($"Reservation for that time period already exists for {employee.Email}");
                }
            }

            _reservationRepository.Insert(newReservation);
        }

        private int GetOfficeId(Reservation reservation)
        {
            if (reservation.Desk != null)
            {
                return reservation.Desk.OfficeId;
            }
            else
            {
                return reservation.ConferenceRoom.OfficeId;
            }
        }
    }
}
