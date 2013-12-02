using AutoReservation.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Common.Interfaces
{
    public interface IAutoReservationService
    {
        #region Autos
        List<AutoDto> FindAllAutos();

        AutoDto FindAuto(int id);

        AutoDto InsertAuto(AutoDto entry);

        AutoDto UpdateAuto(AutoDto original, AutoDto modified);

        AutoDto DeleteAuto(AutoDto entry);

        IEnumerable<AutoDto> Autos { get; }
        #endregion Autos

        #region Kunden
        List<KundeDto> FindAllKunden();

        KundeDto FindKunde(int id);

        KundeDto InsertKunde(KundeDto entry);

        KundeDto UpdateKunde(KundeDto original, KundeDto modified);

        KundeDto DeleteKunde(KundeDto entry);

        IEnumerable<KundeDto> Kunden { get; }
        #endregion Kunden

        #region Reservationen
        List<ReservationDto> FindAllReservationen();

        ReservationDto FindReservation(int id);

        ReservationDto InsertReservation(ReservationDto entry);

        ReservationDto UpdateReservation(ReservationDto original, ReservationDto modified);

        ReservationDto DeleteReservation(ReservationDto entry);

        IEnumerable<ReservationDto> Reservationen { get; }
        #endregion Reservationen
    }
}
