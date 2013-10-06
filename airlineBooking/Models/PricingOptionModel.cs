using System.Collections.Generic;

namespace airlineBooking.Models
{
    public class PricingOptionModel
    {
        /// <summary>
        /// Gets or Sets IsConstrained
        /// </summary>
        public bool IsConstrained { get; set; }

        /// <summary>
        /// Gets or Sets QuoteIds
        /// </summary>
        public List<string> QuoteIds { get; set; }

        public PricingOptionModel()
        {
            QuoteIds = new List<string>();
        }
    }
}