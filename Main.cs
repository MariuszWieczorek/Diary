using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Diary
{
    public partial class Main : Form
    {

        private delegate void DisplayMessage(string message);
        private FileHelper<List<Student>> _fileHelper = 
            new FileHelper<List<Student>>(Program.FilePath);

        public Main()
        {
            InitializeComponent();
            Text = "Dziennik Ucznia";
            
            //PopulateStudents();
            //DeserializeAndShowStudents();
            RefreshDiary();
            SetColumnHeader();
            SetColumnProperities();
            // TestLinq();
            // TestInheritance();
            // wykorzystanie kompozycji
            // var student = new Student();
            // student.Address.City = "Szczecin";

            // 
            var messages1 = new DisplayMessage(DisplayMessage1);
            var messages2 = new Action<string>(DisplayMessage2);

            //messages1("aaaa");


            MetodUseDelegate2(messages2);

        }


        // metoda pasująca sygnaturą do zeefiniowanego delegata
        public void DisplayMessage1(string message)
        {
            MessageBox.Show($"Metoda 1 {message}");
        }

        public void DisplayMessage2(string message)
        {
            MessageBox.Show($"Metoda 2 {message}");
        }

        // metody przyjmujące jako parametr delegat
        private void MetodUseDelegate1(DisplayMessage mess)
        {
            mess("wykorzystanie własnego delegata");
        }

        private void MetodUseDelegate2(Action<string> mess)
        {
            mess("wykorzystanie delegata Action");
        }


        /// <summary>
        /// zabawy z dziedziczeniem
        /// </summary>
        private static void TestInheritance()
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
            foreach(var item in list3)
            {
                if(item is Student)
                    MessageBox.Show((item as Student).GetStudentInfo());
                else if (item is Teacher)
                    MessageBox.Show((item as Teacher).GetTeacherInfo());
                
            }

            // to samo z wykorzystaniem polimorfizmu
            // każdy z obiektów ma taką samą metodę, króra zostanie wywołana automatycznie bez sprawdzania obiektu
            foreach (var item in list3)
            {
                    MessageBox.Show( item.GetInfo());

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
            // Action przyjmuje metody które nie zwracają

            // dodamy własne zdarzenie które będzie powiadamiaćokno główne gdy zostanie dodaby użytkownik do pliku
            // dzięki temu będziemy mogli odświeżyć sobie dataGrid
            // aby zdefiniować event potrzebujemy najpierw delegata


        }

        private static void TestLinq()
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
            var firstElement = list1.First(x => x > 10 );



            return;
        }

        private void RefreshDiary()
        {
            var students = _fileHelper.DeserializeFromFile();
            //students = students.OrderBy(x => x.Id);
            dgvDiary.DataSource = students.OrderBy(x => x.Id).ToList();
        }

        private void SetColumnHeader()
        {
            dgvDiary.Columns[0].HeaderText = "Numer";
            dgvDiary.Columns[1].HeaderText = "Imię";
            dgvDiary.Columns[2].HeaderText = "Nazwisko";
            dgvDiary.Columns[3].HeaderText = "Komentarz";
            dgvDiary.Columns[4].HeaderText = "Matematyka";
            dgvDiary.Columns[5].HeaderText = "Fizyka";
            dgvDiary.Columns[6].HeaderText = "Technologia";
            dgvDiary.Columns[7].HeaderText = "Język Polski";
            dgvDiary.Columns[8].HeaderText = "Język Obcy";
        }

        private void SetColumnProperities()
        {
            dgvDiary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDiary.RowHeadersVisible = false;
            dgvDiary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // ustawić Anchor na Right, Top, Left, Down we właściwościach aby Grid rozszerzał się wraz z oknem
        }

        


     
        
        // wypełnienie listy studentów przykładowymi danymi
        public void PopulateStudents()
        {
            var students = new List<Student>();
            // w tym przypadku może też być z nawiasami po new Student() lub bez 
            students.Add(new Student() { Id = 1, FirstName = "Jan", LastName = "Kowalski" });
            students.Add(new Student { Id = 2, FirstName = "Jan", LastName = "Nowak" });
            students.Add(new Student { Id = 3, FirstName = "Alfred", LastName = "Kowalski" });
            students.Add(new Student { Id = 4, FirstName = "Joanna", LastName = "Bartkowiak" });
            _fileHelper.SerializeToFile2(students);
        }

        public void DeserializeAndShowStudents()
        {
            var students = _fileHelper.DeserializeFromFile();
            foreach (var item in students)
            {
                MessageBox.Show($"{item.FirstName} {item.LastName}");
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // okno jest zwykłą klasą więc tworzymy jego instancję
            var addEditStudent = new AddEditStudent();
            addEditStudent.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // sprawdzamy, czy jakiś wiersz został zaznaczony
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Zaznacz ucznia, którego chcesz usunąć");
                return;
            }

            // okno jest zwykłą klasą więc tworzymy jego instancję
            // F12 na nazwie klasy
            var addEditStudent = new AddEditStudent( Convert.ToInt32(dgvDiary.SelectedRows[0].Cells[0].Value));
            addEditStudent.ShowDialog();
            RefreshDiary();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // sprawdzamy, czy jakiś wiersz został zaznaczony
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nie zaznaczono żadnej pozycji");
                return;
            }

            var selectedStudent = dgvDiary.SelectedRows[0];
            var confirmDelete = MessageBox.Show($"Czy na pewno chcesz usunąć ucznia " +
                $"{selectedStudent.Cells[1].Value.ToString()} {selectedStudent.Cells[2].Value.ToString()}"
                ,"Usuwanie ucznia",MessageBoxButtons.OKCancel);

            if (confirmDelete == DialogResult.OK)
            {
                DeleteStudent((int)selectedStudent.Cells[0].Value);
                RefreshDiary();
            }

        }

        private void DeleteStudent(int id)
        {
            var students = _fileHelper.DeserializeFromFile();
            students.RemoveAll(x => x.Id == id);
            _fileHelper.SerializeToFile2(students);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        // zdarzenia
        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            MessageBox.Show("Mouse Enter");
        }
        // w MainDesigner.cs został dodany kod jak poniżej
        // this.btnAdd.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
        // czyli do zdefiniowanego zdarzenie MouseEnter w klasie Button dodajemy 
        // za pomocą operatora += metody, która ma zostać wyzwolona

    }
}
