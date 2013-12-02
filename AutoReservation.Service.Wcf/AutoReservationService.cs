using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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

        public AutoDto InsertAuto(AutoDto entry)
        {
            return bc.InsertAuto(entry.ConvertToEntity()).ConvertToDto();
        }

        public AutoDto UpdateAuto(AutoDto original, AutoDto modified)
        {
            return bc.UpdateAuto(original.ConvertToEntity(), modified.ConvertToEntity()).ConvertToDto();
        }

        public AutoDto DeleteAuto(AutoDto entry)
        {
            return bc.DeleteAuto(entry.ConvertToEntity()).ConvertToDto();
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

        public KundeDto InsertKunde(KundeDto entry)
        {
            return bc.InsertKunde(entry.ConvertToEntity()).ConvertToDto();
        }

        public KundeDto UpdateKunde(KundeDto original, KundeDto modified)
        {
            return bc.UpdateKunde(original.ConvertToEntity(), modified.ConvertToEntity()).ConvertToDto();
        }

        public KundeDto DeleteKunde(KundeDto entry)
        {
            return bc.DeleteKunde(entry.ConvertToEntity()).ConvertToDto();
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
            return bc.FindReservation(id).ConvertToDto();
        }

        public ReservationDto InsertReservation(ReservationDto entry)
        {
            return bc.InsertReservation(entry.ConvertToEntity()).ConvertToDto();
        }

        public ReservationDto UpdateReservation(ReservationDto original, ReservationDto modified)
        {
            return bc.UpdateReservation(original.ConvertToEntity(), modified.ConvertToEntity()).ConvertToDto();
        }

        public ReservationDto DeleteReservation(ReservationDto entry)
        {
            return bc.DeleteReservation(entry.ConvertToEntity()).ConvertToDto();
        }
        #endregion Reservationen
    }
}