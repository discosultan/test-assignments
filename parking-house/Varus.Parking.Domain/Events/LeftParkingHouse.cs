using Varus.Core;

namespace Varus.Parking.Domain.Events
{
    public class LeftParkingHouse : Event
    {        
        public Client Client { get; set; }        
    }
}
