using Varus.Core;

namespace Varus.Parking.Domain.Exceptions
{
    public class ParkingHouseFull : DomainException
    {
        public Client Client { get; set; }
        public int ParkingHouseCapacity { get; set; }
    }
}
