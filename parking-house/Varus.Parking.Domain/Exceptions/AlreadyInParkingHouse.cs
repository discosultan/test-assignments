using Varus.Core;

namespace Varus.Parking.Domain.Exceptions
{
    public class AlreadyInParkingHouse : DomainException
    {
        public Client Client { get; set; }
    }
}
