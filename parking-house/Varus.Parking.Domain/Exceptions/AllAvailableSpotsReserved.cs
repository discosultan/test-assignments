using Varus.Core;

namespace Varus.Parking.Domain.Exceptions
{
    public class AllAvailableSpotsReserved : DomainException
    {
        public Client Client { get; set; }
    }
}
