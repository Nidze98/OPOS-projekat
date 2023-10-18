Odabrana poglavlja iz operativnih sistema 

Projektni zadatak – Raspoređivač zadataka i paralelna obrada multimedijalnih podataka  

U proizvoljnom objektno-orijentisanom programskom jeziku realizovati raspoređivač zadataka. Definisati programski API za raspoređivač i realizovati logički odvojenu GUI aplikaciju za raspoređivanje zadataka koja koristi definisani API. Kreirati tip zadatka koji vrši paralelnu obradu multimedijalnih podataka. 

Pravilno dokumentovati projekat. Program se mora moći kompajlirati, izvršiti i testirati. Uz priloženi rad treba da se obezbijede i primjeri koji se mogu iskoristiti za testiranje programa. Obezbijediti nekoliko jediničnih testova kojima se demonstriraju funkcionalnosti iz stavki u specifikaciji projektnog zadatka (nije potrebno za GUI aplikaciju i fajl-sistem). Pridržavati se principa objektno-orijentisanog programiranja, SOLID principa, principa pisanja čistog koda i konvencija za korišteni programski jezik. 

**(15)** Raspoređivač zadataka 

Pravilno realizovati osnovne funkcionalnosti za raspoređivač zadataka. Raspoređivač zadataka mora biti realizovan  u  sklopu  biblioteke.  Obezbijediti  jednostavan  API  koji  korisniku  omogućava  da  vrši raspoređivanje zadataka. Omogućiti da se za raspoređivač specificira maksimalan broj zadataka koji se konkurentno izvršavaju.** Dozvoljeno je da se definiše sopstveni API koji mora biti korišten u implementaciji zadataka,  kao  način  da  se  kooperativnim  mehanizmima  omoguće  funkcionalnosti  iz  specifikacije projektnog zadatka. Dozvoljeno je i  da se izvršavanje svakog zadataka pokreće kao  zaseban proces. Obezbijediti jednostavne jedinične testove koji demonstriraju rad raspoređivača. 

Osnovne funkcionalnosti koje API raspoređivača treba da omogući su: 

- dodavanje zadatka u red raspoređivača, sa započinjanjem ili bez započinjanja, 
- raspoređivanje zadataka za vrijeme dok se izvršavaju već raspoređeni zadaci, 
- započinjanje, pauziranje, nastavak i zaustavljanje zadatka, 
- čekanje na kraj izvršavanja zadatka i 
- specifikacija algoritma za raspoređivanje, što uključuje FIFO (FCFS) i prioritetno raspoređivanje. 

Omogućiti da se za zadatak mogu opcionalno specificirati: datum i vrijeme početka izvršavanja, dozvoljeno ukupno vrijeme izvršavanja i rok do kojeg zadatak mora biti izvršen ili prekinut.** 

