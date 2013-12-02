using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Common.Interfaces.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService: IAutoReservationService
    {
        AutoReservationBusinessComponent bc = new AutoReservationBusinessComponent();

        #region Autos
        public List<AutoDto> FindAllAutos() 
        {
            return bc.FindAllAutos().ConvertToDtos();   
        }

        public IEnumerable<AutoDto> Autos {
            get
            {
                return FindAllAutos();
            }
        }

        public AutoDto FindAuto(int id)
        {
            return bc.FindAuto(id).ConvertToDto();
        }

        public AutoDto InsertAuto(AutoDto auto)
        {
            return bc.InsertAuto(auto.ConvertToEntity()).ConvertToDto();
        }

        public AutoDto UpdateAuto(AutoDto original, AutoDto modified)
        {
            return bc.UpdateAuto(original.ConvertToEntity(), modified.ConvertToEntity()).ConvertToDto();
        }

        public AutoDto DeleteAuto(AutoDto auto)
        {
            return bc.DeleteAuto(auto.ConvertToEntity()).ConvertToDto();
        }
        #endregion Autos

        #region Kunden
        public List<KundeDto> FindAllKunden()
        {
            return bc.FindAllKunden().ConvertToDtos();
        }

        public IEnumerable<KundeDto> Kunden
        {
            get
            {
                return FindAllKunden();
            }
        }

        public KundeDto FindKunde(int id)
        {
            return bc.FindKunde(id).ConvertToDto();
        }

        public KundeDto InsertKunde(KundeDto kunde)
        {
            return bc.InsertKunde(kunde.ConvertToEntity()).ConvertToDto();
        }

        public KundeDto UpdateKunde(KundeDto original, KundeDto modified)
        {
            return bc.UpdateKunde(original.ConvertToEntity(), modified.ConvertToEntity()).ConvertToDto();
        }

        public KundeDto DeleteKunde(KundeDto kunde)
        {
            return bc.DeleteKunde(kunde.ConvertToEntity()).ConvertToDto();
        }
        #endregion Kunden

        #region Reservationen
        public List<ReservationDto> FindAllReservationen()
        {
            return bc.FindAllReservationen().ConvertToDtos();
        }

        public IEnumerable<ReservationDto> Reservationen
        {
            get
            {
                return FindAllReservationen();
            }
        }

        public ReservationDto FindReservation(int id)
        {
            try
            {
                return bc.FindReservation(id).ConvertToDto();
            }
            catch (InvalidOperationException)
            {
                NotFoundException exc = new NotFoundException("Reservation", id);
                throw new FaultException<NotFoundException>(exc, new FaultReason(exc.Message));
            }
        }

        public ReservationDto InsertReservation(ReservationDto reservation)
        {
            return bc.InsertReservation(reservation.ConvertToEntity()).ConvertToDto();
        }

        public ReservationDto UpdateReservation(ReservationDto original, ReservationDto modified)
        {
            return bc.UpdateReservation(original.ConvertToEntity(), modified.ConvertToEntity()).ConvertToDto();
        }

        public ReservationDto DeleteReservation(ReservationDto reservation)
        {
            return bc.DeleteReservation(reservation.ConvertToEntity()).ConvertToDto();
        }
        #endregion Reservationen
    }
}