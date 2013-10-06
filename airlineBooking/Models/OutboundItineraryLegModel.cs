using System;
using System.Collections.Generic;

namespace airlineBooking.Models
{
    public class OutboundItineraryLegModel
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets OriginStation
        /// </summary>
        public int OriginStation { get; set; }

        /// <summary>
        /// Gets or Sets DestinationStation
        /// </summary>
        public int DestinationStation { get; set; }

        /// <summary>
        /// Gets or Sets DepartureDatetime
        /// </summary>
        public DateTime DepartureDateTime { get; set; }

        /// <summary>
        /// Gets or Sets ArricalDatetime
        /// </summary>
        public DateTime ArrivalDateTime { get; set; }

        /// <summary>
        /// Gets or Sets Duration
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or Sets MarketingCarrierIds
        /// </summary>
        public List<string> MarketingCarrierIds { get; set; }

        /// <summary>
        /// Gets or Sets OperatingCarrierIds
        /// </summary>
        public List<string> OperatingCarrierIds { get; set; }

        /// <summary>
        /// Gets or Sets PricingOptionModelList
        /// </summary>
        public List<PricingOptionModel> PricingOptionModelList { get; set; }

        /// <summary>
        /// Gets or Sets ItineraryLegType
        /// </summary>
        public string ItineraryLegType { get; set; }

        /// <summary>
        /// Contructer
        /// </summary>
        public OutboundItineraryLegModel()
        {
            OperatingCarrierIds = new List<string>();
            MarketingCarrierIds = new List<string>();
            PricingOptionModelList = new List<PricingOptionModel>();
        }
    }
}