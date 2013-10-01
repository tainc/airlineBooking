using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airlineBooking.Models
{
    public class OutboundLegModel
    {
        public int OriginId { get; set; }

        public int DestinationId { get; set; }

        public DateTime DepartureDate { get; set; }

        public List<CarrierIdsModel> CarrerIds { get; set; }

        public OutboundLegModel()
        {
            this.CarrerIds = new List<CarrierIdsModel>();
        }
    }
}