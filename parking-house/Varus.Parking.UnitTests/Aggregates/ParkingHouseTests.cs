using System;
using NUnit.Framework;
using Varus.Core;
using Varus.Parking.Domain;
using Varus.Parking.Domain.Aggregates;
using Varus.Parking.Domain.Commands;
using Varus.Parking.Domain.Events;
using Varus.Parking.Domain.Exceptions;

namespace Varus.Parking.UnitTests.Aggregates
{
    /// <summary>
    /// Defines a set of tests for <see cref="ParkingHouse"/> aggregate. These tests
    /// validate business rules.
    /// </summary>
    [TestFixture]
    class ParkingHouseTests : BehaviorDrivenTests<ParkingHouse>
    {
        private static readonly DateTime InitialDateTime = new DateTime(2000, 1, 1);
        private static readonly TimeSpan TimeStepPerAction = TimeSpan.FromHours(1);
        private static readonly ParkingHouseInformation ParkingHouseInformation = new ParkingHouseInformation
        {
            Capacity = 500,
            HourlyRate = 10,
            ParkingSpotSize = new Size(4, 2),
            PortionOfParkingSpotsReservedForContractClients = 0.1f
        };

        private Guid _id;
        private Client _regularClient;
        private Client _contractClient;

        [SetUp]
        public void Setup()
        {
            _id = Guid.NewGuid();
            _regularClient = new Client { Vehicle = new Vehicle { Size = new Size(1, 1) } };
            _contractClient = new ContractClient { Vehicle = new Vehicle { Size = new Size(1, 1) } };
        }

        protected override ParkingHouse BuildNewAggregate()
        {
            return new ParkingHouse(new LinearStepTimeMachine(InitialDateTime, TimeStepPerAction), ParkingHouseInformation);
        }

        [Test]
        public void CanEnterParkingHouse()
        {
            // Act & Arrange.
            Test(
                Given(),
                When(new EnterParkingHouse
                {
                    Id = _id,
                    Client = _regularClient
                }),
                Then(new EnteredParkingHouse
                {
                    Id = _id,
                    Client = _regularClient,
                    DateTime = InitialDateTime
                }));
        }

        [Test]
        public void CannotEnterParkingHouse_ParkingHouseFull()
        {
            // Arrange.
            var events = new Event[ParkingHouseInformation.Capacity];
            for (int i = 0; i < events.Length; ++i)
                // We are adding contract clients to actually fill up ALL the parking spots (including reserved spots).
                events[i] = new EnteredParkingHouse { Client = new ContractClient { Vehicle = new Vehicle() } };

            // Act & Assert.
            Test(
                Given(events),
                When(new EnterParkingHouse
                {
                    Id = _id,
                    Client = _regularClient
                }),
                ThenFailWith<ParkingHouseFull>());
        }

        [Test]
        public void CannotEnterParkingHouseTwice()
        {
            // Act & Assert.
            Test(
                Given(new EnteredParkingHouse
                {
                    Id = _id,
                    Client = _regularClient
                }),
                When(new EnterParkingHouse
                {
                    Id = _id,
                    Client = _regularClient
                }),
                ThenFailWith<AlreadyInParkingHouse>());
        }

        [Test]
        public void CanLeaveParkingHouse()
        {
            // Arrange.
            DateTime entered = InitialDateTime.Add(-TimeStepPerAction);
            decimal expectedBill = (decimal) TimeStepPerAction.TotalHours * ParkingHouseInformation.HourlyRate;

            // Act & Assert.
            Test(
                Given(new EnteredParkingHouse
                {
                    Id = _id,
                    Client = _regularClient,
                    DateTime = entered
                },
                new PaidParkingBill
                {
                    Id = _id,
                    Client = _regularClient,
                    Amount = expectedBill
                }),
                When(new LeaveParkingHouse
                {
                    Id = _id,
                    Client = _regularClient
                }),
                Then(new LeftParkingHouse
                {
                    Id = _id,
                    Client = _regularClient,
                    DateTime = InitialDateTime
                }));
        }

