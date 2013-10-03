using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airlineBooking.Models
{
    public class FlightModel
    {
        /// <summary>
        /// Gets or Sets from city
        /// </summary>
        public string FromCity { get; set; }

        /// <summary>
        /// Gets or Sets to city
        /// </summary>
        public string ToCity { get; set; }

        /// <summary>
        /// Gets or Sets from date
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or Sets to date
        /// </summary>
        public DateTime ToDate { get; set; }
    }
}