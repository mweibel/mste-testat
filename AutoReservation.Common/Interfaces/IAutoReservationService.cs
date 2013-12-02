using AutoReservation.Common.DataTransferObjects;
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
        AutoDto FindAuto(int id);

        [OperationContract]
        AutoDto InsertAuto(AutoDto entry);

        [OperationContract]
        AutoDto UpdateAuto(AutoDto original, AutoDto modified);

        [OperationContract]
        AutoDto DeleteAuto(AutoDto entry);

        IEnumerable<AutoDto> Autos { get; }
        #endregion Autos

        #region Kunden
        [OperationContract]
        List<KundeDto> FindAllKunden();

        [OperationContract]
        KundeDto FindKunde(int id);

        [OperationContract]
        KundeDto InsertKunde(KundeDto entry);

        [OperationContract]
        KundeDto UpdateKunde(KundeDto original, KundeDto modified);

        [OperationContract]
        KundeDto DeleteKunde(KundeDto entry);

        IEnumerable<KundeDto> Kunden { get; }
        #endregion Kunden

        #region Reservationen
        [OperationContract]
        List<ReservationDto> FindAllReservationen();

        [OperationContract]
        ReservationDto FindReservation(int id);

        [OperationContract]
        ReservationDto InsertReservation(ReservationDto entry);

        [OperationContract]
        ReservationDto UpdateReservation(ReservationDto original, ReservationDto modified);

        [OperationContract]
        ReservationDto DeleteReservation(ReservationDto entry);

        IEnumerable<ReservationDto> Reservationen { get; }
        #endregion Reservationen
    }
}
