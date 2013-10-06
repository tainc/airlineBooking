using System.Collections.Generic;
using airlineBooking.Models;

namespace airlineBooking.ViewModels
{
    public class SearchFlightViewModel
    {
        /// <summary>
        /// Gets or Sets QuoteRequestsModelList
        /// </summary>
        public List<QuoteRequestModel> QuoteRequestsModelList { get; set; }

        /// <summary>
        /// Gets or Sets QuotesModelList
        /// </summary>
        public List<QuotesModel> QuotesModelList { get; set; }

        /// <summary>
        /// Gets or Sets OutboundItineraryLegsesModelList
        /// </summary>
        public List<OutboundItineraryLegModel> OutboundItineraryLegsesModelList { get; set; }

        /// <summary>
        /// Gets or Sets InboundItineraryLegsesModelList
        /// </summary>
        public List<InboundItineraryLegModel> InboundItineraryLegsesModelList { get; set; }

        /// <summary>
        /// Gets or Sets StationsModelList
        /// </summary>
        public List<StationModel> StationsModelList { get; set; }

        /// <summary>
        /// Gets or Sets CarrierModelList
        /// </summary>
        public List<CarrierModel> CarrierModelList { get; set; }

        public SearchFlightViewModel()
        {
            QuoteRequestsModelList = new List<QuoteRequestModel>();
            QuotesModelList = new List<QuotesModel>();
            OutboundItineraryLegsesModelList = new List<OutboundItineraryLegModel>();
            InboundItineraryLegsesModelList = new List<InboundItineraryLegModel>();
            StationsModelList = new List<StationModel>();
            CarrierModelList = new List<CarrierModel>();
        }
    }
}