        [Test]
        public void CannotLeaveParkingHouseTwice()
        {
            // Act & Assert.
            Test(
                Given(),
                When(new LeaveParkingHouse
                {
                    Id = _id,
                    Client = _regularClient
                }),
                ThenFailWith<NotInParkingHouse>());
        }

        [Test]
        public void CannotLeaveParkingHouse_BillNotPaid()
        {
            // Arrange.
            DateTime entered = InitialDateTime.Add(-TimeStepPerAction);

            // Act & Assert.
            Test(
                Given(new EnteredParkingHouse
                {
                    Id = _id,
                    Client = _regularClient,
                    DateTime = entered
                }),
                When(new LeaveParkingHouse
                {
                    Id = _id,
                    Client = _regularClient
                }),
                ThenFailWith<ParkingBillNotPaid>());
        }

        [Test]
        public void CannotEnterParkingHouse_VehicleTooWide()
        {
            // Arrange.
            var client = new Client
            {
                Vehicle = new Vehicle { Size = ParkingHouseInformation.ParkingSpotSize + new Size(1, 0) }
            };

            // Act & Assert.
            Test(
                Given(),
                When(new EnterParkingHouse
                {
                    Id = _id,
                    Client = client
                }),
                ThenFailWith<ParkingSpotsNotBigEnough>());
        }

        [Test]
        public void CannotEnterParkingHouse_VehicleTooLong()
        {
            // Arrange.
            var client = new Client
            {
                Vehicle = new Vehicle { Size = ParkingHouseInformation.ParkingSpotSize + new Size(0, 1) }
            };

            // Act & Assert.
            Test(
                Given(),
                When(new EnterParkingHouse
                {
                    Id = _id,
                    Client = client
                }),
                ThenFailWith<ParkingSpotsNotBigEnough>());
        }

        [Test]
        public void ContractClientCanLeaveParkingHouse_NoNeedToPayForBill()
        {
            // Arrange.
            DateTime entered = InitialDateTime.Add(-TimeStepPerAction);

            // Act & Assert.
            Test(
                Given(new EnteredParkingHouse
                {
                    Id = _id,
                    Client = _contractClient,
                    DateTime = entered
                }),
                When(new LeaveParkingHouse
                {
                    Id = _id,
                    Client = _contractClient
                }),
                Then(new LeftParkingHouse
                {
                    Id = _id,
                    Client = _contractClient,
                    DateTime = InitialDateTime
                }));
        }

        [Test]
        public void RegularSpotsFull_ContractClientCanEnter()
        {
            // Arrange.
            var events = new Event[ParkingHouseInformation.Capacity -
                (int)(ParkingHouseInformation.Capacity * ParkingHouseInformation.PortionOfParkingSpotsReservedForContractClients)];
            for (int i = 0; i < events.Length; ++i)
                events[i] = new EnteredParkingHouse { Client = new Client { Vehicle = new Vehicle() } };

            // Act & Assert.
            Test(
                Given(events),
                When(new EnterParkingHouse
                {
                    Id = _id,
                    Client = _contractClient
                }),
                Then(new EnteredParkingHouse
                {
                    Id = _id,
                    Client = _contractClient,
                    DateTime = InitialDateTime
                }));
        }

        [Test]
        public void RegularSpotsFull_RegularClientCannotEnter()
        {
            // Arrange.
            var events = new Event[ParkingHouseInformation.Capacity -
                (int)(ParkingHouseInformation.Capacity * ParkingHouseInformation.PortionOfParkingSpotsReservedForContractClients)];
            for (int i = 0; i < events.Length; ++i)
                events[i] = new EnteredParkingHouse { Client = new Client { Vehicle = new Vehicle() } };

            // Act & Assert.
            Test(
                Given(events),
                When(new EnterParkingHouse
                {
                    Id = _id,
                    Client = _regularClient
                }),
                ThenFailWith<AllAvailableSpotsReserved>());
        }
    }
}
