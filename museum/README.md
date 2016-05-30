## Ülesanne

Muuseumis registreeriti päeva jooksul iga külastaja saabumis- ja lahkumisaeg minutilise
täpsusega. Nii saadi N väärtuste paari, kus esimene väärtus näitab külalise saabumis- ja
teine lahkumisaega. Leida ajavahemik, millal muuseumis viibis kõige rohkem külastajaid ja
kui palju neid oli. Külastusaja algus ja lõpp on kaasa arvatud, st näiteks kui üks külastaja
saabus kell 11:10 ja teine lahkus kell 11:10, siis loetakse, et kell 11:10 oli muuseumis 2
külastajat.


### Soovitud lahendus
1. Realiseerida programm, mis:
a. Loeb sisendfailist külastusajad. Sisendfaili asukoht antakse ette programmi
käsurea parameetrina. Failiks on tekstifail, kus igal real on ühe külastaja
saabumis- ja lahkumisaeg komaga eraldatult, näiteks:
```
10:15,14:20
11:10,15:22
```
Külastusajad võivad failis olla läbisegi, saabumis- või lahkumisajajärgi
sorteerimata. Testimiseks võib kasutada sisendfaili kylastusajad.txt.
b. Leiab sisendfailis saadud andmete põhjal ajavahemiku, millal muuseumis
viibis kõige rohkem külastajaid ning mitu külastajat sel ajavahemikul
muuseumis viibis.
c. Väljastab eelmises punktis leitud ajavahemiku ja külastajate arvu
standardväljundisse kujul <ajavahemiku algus>-<ajavahemiku
lõpp>;<külastajate arv>. Näiteks:
```
11:10-14:20;2
```
2. Kogu lähtekood paigutada alamkataloogi “src”.
3. Kirjeldada kasutatud ajavahemiku ja külastajate arvu leidmise algoritm tekstifaili
“Ajavahemiku leidmine.txt”.
4. Dokumenteerida lähtekoodi avalikud klassid ja meetodid vastavalt javadoc reeglitele
ning genereerida javadoc utiliidiga dokumentatsioon eraldi kataloogi “javadoc”.
