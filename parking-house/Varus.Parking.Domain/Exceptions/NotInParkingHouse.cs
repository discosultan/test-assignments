using Varus.Core;

namespace Varus.Parking.Domain.Exceptions
{
    public class NotInParkingHouse : DomainException
    {
        public Client Client { get; set; }        
    }
}
