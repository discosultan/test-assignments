using System.Collections.Generic;
using Varus.Core;

namespace Varus.Parking.Domain.ReadModels
{
    /// <summary>
    /// Contract for read model to query data from a parking house. 
    /// An interface is the recommended access medium for read models as it 
    /// hides away <see cref="ISubscribeTo{TEvent}"/> implementations.
    /// </summary>
    public interface IParkingHouseStatus
    {
        /// <summary>
        /// Gets money received from direct payments. This does not include money
        /// earned from contract clients.
        /// </summary>
        decimal AmountOfMoneyReceived { get; }
        /// <summary>
        /// Gets the total number of cars parked at a parking house. This value is
        /// cumulative meaning that if a same client enters the parking house
        /// multiple times during the simulation, each parking session is considered
        /// as a new car parked.
        /// </summary>
        int TotalNumberOfCarsParked { get; }
        /// <summary>
        /// Gets clients currently in the parking house.
        /// </summary>
        IEnumerable<Client> ClientsInParkingHouse { get; }
        /// <summary>
        /// Gets clients who have been in the parking house but have left.
        /// </summary>
        IEnumerable<Client> ClientsWhoHaveLeftParkingHouse { get; }
    }
}
