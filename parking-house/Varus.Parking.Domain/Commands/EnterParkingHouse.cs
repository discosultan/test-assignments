using Varus.Core;

namespace Varus.Parking.Domain.Commands
{
    public class EnterParkingHouse : Command
    {
        public Client Client { get; set; }
    }
}
