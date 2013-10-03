using airlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace airlineBooking.ViewModels
{
    public class SearchFlightViewModel
    {
        /// <summary>
        /// Gets or Sets list quotes model
        /// </summary>
        public List<QuotesModel> quotesModel { get; set; }

        /// <summary>
        /// Gets or Sets list places model
        /// </summary>
        public List<PlacesModel> placesModel { get; set; }

        /// <summary>
        /// Gets or Sets list carriers model
        /// </summary>
        public List<CarriersModel> carriersModel { get; set; }

        public SearchFlightViewModel()
        {
            this.quotesModel = new List<QuotesModel>();
            this.placesModel = new List<PlacesModel>();
            this.carriersModel = new List<CarriersModel>();
        }
    }
}