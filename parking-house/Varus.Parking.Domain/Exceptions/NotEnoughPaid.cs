using Varus.Core;

namespace Varus.Parking.Domain.Exceptions
{
    public class NotEnoughPaid : DomainException
    {
        public Client Client { get; set; }
        public decimal BillAmount { get; set; }
        public decimal AmountPaid { get; set; }
    }
}
