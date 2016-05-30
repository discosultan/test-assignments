using System;
using System.Linq;
using Ninject;
using NUnit.Framework;
using Varus.Core;
using Varus.Core.Utilities;
using Varus.Parking.Domain;
using Varus.Parking.Domain.Aggregates;
using Varus.Parking.Domain.Commands;
using Varus.Parking.Domain.ReadModels;

namespace Varus.Parking.UnitTests.ReadModels
{
    /// <summary>
    /// Defines a set of tests for querying data from parking house read model. While
    /// these don't validate business rules, they simply demonstrate the querying capabilities
    /// of an event sourced system.
    /// </summary>
    [TestFixture]
    class ParkingHouseStatusTests
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
        
        private MessageDispatcher _messageDispatcher;

        private Guid _id;
        private IParkingHouseStatus _parkingHouseStatus;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            // Resolve dependencies.
            IEventStore store = new InMemoryEventStore();
            ITimeMachine time = new LinearStepTimeMachine(InitialDateTime, TimeStepPerAction);
            var kernel = new StandardKernel();
            kernel.Bind<ParkingHouse>().To<ParkingHouse>()
                .WithConstructorArgument(time)
                .WithConstructorArgument(ParkingHouseInformation);
            IDependencyResolver ioc = new NinjectDependencyResolver(kernel);

            // Create message dispatcher and register parking house.
            _messageDispatcher = new MessageDispatcher(store, ioc);
            _messageDispatcher.ScanType<ParkingHouse>();
        }        

        [SetUp]
        public void Setup()
        {
            _id = Guid.NewGuid();
            _parkingHouseStatus = new ParkingHouseStatus(_id);            
            _messageDispatcher.ScanInstance(_parkingHouseStatus);
        }

        [Test]
        public void ReadParkingHouseStatus_EndOfSimulation()
        {
            // Arrange.
            var client1 = NewClient();            
            var client2 = NewClient();
            const int hoursClient1SpendsInParkingHouse = 2;
            const int totalNumberOfCarsParked = 2;

            // Act.
            _messageDispatcher.SendCommand(new EnterParkingHouse { Id = _id, Client = client1 });   // 00:00 ENTERED
            _messageDispatcher.SendCommand(new EnterParkingHouse { Id = _id, Client = client2 });   // 01:00 ENTERED            
            _messageDispatcher.SendCommand(new PayParkingBill                                       // 02:00 PAID PARKING BILL
            {
                Id = _id,
                Client = client1,
                Amount = hoursClient1SpendsInParkingHouse * ParkingHouseInformation.HourlyRate
            });
            _messageDispatcher.SendCommand(new LeaveParkingHouse { Id = _id, Client = client1 });   // 03:00 LEFT            

            // Assert.
            Assert.AreEqual(hoursClient1SpendsInParkingHouse * ParkingHouseInformation.HourlyRate, _parkingHouseStatus.AmountOfMoneyReceived);
            Assert.AreEqual(totalNumberOfCarsParked, _parkingHouseStatus.TotalNumberOfCarsParked);
            Assert.False(_parkingHouseStatus.ClientsInParkingHouse.Any(client => client == client1));
            Assert.True(_parkingHouseStatus.ClientsInParkingHouse.Any(client => client == client2));            
            Assert.True(_parkingHouseStatus.ClientsWhoHaveLeftParkingHouse.Any(client => client == client1));
            Assert.False(_parkingHouseStatus.ClientsWhoHaveLeftParkingHouse.Any(client => client == client2));            
        }

        [Test]
        public void ReadHistoricalParkingHouseStatus_MiddleOfSimulation()
        {
            // Arrange.
            var client1 = NewClient();
            var client2 = NewClient();            
            const int hoursClient1SpendsInParkingHouse = 2;
            const int hoursToReplay = 2;
            const int totalNumberOfCarsParked = 2;
            DateTime replayUntil = InitialDateTime.Add(TimeStepPerAction.Multiply(hoursToReplay));

            // Act.
            _messageDispatcher.SendCommand(new EnterParkingHouse { Id = _id, Client = client1 });   // 00:00 ENTERED            
            _messageDispatcher.SendCommand(new EnterParkingHouse { Id = _id, Client = client2 });   // 02:00 ENTERED <== WE ARE GOING TO REPLAY TILL THAT POINT         
            _messageDispatcher.SendCommand(new PayParkingBill                                       // 03:00 PAID PARKING BILL
            {
                Id = _id,
                Client = client1,
                Amount = hoursClient1SpendsInParkingHouse * ParkingHouseInformation.HourlyRate
            });
            _messageDispatcher.SendCommand(new LeaveParkingHouse { Id = _id, Client = client1 });   // 04:00 LEFT            

            // Recreate read model and republish events only until a certain point in time.
            _parkingHouseStatus = new ParkingHouseStatus(_id);
            _messageDispatcher.ScanInstance(_parkingHouseStatus);
            _messageDispatcher.RepublishEventsUntil<ParkingHouse>(_id, replayUntil);

            // Assert.
            Assert.AreEqual(0, _parkingHouseStatus.AmountOfMoneyReceived);
            Assert.AreEqual(totalNumberOfCarsParked, _parkingHouseStatus.TotalNumberOfCarsParked);
            Assert.True(_parkingHouseStatus.ClientsInParkingHouse.Any(client => client == client1));
            Assert.True(_parkingHouseStatus.ClientsInParkingHouse.Any(client => client == client2));            
            Assert.False(_parkingHouseStatus.ClientsWhoHaveLeftParkingHouse.Any(client => client == client1));
            Assert.False(_parkingHouseStatus.ClientsWhoHaveLeftParkingHouse.Any(client => client == client2));            
        }

        private static Client NewClient()
        {
            return new Client { Vehicle = new Vehicle() };
        }
    }
}
