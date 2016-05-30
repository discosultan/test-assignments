using Varus.Core;

namespace Varus.Parking.Domain.Exceptions
{
    public class ParkingBillNotPaid : DomainException
    {
        public Client Client { get; set; }
        public decimal BillAmount { get; set; }
    }
}
