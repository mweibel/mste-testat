using System;
using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;
using AutoReservation.Common.Interfaces.Exceptions;

namespace AutoReservation.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        #region FindAll
        [TestMethod]
        public void AutosTest()
        {
            List<AutoDto> autos = Target.FindAllAutos();

            Assert.IsTrue(autos.Count == 3, "Invalid amount of autos");
        }

        [TestMethod]
        public void KundenTest()
        {
            List<KundeDto> kunden = Target.FindAllKunden();

            Assert.IsTrue(kunden.Count == 4, "Invalid amount of kunden");
        }

        [TestMethod]
        public void ReservationenTest()
        {
            List<ReservationDto> reservationen = Target.FindAllReservationen();

            Assert.IsTrue(reservationen.Count == 3, "Invalid amount of reservationen");
        }
        #endregion FindAll

        #region FindById
        [TestMethod]
        public void GetAutoByIdTest()
        {
            AutoDto auto = Target.FindAuto(1);

            Assert.IsTrue(auto.Id == 1);
            Assert.IsTrue(auto.Marke == "Fiat Punto");
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            KundeDto kunde = Target.FindKunde(1);

            Assert.IsTrue(kunde.Id == 1);
            Assert.IsTrue(kunde.Vorname == "Anna");
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            ReservationDto reservation = Target.FindReservation(1);

            Assert.IsTrue(reservation.ReservationNr == 1);
            Assert.AreEqual(reservation.Bis, new DateTime(2020, 01, 20));
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<NotFoundException>))]
        public void GetReservationByIllegalNr()
        {
            // FIXME: FaultException Expecting doesn't work
            ReservationDto reservation = Target.FindReservation(48539);
        }
        #endregion FindById

        #region Insert
        [TestMethod]
        public void InsertAutoTest()
        {
            AutoDto auto = CreateAuto();

            AutoDto savedAuto = Target.InsertAuto(auto);

            Assert.IsTrue(savedAuto.Id > 0);
            Assert.AreEqual(auto.AutoKlasse, savedAuto.AutoKlasse);
            Assert.AreEqual(auto.Basistarif, savedAuto.Basistarif);
            Assert.AreEqual(auto.Tagestarif, savedAuto.Tagestarif);
            Assert.AreEqual(auto.Marke, savedAuto.Marke);
        }

        private static AutoDto CreateAuto()
        {
            AutoDto auto = new AutoDto();
            auto.AutoKlasse = AutoKlasse.Luxusklasse;
            auto.Basistarif = 340;
            auto.Tagestarif = 435;
            auto.Marke = "Jaguar F-Type";
            return auto;
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            KundeDto kunde = CreateKunde();

            KundeDto savedKunde = Target.InsertKunde(kunde);

            Assert.IsTrue(savedKunde.Id > 0);
            Assert.AreEqual(kunde.Geburtsdatum, savedKunde.Geburtsdatum);
            Assert.AreEqual(kunde.Nachname, savedKunde.Nachname);
            Assert.AreEqual(kunde.Vorname, savedKunde.Vorname);
        }

        private static KundeDto CreateKunde()
        {
            KundeDto kunde = new KundeDto();
            kunde.Geburtsdatum = new DateTime(1960, 01, 25);
            kunde.Nachname = "Bar";
            kunde.Vorname = "Foo";
            return kunde;
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            AutoDto auto = CreateAuto();
            auto = Target.InsertAuto(auto);
            KundeDto kunde = CreateKunde();
            kunde = Target.InsertKunde(kunde);

            ReservationDto reservation = new ReservationDto();
            reservation.Auto = auto;
            reservation.Kunde = kunde;
            reservation.Bis = new DateTime(2014, 02, 28);
            reservation.Von = new DateTime(2014, 02, 15);

            ReservationDto savedReservation = Target.InsertReservation(reservation);

            Assert.IsTrue(savedReservation.ReservationNr > 0);
            // FIXME: Kunde / Auto are somehow not set
            Assert.AreEqual(reservation.Kunde, savedReservation.Kunde);
            Assert.AreEqual(reservation.Auto, savedReservation.Auto);
            Assert.AreEqual(reservation.Bis, savedReservation.Bis);
            Assert.AreEqual(reservation.Von, savedReservation.Von);
        }
        #endregion Insert

        #region Update
        [TestMethod]
        public void UpdateAutoTest()
        {
            AutoDto auto = CreateAuto();
            AutoDto original = Target.InsertAuto(auto);

            AutoDto modified = (AutoDto)original.Clone();
            modified.Marke = "Updated Auto";

            AutoDto savedModified = Target.UpdateAuto(original, modified);

            Assert.AreEqual(savedModified.Marke, "Updated Auto");
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            KundeDto kunde = CreateKunde();
            KundeDto original = Target.InsertKunde(kunde);

            KundeDto modified = (KundeDto)original.Clone();
            modified.Nachname = "Updated Kunde";

            KundeDto savedModified = Target.UpdateKunde(original, modified);

            Assert.AreEqual(savedModified.Nachname, "Updated Kunde");
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            ReservationDto original = Target.FindReservation(1);

            ReservationDto modified = (ReservationDto)original.Clone();
            
            DateTime newBis = new DateTime(2015, 12, 12);
            modified.Bis = newBis;

            ReservationDto savedModified = Target.UpdateReservation(original, modified);

            Assert.AreEqual(savedModified.Bis, newBis);
        }
        #endregion Update

        #region UpdateWithConcurrency
        [TestMethod]
        [ExpectedException(typeof(FaultException<ConcurrencyException>))]
        public void UpdateAutoTestWithOptimisticConcurrency()
        {
            AutoDto auto = CreateAuto();
            AutoDto original = Target.InsertAuto(auto);

            AutoDto original2 = (AutoDto)original.Clone();
            original2.Marke = "Original2 Auto";

            AutoDto modified = (AutoDto)original.Clone();
            modified.Marke = "Updated Auto";

            AutoDto savedModified = Target.UpdateAuto(original2, modified);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ConcurrencyException>))]
        public void UpdateKundeTestWithOptimisticConcurrency()
        {
            KundeDto kunde = CreateKunde();
            KundeDto original = Target.InsertKunde(kunde);

            KundeDto original2 = (KundeDto)original.Clone();
            original2.Nachname = "Original2 Kunde";

            KundeDto modified = (KundeDto)original.Clone();
            modified.Nachname = "Updated Kunde";

            KundeDto savedModified = Target.UpdateKunde(original2, modified);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ConcurrencyException>))]
        public void UpdateReservationTestWithOptimisticConcurrency()
        {
            ReservationDto original = Target.FindReservation(1);

            ReservationDto original2 = (ReservationDto)original.Clone();
            original2.Bis = new DateTime(2015, 03, 15);

            ReservationDto modified = (ReservationDto)original.Clone();
            modified.Bis = new DateTime(2015, 04, 15);

            ReservationDto savedModified = Target.UpdateReservation(original2, modified);
        }
        #endregion UpdateWithConcurrency

        #region Delete
        [TestMethod]
        [ExpectedException(typeof(FaultException<NotFoundException>))]
        public void DeleteKundeTest()
        {
            KundeDto deletedKunde = Target.DeleteKunde(Target.FindKunde(1));
            Target.FindKunde(deletedKunde.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<NotFoundException>))]
        public void DeleteAutoTest()
        {
            AutoDto deletedAuto = Target.DeleteAuto(Target.FindAuto(1));
            Target.FindAuto(deletedAuto.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<NotFoundException>))]
        public void DeleteReservationTest()
        {
            ReservationDto deletedReservation = Target.DeleteReservation(Target.FindReservation(1));
            Target.FindReservation(deletedReservation.ReservationNr);
        }
        #endregion Delete
    }
}
