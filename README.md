# Interview_HappyTeam
 
Witam, nazywam się Jakub Konert a to projekt, który przygotowałem dla działu rekrutacji firmy HappyTeam. Postanowiłem wybrać zadanie nr 2, cytując jego treść: 

"Your client has asked you to create a web app for renting Tesla cars in Mallorca. They have a few locations (Palma Airport, Palma City Center, Alcudia and Manacor) and people can rent and return the cars at any one of them. They rent all available passenger Tesla models (except the Semi). They will give you exact pricing later; for now, you should use amounts of your choosing. The website should allow you to create a reservation for a Tesla for a specified date range. It should also calculate the total cost of the reservation and store the reservation details in some database."

Rozwiązanie zostało przygotowane przy pomocy: .Net ASP NET core API, React oraz SQL Server Managment Studio. 
Ponadto użyto m.in.:
.Net: autoMapper, Singleton pattern, Builder pattern, EntityFramework
React: axios, Bootstrap React

Projekt nie skupiał się mocno na efekcie wizualnym strony, dlatego są niedociągnięcia.
Projekt zakłada, że użytkownik jest już zalogowany (lub że wszystkie zamówienia idą na konto "Interviewer"), zrezygnowano z opcji auteryzacji i autentykacji
Projekt zakłada, że użytkownik może tylko złożyć zamówienie i one od razu trafia do bazy danych. Dlatego użytkownik nie ma wglądu do zamówień. W kodzie przygotowano dodatkowe metody kontrolera to czytania wszystkich zamówień oraz status zamówienia, jednakże nie są one używane. Miało to na celu pokazania również kodu od tej części co jednak kłuci się z myślą YAGNI. Również z tą myślą kłócą się pola typu Countrires lub IsAvailable itp. w konfiguracji. Jest to otwarta furtka na rozwój konfiguracji na nowe kraje i zmiany.

Każdy model samochodu posiada wartość ile sztuk firma posiada, jednakże ta wartość nie ulega zmianie po zrobieniu zamówienia. Np. w celu sprawdzenia czy dany model jest dostępny czy jednak wsztystkie są akurat wypożyczone.

Konfiguracja trzymana jest bazie danych i założenie jest że każda nowa konfiguracja jest ustawiona jako IsActive = true, a jej poprzedniczki zmieniają wartość na false. Pozwala to wiedzieć która konfiguracja jest aktualna, a jednocześnie móc trzymać historie konfiguracji w bazie danych.

Nie znam się na samochodach, w tym Tesli, dlatego użyto nazw modeli z strony "https://www.tesla.com/pl_pl"

W celu uruchomienia projektu należy najpierw uruchomić serwer tzn. aplikację .Net, a następnie dopiero klienta tzn. aplikację React. Wynika to z faktu, że aplikacja React przy starcie woła serwer w celu uzyskania konfiguracji.
