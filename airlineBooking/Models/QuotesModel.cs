using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airlineBooking.Models
{
    public class QuotesModel
    {
        public int QuoteId { get; set; }

        public int MinPrice { get; set; }

        public bool Direct { get; set; }

        public RouteModel Route { get; set; }

        public QuotesModel() {
            this.Route = new RouteModel();
        }
    }
}