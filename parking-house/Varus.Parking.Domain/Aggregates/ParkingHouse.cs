using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Varus.Core;
using Varus.Parking.Domain.Commands;
using Varus.Parking.Domain.Events;
using Varus.Parking.Domain.Exceptions;

namespace Varus.Parking.Domain.Aggregates
{
    /// <summary>
    /// The root aggregate for parking house domain/business logic. Handles incoming
    /// commands and generates events based on business rules.
    /// </summary>
    public class ParkingHouse : Aggregate,
        IHandleCommand<EnterParkingHouse>,
        IHandleCommand<LeaveParkingHouse>,
        IHandleCommand<PayParkingBill>,
        IApplyEvent<EnteredParkingHouse>,
        IApplyEvent<LeftParkingHouse>,
        IApplyEvent<PaidParkingBill>
    {
        private readonly ITimeMachine _time;
        // We use thread safe dictionary for thread safe command handling.
        private readonly IDictionary<Client, ClientParkingData> _clients = 
                     new ConcurrentDictionary<Client, ClientParkingData>();
        private readonly ParkingHouseInformation _info;

        private int _reservedSpotsTaken;
        private int _regularSpotsTaken;

        private enum ParkingSpotType
        {
            Regular,
            Reserved
        }

        private class ClientParkingData
        {
            public DateTime DateTimeEntered;
            public ParkingSpotType ParkingSpotType;
            public bool ParkingBillPaid;
        }

        /// <summary>
        /// Creates a new instance of <see cref="ParkingHouse"/>.
        /// </summary>
        /// <param name="time">Concept of time used by the parking house.</param>
        /// <param name="info">Parking house setup.</param>
        public ParkingHouse(ITimeMachine time, ParkingHouseInformation info)
        {
            _time = time;
            _info = info;
        }

        public IEnumerable<Event> Handle(EnterParkingHouse command)
        {
            // Validate.
            if (_clients.Keys.Any(client => command.Client == client))
                throw new AlreadyInParkingHouse { Id = command.Id, Client = command.Client };

            if (_clients.Count >= _info.Capacity)
                throw new ParkingHouseFull
                {
                    Id = command.Id,
                    Client = command.Client,
                    ParkingHouseCapacity = _info.Capacity
                };

            if (command.Client.Vehicle.Size.Width > _info.ParkingSpotSize.Width ||
                command.Client.Vehicle.Size.Length > _info.ParkingSpotSize.Length)
                throw new ParkingSpotsNotBigEnough
                {
                    Id = command.Id,
                    Client = command.Client,
                    ParkingSpotSize = _info.ParkingSpotSize
                };

            if (!command.Client.IsAllowedOnReservedSpot() &&
                _regularSpotsTaken >= _info.Capacity - GetTotalNumberOfReservedSpots())
                throw new AllAvailableSpotsReserved { Id = command.Id, Client = command.Client };

            // Generate event.
            yield return new EnteredParkingHouse
            {
                Id = command.Id,
                Client = command.Client,
                DateTime = _time.Now()
            };
        }

        public IEnumerable<Event> Handle(PayParkingBill command)
        {
            // Validate.
            var clientData = _clients[command.Client];
            var timeClientWantsToLeave = _time.Now();
            var bill = CalculateBill(command.Client, clientData.DateTimeEntered, timeClientWantsToLeave);
            if (command.Amount < bill)
                throw new NotEnoughPaid { Id = command.Id, AmountPaid = command.Amount, BillAmount = bill };

            // Generate event.            
            yield return new PaidParkingBill
            {
                Id = command.Id,
                Client = command.Client,
                Amount = bill,
                DateTime = _time.Now()
            };
        }

        public IEnumerable<Event> Handle(LeaveParkingHouse command)
        {
            // Validate.
            if (_clients.Keys.All(client => command.Client != client))
                throw new NotInParkingHouse { Id = command.Id, Client = command.Client };

            var clientData = _clients[command.Client];
            var timeClientWantsToLeave = _time.Now();
            var bill = CalculateBill(command.Client, clientData.DateTimeEntered, timeClientWantsToLeave);
            if (bill != 0 && !clientData.ParkingBillPaid)
                throw new ParkingBillNotPaid { Id = command.Id, BillAmount = bill, Client = command.Client };

            // Generate event.
            yield return new LeftParkingHouse
            {
                Id = command.Id,
                Client = command.Client,
                DateTime = timeClientWantsToLeave
            };
        }

        public void Apply(EnteredParkingHouse e)
        {
            var spotType = DecideWhichSpotToTake(e.Client);
            switch (spotType)
            {
                case ParkingSpotType.Regular:
                    _regularSpotsTaken++;
                    break;
                case ParkingSpotType.Reserved:
                    _reservedSpotsTaken++;
                    break;
            }
            _clients.Add(e.Client, new ClientParkingData { ParkingSpotType = spotType, DateTimeEntered = e.DateTime });
        }

        public void Apply(PaidParkingBill e)
        {
            _clients[e.Client].ParkingBillPaid = true;
        }

        public void Apply(LeftParkingHouse e)
        {
            switch (_clients[e.Client].ParkingSpotType)
            {
                case ParkingSpotType.Regular:
                    _regularSpotsTaken--;
                    break;
                case ParkingSpotType.Reserved:
                    _reservedSpotsTaken--;
                    break;
            }
            _clients.Remove(e.Client);
        }

        private ParkingSpotType DecideWhichSpotToTake(Client client)
        {
            // Client who is allowed to park on a reserved spot will always prefer the reserved spot to a regular spot.
            if (client.IsAllowedOnReservedSpot() &&
                _reservedSpotsTaken < GetTotalNumberOfReservedSpots())
            {
                return ParkingSpotType.Reserved;
            }
            return ParkingSpotType.Regular;
        }

        private int GetTotalNumberOfReservedSpots()
        {
            return (int) (_info.Capacity * _info.PortionOfParkingSpotsReservedForContractClients);
        }

        private decimal CalculateBill(Client client, DateTime from, DateTime to)
        {
            var difference = to - from;
            var bill = (decimal) difference.TotalHours * _info.HourlyRate;
            return client.ProcessDiscount(bill);
        }
    }
}