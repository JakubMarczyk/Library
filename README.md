# 📚 Aplikacja do Zarządzania Biblioteką z Kodami QR

## 📖 Opis projektu
Aplikacja służy do zarządzania biblioteką i ułatwienia użytkownikom korzystania z jej zasobów.  
Pozwala na obsługę wypożyczeń, przeglądanie książek, sprawdzanie dostępności oraz korzystanie z technologii **kodów QR** w celu szybkiego wypożyczania, zwrotu lub przekazania książek innym użytkownikom.

Projekt zawiera dwa interfejsy:
- **Administrator** – zarządzanie bazą danych, książkami, autorami, kategoriami, półkami oraz generowanie kodów QR.
- **Użytkownik** – wyszukiwanie książek, sprawdzanie dostępności, interakcja z chatbotem, wypożyczanie i zwroty poprzez skanowanie kodów QR.

---

## ✨ Funkcjonalności
- Dodawanie, edytowanie i usuwanie książek, autorów, kategorii i półek.
- Przegląd i wyszukiwanie książek w bibliotece.
- Sprawdzanie dostępności książek.
- Wypożyczanie i zwracanie książek poprzez kody QR.
- Przedłużanie terminu zwrotu.
- Przekazywanie wypożyczonych książek między użytkownikami przez kod QR.
- Rozmowa z botem biblioteki w celu pomocy w wyszukiwaniu – [PLANOWANE]

---

## 🛠 Technologie
- **Frontend:** Angular
- **Backend:** .NET (C#)
- **Baza danych:** PostgreSQL
- **API dokumentacja:** Swagger
- **Obsługa kodów QR:** [ZXing.Net]
