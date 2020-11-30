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

        // private string _filePath = $@"{Environment.CurrentDirectory}\students.txt";
        // lub możemy użyć klasy combine, który sam sklei ścieżkę
        private string _filePath = Path.Combine(Environment.CurrentDirectory,"students.txt");


        public Main()
        {
            InitializeComponent();
            //PopulateStudents();
            //DeserializeAndShowStudents();

        }

        // Zapisujemy Listę studentów do pliku wersja z użyciem TRY ... CATCH
        public void SerializeToFile1(List<Student> students)
        {
            
            // przekazujemy listę obiektów typu Student
            // typeof - zwróci typ podczas kompilacji
            // The typeof is an operator keyword which is used to get a type at the compile-time.
            var serializer = new XmlSerializer(typeof(List<Student>));
            StreamWriter streamWriter = null;

            try
            {
            streamWriter = new StreamWriter(_filePath);

            // stream jest to klasa, która zapewnia nam transfer bajtów
            serializer.Serialize(streamWriter, students);
            streamWriter.Close();
            }
            finally
            {
            
            // Obiekty typu stream trzeba ręcznie usunąć z pamięci
            streamWriter.Dispose();
            }
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

        // Różne metody zapisujące i odczytujące z pliku
        public void ReadFromFile()
        {
            string filePath = $@"{Path.GetDirectoryName(Application.ExecutablePath)}\..\..\NowyPlik2.txt";
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            // File.Delete(filePath);
            // Obydwie poniższe metody tworzą plik jeżeli gop jeszcze nie ma
            // nie trzebw więc używać Create
            File.WriteAllText(filePath, "Zostań programistą .Net");
            File.AppendAllText(filePath, "\nZostań programistą .Net");
            var text = File.ReadAllText(filePath);
            MessageBox.Show(text);
        }
        
        // wypełnienie listy studentów przykłądowymi danymi
        public void PopulateStudents()
        {
            var students = new List<Student>();
            // w tym przypadku może też być z nawiasami po new Student() lub bez 
            students.Add(new Student() { Id = 1, FirstName = "Jan", LastName = "Kowalski" });
            students.Add(new Student { Id = 1, FirstName = "Jan", LastName = "Nowak" });
            students.Add(new Student { Id = 1, FirstName = "Alfred", LastName = "Kowalski" });
            students.Add(new Student { Id = 1, FirstName = "Joanna", LastName = "Bartkowiak" });
            SerializeToFile2(students);
        }

        public void DeserializeAndShowStudents()
        {
            var students = DeserializeFromFile();
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

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }
    
    
    }
}
