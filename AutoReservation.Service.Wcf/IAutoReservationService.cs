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
        List<DtoBase> findAll();

        DtoBase findOne(int id);

        DtoBase insert(DtoBase entry);

        DtoBase update(DtoBase entry);

        bool delete(DtoBase entry);
    }
}
