using Varus.Core;

namespace Varus.Parking.Domain.Commands
{
    public class LeaveParkingHouse : Command
    {
        public Client Client { get; set; }        
    }
}
