# Wild-Boar-IOT

Projekt na przedmiot Serwisy Internetowe .NET

## GUI

### Vue.js (Łukasz)

- [x] przeglądanie zgromadzonych danych w formie tabelarycznej
- [ ] filtrowanie danych według daty, typu czujnika (grupa instancji), instancji czujnika,
- [ ] sortowanie danych w tabeli;
- [ ] pobieranie danych w formacie CSV, JSON dla wybranych filtrów;
- [ ] prezentacja danych w formie wykresów dla wybranych filtrów.
- [ ] pulpit, na którym widać ostatnią wartość i średnią wartość (dla ostatnich 100
   komunikatów) dla każdego sensora - prezentowane dane nie wymagają odświeżania
   strony, wartości odświeżają się samodzielnie po zarejestrowaniu nowej wartości

## API

### ASP .NET CORE 5 (Łukasz, Jakub)

- [x] Automatyczne pobieranie danych z kolejek, konwersję danych i ich zapis w bazie danych;
- [x] Odczyt danych z bazy danych,
- [ ] Filtrowanie i sortowanie danych na podstawie podanych kryteriów,
- [ ] Eksport danych (przefiltrowanych, sortowanych) w formacie CSV, JSON

## DATABASE

### MondoDB (Łukasz)

- [x] Storowanie danych z API

## QUE SERVICE

### rabbitmq (Jakub)

- [x] Generowanie danych z czujników i wrzucenie ich do rabbita

