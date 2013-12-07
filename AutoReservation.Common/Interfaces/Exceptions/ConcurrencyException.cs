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
            this.Message = message;
        }

        [DataMember]
        public string Message { get; set; }
    }
}
