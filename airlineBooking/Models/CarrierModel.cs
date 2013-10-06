namespace airlineBooking.Models
{
    public class CarrierModel
    {
        /// <summary>
        /// Gets or Sets CarrierId
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets DisplayCode
        /// </summary>
        public string DisplayCode { get; set; }

        /// <summary>
        /// Gets or Sets IsMonetized
        /// </summary>
        public bool IsMonetized { get; set; }

        /// <summary>
        /// Gets or Sets WebsiteId
        /// </summary>
        public string WebsiteId { get; set; }
    }
}