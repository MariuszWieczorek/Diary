using Diary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diary
{
    public static class Other
    {
        public static void TestLinq()
        {
            var list1 = new List<int>() { -2, 432, 22, 5, 85 };

            // Zapytanie LINQ - Query Syntax
            // samo LINQ zwróci nam interfejs IEnumerable
            // gdy chcemy aby została zwrócona lista dodajemy .ToList()
            var list2 = (from x in list1
                         where x > 10
                         orderby x descending
                         select x).ToList();

            // Wyrażenie LINQ = Metod Syntax
            // z użyciem wyrażeń Lambda

            var list3 = list1.Where(x => x > 10).OrderByDescending(x => x).ToList();

            var students = new List<Student>();

            var students1 = from x in students
                            select x.Id;

            var students2 = students.Select(x => x.Id);


            // czy wszystkie elementy są większe od zera
            var allPositives = list1.All(x => x > 0);
            MessageBox.Show(allPositives.ToString());

            // cz jakikolwiek element > 100
            var anyNumberBiggerThan100 = list1.Any(x => x > 100);
            MessageBox.Show(anyNumberBiggerThan100.ToString());

            // czy zawiera 10
            var contain10 = list1.Contains(10);

            var sum = list1.Sum();
            var count = list1.Count();
            var average = list1.Average();
            var min = list1.Min();
            var max = list1.Max();
            // pierwszy element, może być warunek
            var firstElement = list1.First(x => x > 10);



            return;
        }

        /// <summary>
        /// zabawy z dziedziczeniem
        /// </summary>
        public static void TestInheritance()
        {
            var student1 = new Student();
            student1.Id = 1;
            student1.FirstName = "Marek";

            Person person1;


            // obiekt pochodny możesz przypisać do obiektu bazowego

            person1 = student1;
            // MessageBox.Show(person1.Id.ToString());

            // Najpierw wywoływany jest konstruktor klasy pochodnej, później bazowej 

            // sealed - zamykamy klasę na dziedziczenie, nigdy nie będzie ona nadrzędną do żadnego typu
            // polimorfizm - wielopostaciowość
            // statyczny - przeciążania funkcji i operatorów
            // nie można przeciążyć metody, która różni się tylko zwracanym typem
            // dynamiczny 
            // powiązany z metodami wirtualnymi i abstrakcyjnymi, przesłanianie i nadpisywanie funkcji
            // dzięki temu, że student i teacher dziedziczą po tej samej klasie bazowej możemy je dodać do wspólnej listy

            var list3 = new List<Person>
            {
                new Student { Id = 1, FirstName = "StudentImię", LastName = "StudentNazwisko", Math = "5,2,4" },
                new Teacher { Id = 2, FirstName = "NauczycielImię", LastName = "NauczycielNazwisko" },
            };

            // item jest klasy Person, więc mamy dostęp tylko do elementów wspólnych
            // aby odwołać się do GetInfo musimy rzutować na ten Obiekt
            // przaydatne dwa słowa kluczowe
            // IS - aby sprawdzić czy możemy rzutować na ten obiekt
            // AS - aby wykonać to rzutowanie
            // takie podejście jest bardzo problematyczne, wprowadz duży bałagan w kodzie
            // wywołanie metod na podstawie zrzuconego typu jest bardzo złe
            foreach (var item in list3)
            {
                if (item is Student)
                    MessageBox.Show((item as Student).GetStudentInfo());
                else if (item is Teacher)
                    MessageBox.Show((item as Teacher).GetTeacherInfo());

            }

            // to samo z wykorzystaniem polimorfizmu
            // każdy z obiektów ma taką samą metodę, króra zostanie wywołana automatycznie bez sprawdzania obiektu
            foreach (var item in list3)
            {
                MessageBox.Show(item.GetInfo());

            }

            // Abstrakcje
            // W c# Abstrakcje realizujemy za pomocą klas abstrakcyjnych lub za pomocą interfejsów
            // Nie interesują nas strzegóły danej implementacji, lecz to co jest udostępniane na zewnątrz
            // mamy jakiś kontrakt i wiemy, że wszystkie implementacje muszą ją realizować, nie interesuje nas w jaki sposób
            // wprowadzanie abstrakcji 
            // loose coupling
            // hight cohesion 
            // luźne powiązanie - nie uzależniamy się od danej implementacji (np: możemy zapisywać do formatu XML lub do JSON lub do bazy danych)
            // są bardziej rozszerzalne
            // mogą być łatwiej testowane
            // klasa abstrakcyjna powstała po to by być klasą bazową do innych klas
            // nie można utworzyć obiektu klasy abstrakcyjnej
            // klasa abstrakcyjna może zawierać same deklaracje metod, lub ich definicje (implementacje)
            // metoda abstrakcyjna - mamy pewność, że każda klasa pochodna musi zaimplementować własną wersję tej metody
            // są z natury wirtualne, nie mogą zawierać ciała
            // metoda abstrakcyjna musi być napisana w klasie abstrakcyjnej i musi być przysłonięta w klasie dziedziczącej przez override

            // interfejs w odróżnieniu od klas abstrakcyjnych umożliwia całkowite oddzielenie szczegółów implementacji od udostępnionych elementów
            // wersje <  c#8.0 - same deklaracje metod bez ich definicji
            // wersje >= c#8.0 - mogą być również definicje metod

            // w klasie abstrakcyjnej
            // słowo kluczowe abstract
            // klasa może dziedziczyć tylko po jednej klasie abstrakcyjnej
            // mogą zostać oznaczone atrybuty dostępu
            // może być konstruktor domyślny

            // w interfejsach
            // słowo kluczowe interface
            // klasa może implementować dowolną liczbę interfejsów
            // składowe nie mogą zostać oznaczone atrybutami dostępu, niejawnie jest to public
            // nie może być kontruktora

            // Hermetyzacja nazywana również enkapsulacją polega na ukrywaniu pewnych danych
            // możemy ukryć szczegóły implementacji , wrażliwe dane 
            // pomagać się zabezpieczyć przed niepożądanym zachowaniem
            // uzyskujemy ją dzięki stosowaniu:
            // modyfikatory dotępu: public, private, protected, internal
            // właściwości
            // const - nie można zmieniać,
            // readonly - może być przypisane podczas deklaracji lub w konstruktorze, w metodzie już nie można zmieniać

            // kompozycja
            // technika podobnie jak dziedziczenie pozwalająca na ponowne wykorzystanie kodu
            // używamy innej klasy w naszej klasie

            // kiedy kompozycja a kiedy dziedziczenie
            // dziedziczenie gdy klasa jest szczególnym przypadkiem innej klasy
            // np samochód jest szczególnym przypadkiem klasy pojazd

            // kompozycja gdy klasa powinna zawirać dodatkową funkcjonalność zawartoś w innej klasie
            // np klasa samochód zawiera w sobie klasę silnik

            // Delegaty to Obiekty, któe wskazują na dowolne metody 
            // do delegata możemy przypisać metody pasujące do niego sygnaturą
            // możemy go przekazać jako parametr do funkcji

            // Predefiniowane delegaty
            // Funk przyjmuje metody które zwracają wartość
            // Action przyjmuje metody które nie zwracają żadnej wartości

            // dodamy własne zdarzenie które będzie powiadamiać okno główne gdy zostanie dodany użytkownik do pliku
            // dzięki temu będziemy mogli odświeżyć sobie dataGrid
            // aby zdefiniować event potrzebujemy najpierw delegata



        }

    }
}
