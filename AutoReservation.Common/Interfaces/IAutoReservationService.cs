using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces.Exceptions;

namespace AutoReservation.Common.Interfaces
{
	[ServiceContract]
	public interface IAutoReservationService
	{
		#region Autos

		IEnumerable<AutoDto> Autos { [OperationContract] get; }

		[OperationContract]
		List<AutoDto> FindAllAutos();

		[OperationContract]
		[FaultContract(typeof (NotFoundException))]
		AutoDto FindAuto(int id);

		[OperationContract]
		AutoDto InsertAuto(AutoDto auto);

		[OperationContract]
		[FaultContract(typeof (ConcurrencyException))]
		AutoDto UpdateAuto(AutoDto original, AutoDto modified);

		[OperationContract]
		[FaultContract(typeof (RelationExistsException))]
		AutoDto DeleteAuto(AutoDto auto);

		#endregion Autos

		#region Kunden

		IEnumerable<KundeDto> Kunden { [OperationContract] get; }

		[OperationContract]
		List<KundeDto> FindAllKunden();

		[OperationContract]
		[FaultContract(typeof (NotFoundException))]
		KundeDto FindKunde(int id);

		[OperationContract]
		KundeDto InsertKunde(KundeDto kunde);

		[OperationContract]
		[FaultContract(typeof (ConcurrencyException))]
		KundeDto UpdateKunde(KundeDto original, KundeDto modified);

		[OperationContract]
		[FaultContract(typeof (RelationExistsException))]
		KundeDto DeleteKunde(KundeDto kunde);

		#endregion Kunden

		#region Reservationen

		IEnumerable<ReservationDto> Reservationen { [OperationContract] get; }

		[OperationContract]
		List<ReservationDto> FindAllReservationen();

		[OperationContract]
		[FaultContract(typeof (NotFoundException))]
		ReservationDto FindReservation(int id);

		[OperationContract]
		ReservationDto InsertReservation(ReservationDto reservation);

		[OperationContract]
		[FaultContract(typeof (ConcurrencyException))]
		ReservationDto UpdateReservation(ReservationDto original, ReservationDto modified);

		[OperationContract]
		ReservationDto DeleteReservation(ReservationDto reservation);

		#endregion Reservationen
	}
}