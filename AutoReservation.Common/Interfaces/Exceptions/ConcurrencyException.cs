using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.Interfaces.Exceptions
{
    [DataContract]
    public class ConcurrencyException
    {
        public ConcurrencyException(String message)
        {
            this.message = message;
        }

        private String message;

        [DataMember]
        public String Message
        {
            get
            {
                return message;
            }
        }
    }
}
