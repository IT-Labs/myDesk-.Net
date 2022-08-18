﻿using inOffice.BusinessLogicLayer.Responses;
using inOfficeApplication.Data.Models;

namespace inOffice.BusinessLogicLayer.Interface
{
    public interface IEmployeeService
    {
        Employee GetByEmail(string email);
        Employee GetByEmailAndPassword(string email, string password);
        Employee GetById(int id);
        List<CustomEmployee> GetAll();
        void Create(Employee employee);
    }
}
