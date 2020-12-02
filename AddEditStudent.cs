using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Diary
{
    public partial class AddEditStudent : Form
    {


        // event – krok 1: aby zdefiniować event potrzebujemy najpierw delegata
        public delegate void MySimpleDelegate();

        // event – krok 2: definiujemy event
        public event MySimpleDelegate StudentAddedEvent;

        // event – krok 3: dobrą praktyką jest pisanie metod pomocniczych
        // które będziemy wywoływać w miejscach w których ma być wyzwolone zdarzenie
        private void OnStudentAdded()
        {
            StudentAddedEvent?.Invoke();
        }

        private int _studentId;
        private Student _student;
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>(Program.FilePath);

        //private FileHelper<List<Student>> _fileHelper2 =
        //new FileHelper<List<Student>>(this._filePath);


        // przekazujemy parametr id, domyślną wartością jest 0
        // będzie tu zawarta logika biznesowa dla dodawania i edytowania
        public AddEditStudent(int id = 0)
        {
            _studentId = id;
            InitializeComponent();
            GetStudentData();

            StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Dodawanie Ucznia";

            tbFirstName.Select();
        }

        /// <summary>
        /// Metoda pobierająca dane o studencie
        /// </summary>
        private void GetStudentData()
        {
            if (_studentId != 0)
            {
                Text = "Edycja Danych Ucznia";

                var students = _fileHelper.DeserializeFromFile();
                _student = students
                   .FirstOrDefault(x => x.Id == _studentId);

                if (_student == null)
                    throw new Exception($"Brak użytkownika o numerze Id = {_studentId}");

                FillTextBoxes();
            }
        }

        /// <summary>
        /// Uzupałniamy pola tekstowe wczytanymi danymi
        /// </summary>
        private void FillTextBoxes()
        {
            tbId.Text = _student.Id.ToString();
            tbFirstName.Text = _student.FirstName;
            tbLastName.Text = _student.LastName;
            rtbComments.Text = _student.Comments;
            tbMath.Text = _student.Math;
            tbPhysics.Text = _student.Physics;
            tbPolishLang.Text = _student.PolishLang;
            tbForeignLang.Text = _student.ForeignLang;
            tbTechnology.Text = _student.Technology;
        }


        /// <summary>
        /// Zatwierdzanie zmian
        /// wspólna logika przycisku zatwierdź dla Edycji i Dodania Nowego Ucznia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            var students = _fileHelper.DeserializeFromFile();

            // pobieramy jeszcze raz Listę wszystkich uczniów
            // jeżeli edytujamy, to usuwamy z listy ucznia któego dane będziemy edytować
            // później dodamy tego ucznia i zapiszemy w pliku
            // robimy tak aby było jak najwięcej wspólnej logiki
            // pole _studentId aby mieć dostęp do parametru przekazanego do konstruktora
            if (_studentId != 0)
            {
                students.RemoveAll(x => x.Id == _studentId);
            }
            else
            {
                AssignIdToNewStudent(students);
            }

            AddNewStudentToList(students);

            // event – krok 4
            // wywołujemy metodę pomocniczą przed zamknięciem ekranu
            // gdy metoda zostanie wywołana wyzwoli zdarzenie StudentAdded
            // która powiadomi o tym zdarzeniu swoich subskrybentów

            OnStudentAdded();
            await LongProcessAsync();
            Close();
        }


        private void LongProcess()
        {
            Thread.Sleep(3000);
        }


        // zwracaną wartością musi być zawsze Task - zamiast void
        // Task<int> zamiast int
        // wywołujemy metodę z klasy statycznej Run, która przyjmuje delegata Action
        private async Task LongProcessAsync()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
            });
        }

        // zapis równoznaczny z powyższym
        private async Task LongProcessAsync2()
        {
            var action = new Action(ActionDelegateMetod);
            await Task.Run(action);
        }

        // równoważny zapis do
        private void ActionDelegateMetod()
        {
            Thread.Sleep(3000);
        }



        /// <summary>
        /// Dodajemy studenta do listy
        /// przypisujemy wartości z pól tekstowych do obiektu
        /// obiekt dodajemy do listy
        /// na koniec wykonujemy serializację
        /// </summary>
        /// <param name="students"></param>
        private void AddNewStudentToList(List<Student> students)
        {
            var student = new Student
            {
                Id = _studentId,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Comments = rtbComments.Text,
                Math = tbMath.Text,
                Physics = tbPhysics.Text,
                PolishLang = tbPolishLang.Text,
                ForeignLang = tbForeignLang.Text,
                Technology = tbTechnology.Text
            };

            students.Add(student);
            _fileHelper.SerializeToFile2(students);
        }



        /// <summary>
        /// Nadajemy Id nowemu uczniowi 
        /// Wykorzystujemy LINQ, Sortujemy malejąco
        /// zczytujemny pierwszy rekord jeżeli istnieje, jeżeli nie to NULL
        /// </summary>
        /// <param name="students"></param>
        private void AssignIdToNewStudent(List<Student> students)
        {
            var studentWithHighestId = students
                    .OrderByDescending(x => x.Id).FirstOrDefault();

            _studentId = studentWithHighestId == null ? 1 : studentWithHighestId.Id + 1;
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
