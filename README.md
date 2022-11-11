# Wild-Boar-IOT

Projekt na przedmiot Serwisy Internetowe .NET

## GUI

### Vue.js (Łukasz)

1. przeglądanie zgromadzonych danych w formie tabelarycznej,
2. filtrowanie danych według daty, typu czujnika (grupa instancji), instancji czujnika,
3. sortowanie danych w tabeli;
4. pobieranie danych w formacie CSV, JSON dla wybranych filtrów;
5. prezentacja danych w formie wykresów dla wybranych filtrów.
6. pulpit, na którym widać ostatnią wartość i średnią wartość (dla ostatnich 100
   komunikatów) dla każdego sensora - prezentowane dane nie wymagają odświeżania
   strony, wartości odświeżają się samodzielnie po zarejestrowaniu nowej wartości

## API

### ASP .NET CORE 5 (Łukasz(dzik), Jakub)

1. Automatyczne pobieranie danych z kolejek, konwersję danych i ich zapis w bazie danych;
2. Odczyt danych z bazy danych,
3. Filtrowanie i sortowanie danych na podstawie podanych kryteriów,
4. Eksport danych (przefiltrowanych, sortowanych) w formacie CSV, JSON

## DATABASE

### MondoDB (Łukasz)

1. Storowanie danych z API

## QUE SERVICE

### rabbitmq (Jakub)

1. Generowanie danych z czujników i wrzucenie ich do rabbita

