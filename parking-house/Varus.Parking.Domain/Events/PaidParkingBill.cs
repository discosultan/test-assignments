using Varus.Core;

namespace Varus.Parking.Domain.Events
{
    public class PaidParkingBill : Event
    {
        public Client Client { get; set; }
        public decimal Amount { get; set; }        
    }
}
