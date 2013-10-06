namespace airlineBooking.Models
{
    public class QuoteRequests
    {
        /// <summary>
        /// Gets or Sets Id 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets AngentId
        /// </summary>
        public string AngentId { get; set; }

        /// <summary>
        /// Gets or Sets HasResults
        /// </summary>
        public bool HasResults { get; set; }

        /// <summary>
        /// Gets or Sets JacquardPoolId
        /// </summary>
        public int JacquardPoolId { get; set; }
    }
}