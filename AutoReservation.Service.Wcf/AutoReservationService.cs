using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService: IAutoReservationService
    {
        public List<DtoBase> findAll() 
        {
            return new List<DtoBase> { };
        }

        public DtoBase findOne(int id)
        {
            return null;
        }

        public DtoBase insert(DtoBase entry)
        {
            return null;
        }

        public DtoBase update(DtoBase entry)
        {
            return null;
        }

        public bool delete(DtoBase entry)
        {
            return false;
        }
    }
}