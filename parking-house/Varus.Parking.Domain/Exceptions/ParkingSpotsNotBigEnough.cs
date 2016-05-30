using Varus.Core;

namespace Varus.Parking.Domain.Exceptions
{
    public class ParkingSpotsNotBigEnough : DomainException
    {
        public Client Client { get; set; }
        public Size ParkingSpotSize { get; set; }
    }
}
