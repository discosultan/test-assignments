## About

CGI's assignment where the assignee is required to sort an array of strings based on a custom alphabet.

## Assignment (Estonian)

### Ülesanne

Tekstifaili esimesel real on mingis järjestuses tähed a..z, igal järgmisel real üks nendest tähtedest koosnev sõna (kuni 20 sõna pikkusega kuni 30 tähte). Paigutada ekraani vasakule poolele sisendandmed ja paremale poolele üksteise alla sõnad järjestatuna faili esimesel real antud "tähestiku" järjekorras. Näide:

Sisend:
```
qwertyuiopasdfghjklzxcvbnm
aas
aasta
year
jahr
god
```

Väljund:
```
qwertyuiopasdfghjklzxcvbnm 	
aas                     year
aasta                   aas
year                    aasta
jahr                    god
god                     jahr
```

### Soovitud lahendus

1. Realiseerida programm (käsurea rakendus), mis:
   - Loeb sisendfailist „tähestiku” ja järjestatavad sõnad. Sisendfaili asukoht antakse ette programmi käsurea parameetrina. Failiks on tekstifail, mille struktuur on kirjeldatud ülal.
   - Järjestab sisendfailist loetud sõnad vastavalt sisendfailist loetud „tähestikule”.
   - Väljastab standardväljundisse tähestiku, sõnade algse järjestuse ja sõnade järjestuse „tähestiku” järgi vastavalt ülal toodud näitele.
2. Kirjeldada programmi algoritm failis “Tähestik.txt”.
3. Kommenteerida lähtekoodi avalikud klassid ja meetodid

