using System;
using System.Collections.Generic;

namespace AutoReservation.BusinessLayer
{
    public class RelationExistsException : Exception
    {
	    public RelationExistsException(object entity, List<object> relatedTo)
	    {
			this.Entity = entity;
			this.RelatedTo = relatedTo;
	    }

		public object Entity { get; set; }
		public List<object> RelatedTo { get; set; }

		public override string ToString()
		{
			return "[" + Entity.ToString() + "] releates to [" + RelatedTo.ToString() + "]";
		}

    }
}