using Varus.Core;

namespace Varus.Parking.Domain.Events
{
    public class EnteredParkingHouse : Event
    {
        public Client Client { get; set; }
    }
}
