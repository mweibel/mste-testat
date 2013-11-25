using AutoReservation.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Service.Wcf
{
    interface IAutoReservationService
    {
        public List<DtoBase> findAll();

        public DtoBase findOne(int id);

        public DtoBase insert(DtoBase entry);

        public DtoBase update(DtoBase entry);

        public bool delete(DtoBase entry);
    }
}
