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

        private string _filePath = Path.Combine(Environment.CurrentDirectory, "students.txt");

     
       // przekazujemy parametr domyślny
        public AddEditStudent(int id = 0)
        {
            InitializeComponent();
            if (id != 0)
            {
                var students = DeserializeFromFile();
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
            }
        }

        // pobieramy jeszcze raz Listę

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var students = DeserializeFromFile();
            
            // Wykorzystujemy LINQ
            // Sortujemy malejąco
            // zczytujemny pierwszy rekord jeżeli istnieje, jeżeli nie to NULL
            var studentWithHighestId = students
                    .OrderByDescending(x => x.Id).FirstOrDefault();

            //var studentId = 0;

            //if (studentWithHighestId == null)
            //{
            //    studentId = 1;
            //}
            //else
            //{
            //    studentId = studentWithHighestId.Id + 1;
            //}

            // równorzędny zapis
            var studentId = studentWithHighestId == null ? 1 : studentWithHighestId.Id + 1;

            // przypisujemy wartości z pól tekstowych do obiektu

            var student = new Student
            {
                Id = studentId,
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
            SerializeToFile2(students);
            Close();
        }


        // Zapisujemy Listę studentów do pliku wersja z użyciem USING
        public void SerializeToFile2(List<Student> students)
        {
            // przekazujemy listę obiektów typu Student
            // typeof() - zwróci typ podczas kompilacji
            // The typeof is an operator keyword which is used to get a type at the compile-time.
            var serializer = new XmlSerializer(typeof(List<Student>));
            StreamWriter streamWriter = null;

            // jeżeli w using jest deklaracja jakiegoś obiektu
            // to zawsze na tym obiekcie zostanie automatycznie wywołana metoda Dispose
            using (streamWriter = new StreamWriter(_filePath))
            {
                // stream jest to klasa, która zapewnia nam transfer bajtów
                serializer.Serialize(streamWriter, students);
                streamWriter.Close();
            }
        }

        // Odczytuje Listę obiektów z pliku w tym przypadku z XML'a
        public List<Student> DeserializeFromFile()
        {
            if (!File.Exists(_filePath))
            {
                MessageBox.Show("Brak pliku");
                return new List<Student>();
            }

            var serializer = new XmlSerializer(typeof(List<Student>));

            using (var streamReader = new StreamReader(_filePath))
            {
                // stream jest to klasa, która zapewnia nam transfer bajtów
                // Deserializer zwraca typ obiekt, musimy go rzutować na listę studentów
                var students = (List<Student>)serializer.Deserialize(streamReader);
                streamReader.Close();
                return students;
            }

        }
    }
}
