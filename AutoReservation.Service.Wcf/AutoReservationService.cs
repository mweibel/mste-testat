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

        public List<AutoDto> FindAllAutos() 
        {
            return null;   
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
    }
}