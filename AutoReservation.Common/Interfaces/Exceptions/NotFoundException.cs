using System;
using System.Runtime.Serialization;

namespace AutoReservation.Common.Interfaces.Exceptions
{
	[DataContract]
	public class NotFoundException
	{
		public NotFoundException(String entityType, int entityId)
		{
			EntityType = entityType;
			EntityId = entityId;
			Message = "Entity of Type '" + EntityType + "' with Id '" + EntityId + "' not found.";
		}

		[DataMember]
		public string EntityType { get; set; }

		[DataMember]
		public int EntityId { get; set; }

		[DataMember]
		public string Message { get; set; }
	}
}