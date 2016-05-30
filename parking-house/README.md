## About

Finestmedia's assignment where the assignee is required to create a parking house system that conforms to specified rules.

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

## Assignment (Estonian)

### Sissejuhatus

Proovitöö eesmärk ei ole selgitada seda kui hästi proovitöö tegija tunneb .NET raamistiku baasklasse ja API, vaid saada aru kuidas on kandidaat võimeline rakendama objekt orienteeritud programmeerimise meetodikaid ja mustreid.

Proovitöö lahendus peab koosnema süsteemist, mis tagab allpool kirjeldatud ärinõuded ja moodultestidest (unit testid), mis simuleeriksid süsteemi kasutamist. Proovitööle rakenduvad järgmised eeldused:

- Kasutajaliides ei ole vajalik
- Andmebaas ei ole kohustuslik. Algandmed võib laadida kas tekst või XML failidest või nad võivad olla defineeritud eraldi klassides.
- Saadetud töö peab sisaldama kõiki vajalikke kolmanda osapoole teeke ja komponente.
- Töö peab olema kompileeritav Visual Studio 2008 või 2010’ga ilma lisatoiminguteta.

### Taust

Tegemist on parkimismaja parkimiskohtade haldamissüsteemiga. Parkimismaja on avatud 24h/365p, parkimismaja teenindab kahte sorti kliente:

- Lepingulised kliendid, kes saavad parkida terve kuu jooksul (proovitöö kontekstis terve simulatsiooni jooksul).
- Kliendid, kes maksavad iga pargitud tunni eest.

### Ärilised nõuded

1. Autod saavad siseneda ja väljuda suvalistel ajahetkedel. Sõltuvalt parkimislepingu tüübist tuleb esitada kliendile arve (proovitöö kontekstis võib lihtsat liita kõikide parkimisarvete summad).
2. Simulatsiooni lõpus peab genereerima statistika mitu autot simulatsiooni ajal parkis ja arvutama kõikide lepinguliste ja tavaliste klientide arvete summa ehk simulatsiooni ajal teenitud raha.
3. Parkimismaja sees ei tohi olla korraga rohkem kui 500 autot.
4. Lepingulisel kliendil peab olema alati garanteeritud parkimiskoht. 10% parkimiskohadest peavad olema reserveeritud lepingulistele klientidele. Kas kliendil on kindel parkimiskoht või ta saab parkida esimesele ettejuhtuvale kohale, on proovitöö tegija otsustada.
5. Parkimiskoha pikkus on 4 meetrit ja laius 2 meetrit. Pikemad või laiemad autod ei tohi siseneda parkimismajja.

### Soovitused

Selleks, et saavutada maksimaalne punktide arv, peab proovitöö tegija võtma arvesse järgmiseid soovitusi:

- Lahendus peab baseeruma valdkonna põhisel kavandil (domain driven design)
- Lahendus peab olema jagatud kihtideks
- Lahenduses peavad olema tagatud nõrgad seosed kihtide vahel (kasutades liideseid ja dependency injection)
- Vajadusel kommenteerida olulisemaid meetodeid