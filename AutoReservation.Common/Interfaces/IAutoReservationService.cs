using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        #region Autos
        [OperationContract]
        List<AutoDto> FindAllAutos();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AutoDto FindAuto(int id);

        [OperationContract]
        AutoDto InsertAuto(AutoDto auto);

        [OperationContract]
        [FaultContract(typeof(ConcurrencyException))]
        AutoDto UpdateAuto(AutoDto original, AutoDto modified);

        [OperationContract]
        AutoDto DeleteAuto(AutoDto auto);

        IEnumerable<AutoDto> Autos
        {
            [OperationContract]
            get;
        }
        #endregion Autos

        #region Kunden
        [OperationContract]
        List<KundeDto> FindAllKunden();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        KundeDto FindKunde(int id);

        [OperationContract]
        KundeDto InsertKunde(KundeDto kunde);

        [OperationContract]
        [FaultContract(typeof(ConcurrencyException))]
        KundeDto UpdateKunde(KundeDto original, KundeDto modified);

        [OperationContract]
        KundeDto DeleteKunde(KundeDto kunde);

        IEnumerable<KundeDto> Kunden
        {
            [OperationContract]
            get;
        }
        #endregion Kunden

        #region Reservationen
        [OperationContract]
        List<ReservationDto> FindAllReservationen();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ReservationDto FindReservation(int id);

        [OperationContract]
        ReservationDto InsertReservation(ReservationDto reservation);

        [OperationContract]
        [FaultContract(typeof(ConcurrencyException))]
        ReservationDto UpdateReservation(ReservationDto original, ReservationDto modified);

        [OperationContract]
        ReservationDto DeleteReservation(ReservationDto reservation);

        IEnumerable<ReservationDto> Reservationen
        {
            [OperationContract]
            get;
        }
        #endregion Reservationen
    }
}
