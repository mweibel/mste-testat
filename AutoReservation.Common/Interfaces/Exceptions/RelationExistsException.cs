using System;
using System.Runtime.Serialization;

namespace AutoReservation.Common.Interfaces.Exceptions
{
	[DataContract]
	public class RelationExistsException
	{
		public RelationExistsException(String message)
		{
			Message = message;
		}

		[DataMember]
		public String Message { get; set; }

		public override string ToString()
		{
			return Message;
		}
	}
}