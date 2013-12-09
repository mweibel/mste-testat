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
        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            var target = Target;
        }

        protected abstract IAutoReservationService Target
        {
            get;
        }

        [TestMethod]
        public void AutosLoadTest()
        {
            var autoViewModel = new AutoViewModel();
            var cmd = autoViewModel.LoadCommand;
            Assert.IsTrue(cmd.CanExecute(this));
            cmd.Execute(this);
            Assert.IsNotNull(autoViewModel.Autos);
        }

        [TestMethod]
        public void KundenLoadTest()
        {
            var kundeViewModel = new KundeViewModel();
            var cmd = kundeViewModel.LoadCommand;
            Assert.IsTrue(cmd.CanExecute(this));
            cmd.Execute(this);
            Assert.IsNotNull(kundeViewModel.Kunden);
        }

        [TestMethod]
        public void ReservationenLoadTest()
        {
            var reservationViewModel = new ReservationViewModel();
            var cmd = reservationViewModel.LoadCommand;
            Assert.IsTrue(cmd.CanExecute(this));
            cmd.Execute(this);
            Assert.IsNotNull(reservationViewModel.Reservationen);
        }
    }
}