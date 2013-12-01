using System;
using AutoReservation.BusinessLayer;
using AutoReservation.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Testing
{
    [TestClass]
    public class BusinessLayerTest
    {
        private AutoReservationEntities context;
        private AutoReservationEntities Context
        {
            get
            {
                if (context == null)
                {
                    context = new AutoReservationEntities();
                }
                return context;
            }
        }

        private AutoReservationBusinessComponent target;
        private AutoReservationBusinessComponent Target
        {
            get
            {
                if (target == null)
                {
                    target = new AutoReservationBusinessComponent();
                }
                return target;
            }
        }


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void InsertAutoTest()
        {
            Auto auto = new StandardAuto();
            auto.Marke = "BMW";
            auto.Tagestarif = 400;

            Auto insertedAuto = target.insertAuto(context, auto);

            Assert.IsNotNull(insertedAuto.Id);
        }

        [TestMethod]
        public void UpdateAutoTest()
        {
            Assert.Inconclusive("Test wurde noch nicht implementiert!");
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            Assert.Inconclusive("Test wurde noch nicht implementiert!");
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            Assert.Inconclusive("Test wurde noch nicht implementiert!");
        }

    }
}
