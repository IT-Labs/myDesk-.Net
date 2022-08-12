﻿using inOffice.Repository.Interface;
using inOfficeApplication.Data;
using inOfficeApplication.Data.Models;

namespace inOffice.Repository.Implementation
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly ApplicationDbContext _context;

        public OfficeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Office Get(int id)
        {
            return _context.Offices.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        }

        public List<Office> GetAll()
        {
            return _context.Offices.Where(x => !x.IsDeleted).ToList();
        }

        public Office GetByName(string name)
        {
            return _context.Offices.FirstOrDefault(x => x.Name == name && !x.IsDeleted);
        }

        public void Insert(Office office)
        {
            _context.Offices.Add(office);
            _context.SaveChanges();
        }

        public void Update(Office office)
        {
            _context.Offices.Update(office);
            _context.SaveChanges();
        }

        public void SoftDelete(Office office)
        {
            office.IsDeleted = true;
            _context.Offices.Update(office);
            _context.SaveChanges();
        }
    }
}