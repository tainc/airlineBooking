using System;

namespace airlineBooking.Models
{
    public class QuotesModel
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets QuoteRequestId
        /// </summary>
        public int QuoteRequestId { get; set; }

        /// <summary>
        /// Gets or Sets RequestDateTime
        /// </summary>
        public DateTime RequestDateTime { get; set; }

        /// <summary>
        /// Gets or Sets Age
        /// </summary>
        public double Age { get; set; }

        /// <summary>
        /// Gets or Sets Price
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Gets or Sets IsReturn
        /// </summary>
        public bool IsReturn { get; set; }
    }
}