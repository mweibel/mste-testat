using System;
using AutoReservation.BusinessLayer;
using AutoReservation.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.Testing
{
	[TestClass]
	public class BusinessLayerTest
	{
		private AutoReservationEntities context;

		private AutoReservationBusinessComponent target;

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

			Auto insertedAuto = Target.InsertAuto(auto);

			Assert.IsTrue(auto.Id > 0, "ID is not auto incremented. Probably insert didn't happen.");
		}

		[TestMethod]
		public void UpdateAutoTest()
		{
			// FIXME: Better way to do this?
			Auto original = Target.FindAuto(1);
			Auto modified = Target.FindAuto(1);

			modified.Tagestarif = 900;

			Target.UpdateAuto(original, modified);
			Assert.IsTrue(modified.Tagestarif == 900, "Update failed");
		}

		[TestMethod]
		public void UpdateKundeTest()
		{
			Kunde original = Target.FindKunde(1);
			Kunde modified = Target.FindKunde(1);

			modified.Vorname = "Foobar";

			Target.UpdateKunde(original, modified);
			Assert.IsTrue(modified.Vorname == "Foobar", "Update failed");
		}

		[TestMethod]
		public void UpdateReservationTest()
		{
			Reservation original = Target.FindReservation(1);
			Reservation modified = Target.FindReservation(1);

			var bis = new DateTime(2014, 12, 30);

			modified.Bis = bis;

			Target.UpdateReservation(original, modified);
			Assert.IsTrue(modified.Bis == bis, "Update failed");
		}
	}
}