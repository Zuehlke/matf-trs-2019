# Testiranje u razvoju softvera
Repozitorijum sa materijalima za kurs "Testiranje u razvoju softvera" koji se drži na Matematičkom fakultetu univerziteta u Beogradu.

## Virtualna mašina za razvoj

Virtualna mašina dolazi sa preinstaliranim alatima za razvoj kao što su 
- git klijent
- Visual Studio 2017 (Comunity Edition)
- SQL Server


Svi ovi alati su vam neophodni ukoliko želite da MVC aplikaciju i testove pokrenete na svom *privatnom računaru*.


## Podešavanje privatnog računara za razvoj
Kako biste preuzeli i pokrenuli trenutnu verziju projekta potrebno je:
1. Da instalirate git, Visual Studio 2017 (CE) i Sql Sever Developer Edition na vaš računar.
2. Da se iz Command Prompt-a ili Powershell-a pozicionirate u folder po vašem izboru
3. Kucanjem komande `git clone https://github.com/Zuehlke/matf-trs-2019.git` u terminalu preuzmete repozitorijum
4. Otvorite `eBidder.sln` fajl iz Visual Studia
5. Za pokretanje MVC aplikacije možete pritisnuti `F5` na tastaturi
6. Testovi se pokreću iz TestExplorer-a u Visual Studio-u


## Preuzimanje najnovije verzije koda i testova
Predavači će svake nedelje dodavati nove testove na repozitorijum nakon predavanja.

Za najnoviju verziju sa dodatnim testovima u terminalu (Command Prompt ili Powershell) iz foldera u kome se nalazi projekat (`eBidder.sln` fajl) možete pozvati `git pull`.

*Alternativa*: Ukoliko koristite Visual Studio:
- Otvorite .sln u Visual Studiju, kliknite na "Team Explorer" (*nalazi se pored taba "Solution Explorer"*)
- Kliknite na opciju "Sync"
- Zatim kliknite na "Pull"



