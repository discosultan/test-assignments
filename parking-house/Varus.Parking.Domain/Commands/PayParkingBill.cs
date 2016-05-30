using Varus.Core;

namespace Varus.Parking.Domain.Commands
{
    public class PayParkingBill : Command
    {
        public Client Client { get; set; }
        public decimal Amount { get; set; }
    }
}
