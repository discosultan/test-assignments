using System;
using System.Collections.Generic;
using Varus.Core;
using Varus.Parking.Domain.Events;

namespace Varus.Parking.Domain.ReadModels
{
    /// <summary>
    /// Read only model used for querying data about a parking house.
    /// </summary>
    public class ParkingHouseStatus :
        IParkingHouseStatus,
        ISubscribeTo<EnteredParkingHouse>,
        ISubscribeTo<LeftParkingHouse>,
        ISubscribeTo<PaidParkingBill>
    {
        private readonly Guid _id;
        private readonly List<Client> _clientsInParkingHouse = new List<Client>();
        private readonly List<Client> _clientsWhoHaveLeftParkingHouse = new List<Client>();

        /// <summary>
        /// Gets money received from direct payments. This does not include money
        /// earned from contract clients.
        /// </summary>
        public decimal AmountOfMoneyReceived { get; private set; }

        /// <summary>
        /// Gets the total number of cars parked at a parking house. This value is
        /// cumulative meaning that if a same client enters the parking house
        /// multiple times during the simulation, each parking session is considered
        /// as a new car parked.
        /// </summary>
        public int TotalNumberOfCarsParked { get; private set; }

        /// <summary>
        /// Gets clients currently in the parking house.
        /// </summary>
        public IEnumerable<Client> ClientsInParkingHouse
        {
            get { return _clientsInParkingHouse; }
        }

        /// <summary>
        /// Gets clients who have been in the parking house but have left.
        /// </summary>
        public IEnumerable<Client> ClientsWhoHaveLeftParkingHouse
        {
            get { return _clientsWhoHaveLeftParkingHouse; }
        }

        /// <summary>
        /// Constructs a new instance of <see cref="ParkingHouseStatus"/>.
        /// </summary>
        /// <param name="parkingHouseId">Parking house identifier.</param>
        public ParkingHouseStatus(Guid parkingHouseId)
        {
            _id = parkingHouseId;
        }

        public void Handle(EnteredParkingHouse e)
        {
            if (e.Id == _id)
            {
                TotalNumberOfCarsParked++;
                _clientsInParkingHouse.Add(e.Client);
                _clientsWhoHaveLeftParkingHouse.Remove(e.Client);
            }
        }

        public void Handle(LeftParkingHouse e)
        {
            if (e.Id == _id)
            {
                _clientsInParkingHouse.Remove(e.Client);
                _clientsWhoHaveLeftParkingHouse.Add(e.Client);
            }
        }

        public void Handle(PaidParkingBill e)
        {
            if (e.Id == _id)
                AmountOfMoneyReceived += e.Amount;
        }
    }
}
