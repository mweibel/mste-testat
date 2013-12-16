using System;
using System.Runtime.Serialization;

namespace AutoReservation.Common.Interfaces.Exceptions
{
	[DataContract]
	public class ConcurrencyException
	{
		public ConcurrencyException(String message)
		{
			Message = message;
		}

		[DataMember]
		public string Message { get; set; }
	}
}