using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airlineBooking.Models
{
    public class FlightModel
    {
        public string fromCity { get; set; }

        public string toCity { get; set; }

        public DateTime fromDate { get; set; }

        public DateTime toDate { get; set; }
    }
}