namespace Varus.Parking.Domain
{
    /// <summary>
    /// Information about the setup of a parking house.
    /// </summary>
    public class ParkingHouseInformation
    {
        /// <summary>
        /// Gets or sets the total number of parking spots.
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// Gets or sets the size of a parking spot.
        /// </summary>
        public Size ParkingSpotSize { get; set; }
        /// <summary>
        /// Gets or sets the percentage of parking spots that are reserved for contract clients.
        /// </summary>
        public float PortionOfParkingSpotsReservedForContractClients { get; set; }
        /// <summary>
        /// Gets or sets the hourly cost for parking.
        /// </summary>
        public decimal HourlyRate { get; set; }        
    }
}
