using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airlineBooking.Models
{
    public class RouteModel
    {
        public int OriginId { get; set; }

        public int DestinationId { get; set; }

        public DateTime DepartureDate { get; set; }

        public List<CarrierModel> Carrier { get; set; }

        public RouteModel()
        {
            this.Carrier = new List<CarrierModel>();
        }
    }
}