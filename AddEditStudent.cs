using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Diary
{
    public partial class AddEditStudent : Form
    {

        private int _studentId;
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>(Program.FilePath);

        //private FileHelper<List<Student>> _fileHelper2 =
        //new FileHelper<List<Student>>(this._filePath);


        // przekazujemy parametr id, domyślną wartością jest 0
        // będzie tu zawarta logika biznesowa dla dodawania i edytowania
        public AddEditStudent(int id = 0)
        {
            _studentId = id;

            
            InitializeComponent();
            if (id != 0)
            {
                var students = _fileHelper.DeserializeFromFile();
                var student = students
                   .FirstOrDefault(x => x.Id == id);

                if (students == null)
                    throw new Exception($"Brak użytkownika o numerze Id = {id}");

                tbId.Text = student.Id.ToString();
                tbFirstName.Text = student.FirstName;
                tbLastName.Text = student.LastName;
                rtbComments.Text = student.Comments;
                tbMath.Text = student.Math;
                tbPhysics.Text = student.Physics;
                tbPolishLang.Text = student.PolishLang;
                tbForeignLang.Text = student.ForeignLang;
                tbTechnology.Text = student.Technology;

                Text = "Edycja Ucznia";
                StartPosition = FormStartPosition.CenterScreen;
       

            }
            else
            {
                this.Text = "Dodawanie Ucznia";
            }
            
            tbFirstName.Select();
        }



        // wspólna logika przycisku zatwierdź
        // pobieramy jeszcze raz Listę wszystkich uczniów
        // jeżeli edytujamy, to usuwamy z listy ucznia któego dane będziemy edytować
        // później dodamy tego ucznia i zapiszemy w pliku
        // robimy tak aby było jak najwięcej wspólnej logiki

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var students = _fileHelper.DeserializeFromFile();

            // pole _studentId aby mieć dostęp do parametru przekazanego do konstruktora
            if (_studentId != 0)
            {
                students.RemoveAll(x => x.Id == _studentId);
            }
            else
            {

                // Wykorzystujemy LINQ
                // Sortujemy malejąco
                // zczytujemny pierwszy rekord jeżeli istnieje, jeżeli nie to NULL
                var studentWithHighestId = students
                        .OrderByDescending(x => x.Id).FirstOrDefault();

                _studentId = studentWithHighestId == null ? 1 : studentWithHighestId.Id + 1;
            }            

            // przypisujemy wartości z pól tekstowych do obiektu

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
            Close();
        }


        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
