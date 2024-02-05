# Aplikacja Magazynu Produktów i Przepisów
## Opis
Aplikacja Magazynu Produktów i Przepisów to prosta platforma umożliwiająca zarządzanie listą produktów, przepisów oraz produktów końcowych w magazynie. 
Aplikacja oparta jest na architekturze Client - API, co umożliwia łatwą integrację z różnymi interfejsami użytkownika oraz innymi systemami.

## Zarządzanie Produktami:

- Dodawanie nowych produktów do magazynu.
- Edytowanie informacji o istniejących produktach.
- Usuwanie produktów z magazynu.
- Zarządzanie Przepisami:

## Zarządzanie Przepisami:
- Dodawanie nowych przepisów na wytworzenie produktów końcowych.
- Edytowanie istniejących przepisów.
- Usuwanie przepisów.
- Zarządzanie Produktami Końcowymi:

## Zarządzanie Produktami Gotowymi:
- Tworzenie nowych produktów końcowych na podstawie dostępnych przepisów.
- Edytowanie informacji o produktach końcowych.
- Usuwanie produktów końcowych z magazynu.
- Interfejs API:


# Dane Techniczne
## Backend:

- Aplikacja została napisana w języku ASP.NET.
- Wykorzystuje framework 7.0 do obsługi żądań HTTP.
- Komunikuje się z bazą danych MariaDB 10.5.

## Frontend:

Interfejs użytkownika został zaimplementowany przy użyciu REACT, TypeScript, JavaScript.
Komunikuje się z backendem poprzez zapytania API.

## Baza Danych:

Wykorzystano bazę danych MariaDB w wersji 10.5 uruchomioną na serwerze Linux Debian.


# Instalacja i Uruchomienie
## Backend:

## Frontend:

Przejdź do folderu frontend.
Zainstaluj zależności poleceniem npm install.
Uruchom aplikację poleceniem npm start.

## Baza Danych:

Zainstaluj i skonfiguruj bazę danych zgodnie z instrukcjami w pliku database-setup.md.
Dostęp do API:

Aplikacja API dostępna jest pod adresem https://localhost:7093/api.
Dokumentacja API
Pełna dokumentacja API dostępna jest w pliku API-docs.md.

# Autorzy
SchoolCanteen została stworzona przez zespół RSK Solution w składzie 
- Kacper
- Szymon
- Robert

Dla bardziej szczegółowych informacji i wsparcia, prosimy o kontakt pod adresem mailToUs@rsksolution.com.
