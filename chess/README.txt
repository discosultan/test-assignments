Malem�ng (proovit�� �lesanne)

M�ngu (t��) eesm�rk: j�uda ratsuga malelaua punktist A punkti B v�hima arvu sammuga.

N�uded
Programm peab olema kirjutatud C# keeles konsoolrakendusena. K�ivitamisel programm peab lugema sisendandmed failist ning kirjutama vastus v�ljundfaili. Failide nimed antakse programmi parameetritesse k�ivitamisel.

Sisendparameetrid
�	Figuuri (ratsu) algpositsioon malelaual kujul A1, kus A t�hendab positsiooni horisontaalis ning 1 � vertikaalis. K�ik figuuride positsioonid antakse ette kasutades sellist kirjaviisi.
�	Figuuri (ratsu) sihtpositsioon malelaual.
�	Nimekiri kinnistest positsioonidest (positsioonid, mille peal asuvad teised figuurid). Teised malelaual olevad figuurid ei liigu (j��vad paika).

Programmi v�ljund
�	Sammude arv, mida tuleb teha ratsul, et j�uda positsioonist A positsiooni B.
�	Ratsu �tee� punktist A punkti B, ehk nimekiri positsioonidest, mida l�bib ratsu omal teel.

Failide struktuur
K�ik programmis kasutatud ja loodud failid peavad olema ANSI kodeeringus tekstifailid, Windows� reavahetustega (kasutades 2 baiti reavahetuse kohta).

Sisendfail
See on tekstifail, kus esimesel real on figuuri algpositsioon, teisel real on figuuri sihtpositsioon ning kolmandal real on komadega eraldatud nimekiri kinnistest positsioonidest, n�iteks:
A1
H8
B2, D7, G6

V�ljundfail
V�ljundfail on tekstifail, kus esimesel real on vajalik sammude arv ning teisel real on komadega eraldatud positsioonid, mida l�bib ratsu omal teel algpunktist sihtpunkti (k.a. sihtpositsioon), n�iteks:
6
B3, C5, E6, G5, F7, H8

N�uded tulemusele
Proovit�� tulemus peab olema kompileeritav programmi kood (ilma .exe failita), mis peab olema kompileeritav ja t��tama Microsoft.Net versioonil 3.5 v�i 4.0. Juhul kui arendamisel kasutatakse Microsoft Visual Studio 2005, 2008 v�i 2010 v�i selle Express versioon, tulemusega pange kokku ka projekti failid. Tulemus peab olema kokku pandud .zip failis.

T�iendavad n�uded
Lisaks p�hin�uete t�itmisele kasuks tuleb realiseerida (v�i v�hemalt olla valmis r��kida, mida oleks vaja muuta programmis realiseerimiseks) j�rgmised t�iendavad n�uded:
�	V�imalus valida malefiguuri valikust: ratsu, kuningas, lipp, vanker, oda
�	V�imalus juhtida malelaua suurust (N x M laud).
�	V�imalus tagastada k�ik l�bitava tee versioonid v�ljundfailis (juhul kui on olemas kaks erinevat l�himat teed punktist A punkti B).

Juhul, kui t�iendavad n�uded on realiseeritud, �leantavas paketis peavad olema sisend- ja v�ljundfailide n�idised koos kirjeldustega.
