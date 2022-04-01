﻿using inOffice.BusinessLogicLayer.Requests;
using inOffice.BusinessLogicLayer.Responses;
using inOfficeApplication.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inOffice.BusinessLogicLayer.Interface
{
    public interface IReservationService
    {
       ReservationResponse Reserve(ReservationRequest o, Employee employee);
    }
}