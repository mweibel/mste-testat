using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.Interfaces.Exceptions
{
    [DataContract]
    public class NotFoundException
    {
        public NotFoundException(String entityType, int entityId)
        {
            this.entityType = entityType;
            this.entityId = entityId;
        }
        private String entityType;
        private int entityId;

        [DataMember]
        public String EntityType 
        {
            get {
                return entityType;
            }
        }
        [DataMember]
        public int EntityId
        {
            get
            {
                return entityId;
            }
        }
        [DataMember]
        public String Message
        {
            get
            {
                return "Entity of Type '" + EntityType + "' with Id '" + EntityId + "' not found.";
            }
        }
    }
}
