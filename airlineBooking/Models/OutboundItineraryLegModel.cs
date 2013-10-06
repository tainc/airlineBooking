using System;
namespace airlineBooking.Models
{
    public class OutboundItineraryLegModel
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }

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
        /// Gets or Sets ItineraryLegType
        /// </summary>
        public string ItineraryLegType { get; set; }
    }
}