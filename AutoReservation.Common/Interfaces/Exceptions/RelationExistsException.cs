using System;
using System.Runtime.Serialization;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces.Exceptions
{
	[DataContract]
	public class RelationExistsException
	{
		public RelationExistsException(String message)
		{
			this.Message = message;
		}

		[DataMember]
		public String Message { get; set; }

		public override string ToString()
		{
			return Message;
		}
	}
}
