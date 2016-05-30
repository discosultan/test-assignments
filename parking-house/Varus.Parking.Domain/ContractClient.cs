namespace Varus.Parking.Domain
{
    /// <summary>
    /// Parking house client, who has a contract with the parking house company. The contract provides
    /// the client with additional rights and discounts.
    /// </summary>
    public class ContractClient : Client
    {
        /// <inheritdoc />
        public override decimal ProcessDiscount(decimal amount)
        {
            return 0.0m;
        }

        /// <inheritdoc />
        public override bool IsAllowedOnReservedSpot()
        {
            return true;
        }
    }
}
