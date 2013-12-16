using System.Threading;
using System.Windows.Input;
using AutoReservation.Common.Interfaces;
using AutoReservation.Testing;
using AutoReservation.Ui.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.Ui.Testing
{
	[TestClass]
	public abstract class ViewModelTestBase
	{
		protected abstract IAutoReservationService Target { get; }

		[TestInitialize]
		public void InitializeTestData()
		{
			TestEnvironmentHelper.InitializeTestData();
			SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
			IAutoReservationService target = Target;
		}

		[TestMethod]
		public void AutosLoadTest()
		{
			var autoViewModel = new AutoViewModel();
			ICommand cmd = autoViewModel.LoadCommand;
			Assert.IsTrue(cmd.CanExecute(this));
			cmd.Execute(this);
			Assert.IsNotNull(autoViewModel.Autos);
		}

		[TestMethod]
		public void KundenLoadTest()
		{
			var kundeViewModel = new KundeViewModel();
			ICommand cmd = kundeViewModel.LoadCommand;
			Assert.IsTrue(cmd.CanExecute(this));
			cmd.Execute(this);
			Assert.IsNotNull(kundeViewModel.Kunden);
		}

		[TestMethod]
		public void ReservationenLoadTest()
		{
			var reservationViewModel = new ReservationViewModel();
			ICommand cmd = reservationViewModel.LoadCommand;
			Assert.IsTrue(cmd.CanExecute(this));
			cmd.Execute(this);
			Assert.IsNotNull(reservationViewModel.Reservationen);
		}
	}
}