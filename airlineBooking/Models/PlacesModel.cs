using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airlineBooking.Models
{
    public class PlacesModel
    {
        /// <summary>
        /// Gets or Sets PlaceId 
        /// </summary>
        public int PlaceId { get; set; }

        /// <summary>
        /// Gets or Sets IataCode
        /// </summary>
        public string IataCode { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        public string Type { get; set; }
    }
}