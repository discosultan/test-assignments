ParkingHouse
=============================================

## Assemblies

- **Varus.Core**: Inspired by [Edument CQRS Starter Kit](http://cqrs.nu/), it contains the core building blocks used for applying CQRS and event sourcing patterns.
- **Varus.Parking.Domain**: Parking house domain models and business rules live here.
- **Varus.Parking.UnitTests**: Provides a set of unit test which validate that the domain conforms to business rules.

## Business rules

1. Vehicle's must be able to enter and leave the parking house at any point in time. Depending on the type of client, the client must be provided a parking bill before he/she is allowed to leave.
2. At the end of a simulation, statistics have to be generated to provide the following information: how many cars parked, total amount of money earned.
3. Parking house has a maximum capacity of 500 vehicles.
4. 10% of parking spots need to reserved only for contract clients.
5. A parking spot is 4 meters long and 2 meters wide.
