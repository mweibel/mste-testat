using System;
using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;

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

        //[TestMethod]
        //public void GetReservationByIllegalNr()
        //{
        //    Assert.Inconclusive("Test wurde noch nicht implementiert!");
        //}

        //[TestMethod]
        //public void InsertAutoTest()
        //{
        //    Assert.Inconclusive("Test wurde noch nicht implementiert!");
        //}

        //[TestMethod]
        //public void InsertKundeTest()
        //{
        //    Assert.Inconclusive("Test wurde noch nicht implementiert!");
        //}

        //[TestMethod]
        //public void InsertReservationTest()
        //{
        //    Assert.Inconclusive("Test wurde noch nicht implementiert!");
        //}

        //[TestMethod]
        //public void UpdateAutoTest()
        //{
        //    Assert.Inconclusive("Test wurde noch nicht implementiert!");
        //}

        //[TestMethod]
        //public void UpdateKundeTest()
        //{
        //    Assert.Inconclusive("Test wurde noch nicht implementiert!");
        //}

        //[TestMethod]
        //public void UpdateReservationTest()
        //{
        //    Assert.Inconclusive("Test wurde noch nicht implementiert!");
        //}

        //[TestMethod]
        //public void UpdateAutoTestWithOptimisticConcurrency()
        //{
        //    Assert.Inconclusive("Test wurde noch nicht implementiert!");
        //}

        //[TestMethod]
        //public void UpdateKundeTestWithOptimisticConcurrency()
        //{
        //    Assert.Inconclusive("Test wurde noch nicht implementiert!");
        //}

        //[TestMethod]
        //public void UpdateReservationTestWithOptimisticConcurrency()
        //{
        //    Assert.Inconclusive("Test wurde noch nicht implementiert!");
        //}

        //[TestMethod]
        //public void DeleteKundeTest()
        //{
        //    Assert.Inconclusive("Test wurde noch nicht implementiert!");
        //}

        //[TestMethod]
        //public void DeleteAutoTest()
        //{
        //    Assert.Inconclusive("Test wurde noch nicht implementiert!");
        //}

        //[TestMethod]
        //public void DeleteReservationTest()
        //{
        //    Assert.Inconclusive("Test wurde noch nicht implementiert!");
        //}
    }
}
