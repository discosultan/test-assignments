namespace Varus.Parking.Domain
{
    /// <summary>
    /// Parking house client. A person who uses the parking house as a location
    /// to keep his/her vehicle.
    /// </summary>
    public class Client
    {
        // TODO: Add an id. Reference comparison works only as long as we stay within
        // TODO: the bounds of a process and use an in memory event store.

        /// <summary>
        /// Gets or sets the vehicle client is driving.
        /// </summary>
        public Vehicle Vehicle { get; set; }

        /// <summary>
        /// Applies client specific discounts to parking bills.
        /// </summary>
        /// <param name="amount">Original parking bill.</param>
        /// <returns>Discounted bill.</returns>
        public virtual decimal ProcessDiscount(decimal amount)
        {
            // No discount for a regular client.
            return amount;
        }

        /// <summary>
        /// Determines whether the client is allowed to park on a reserved spot.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsAllowedOnReservedSpot()
        {
            return false;
        }
    }
}
