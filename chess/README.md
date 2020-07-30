## About

Fujitsu's assignment where the assignee is required to find and print the minimum moves required for a particual chess piece to move from location A to B.

## Assignment (Estonian)

Malemäng (proovitöö ülesanne)

Mängu (töö) eesmärk: jõuda ratsuga malelaua punktist A punkti B vähima arvu sammuga.

Nõuded
Programm peab olema kirjutatud C# keeles konsoolrakendusena. Käivitamisel programm peab lugema sisendandmed failist ning kirjutama vastus väljundfaili. Failide nimed antakse programmi parameetritesse k�ivitamisel.

Sisendparameetrid
- Figuuri (ratsu) algpositsioon malelaual kujul A1, kus A tähendab positsiooni horisontaalis ning 1 vertikaalis. Kõik figuuride positsioonid antakse ette kasutades sellist kirjaviisi.
- Figuuri (ratsu) sihtpositsioon malelaual.
- Nimekiri kinnistest positsioonidest (positsioonid, mille peal asuvad teised figuurid). Teised malelaual olevad figuurid ei liigu (jäävad paika).

Programmi väljund
- Sammude arv, mida tuleb teha ratsul, et jõuda positsioonist A positsiooni B.
- Ratsu tee punktist A punkti B, ehk nimekiri positsioonidest, mida läbib ratsu omal teel.

Failide struktuur
Kõik programmis kasutatud ja loodud failid peavad olema ANSI kodeeringus tekstifailid, Windows'i reavahetustega (kasutades 2 baiti reavahetuse kohta).

Sisendfail
See on tekstifail, kus esimesel real on figuuri algpositsioon, teisel real on figuuri sihtpositsioon ning kolmandal real on komadega eraldatud nimekiri kinnistest positsioonidest, näiteks:
A1
H8
B2, D7, G6

Väljundfail
Väljundfail on tekstifail, kus esimesel real on vajalik sammude arv ning teisel real on komadega eraldatud positsioonid, mida läbib ratsu omal teel algpunktist sihtpunkti (k.a. sihtpositsioon), näiteks:
6
B3, C5, E6, G5, F7, H8

Nõuded tulemusele
Proovitöö tulemus peab olema kompileeritav programmi kood (ilma .exe failita), mis peab olema kompileeritav ja töötama Microsoft.Net versioonil 3.5 või 4.0. Juhul kui arendamisel kasutatakse Microsoft Visual Studio 2005, 2008 või 2010 või selle Express versioon, tulemusega pange kokku ka projekti failid. Tulemus peab olema kokku pandud .zip failis.

Täiendavad nõuded
Lisaks põhinõuete täitmisele kasuks tuleb realiseerida (või vähemalt olla valmis rääkima, mida oleks vaja muuta programmis realiseerimiseks) järgmised täiendavad nõuded:
- Võimalus valida malefiguuri valikust: ratsu, kuningas, lipp, vanker, oda
- Võimalus juhtida malelaua suurust (N x M laud).
- Võimalus tagastada kõik läbitava tee versioonid väljundfailis (juhul kui on olemas kaks erinevat lähimat teed punktist A punkti B).

Juhul, kui täiendavad nõuded on realiseeritud, üleantavas paketis peavad olema sisend- ja väljundfailide näidised koos kirjeldustega.
