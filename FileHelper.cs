using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;


// umieszczamy w tej klasie uniwersalne metody do serializacji i deserializacji aby nie powielać ich kodu
// przerabiamy tę klasę na generyczną aby nadawała się nie tylko do obiektów klasy List<Student>
// w tym celi do nazwy klasy dodajemy <T>
// i każde wystąpienie List<Student> zastępujemy T
// aby móc zapisaćlinię kodu: return new T();
// musimy zawęzić tryp generyczny do klas, które mogą miećpusty konstruktor
// FileHelper<T> where T : new()
namespace Diary
{
    public class FileHelper<T> where T : new()
    {
        private string _filePath;

        public FileHelper(string filePath)
        {
            _filePath = filePath;
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

        public void SerializeToFile1(T students)
        {
            // Zapisujemy Listę studentów do pliku wersja z użyciem TRY ... CATCH    
            // przekazujemy listę obiektów typu Student
            // typeof - zwróci typ podczas kompilacji
            // The typeof is an operator keyword which is used to get a type at the compile-time.

            var serializer = new XmlSerializer(typeof(T));
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


        public void SerializeToFile2(T students)
        {
            // Zapisujemy Listę studentów do pliku wersja z użyciem USING
            // przekazujemy listę obiektów typu Student
            // typeof() - zwróci typ podczas kompilacji
            // The typeof is an operator keyword which is used to get a type at the compile-time.

            var serializer = new XmlSerializer(typeof(T));
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
        public T DeserializeFromFile()
        {
            if (!File.Exists(_filePath))
            {
                MessageBox.Show("Brak pliku");
                return new T();
            }

            var serializer = new XmlSerializer(typeof(T));

            using (var streamReader = new StreamReader(_filePath))
            {
                // stream jest to klasa, która zapewnia nam transfer bajtów
                // Deserializer zwraca typ obiekt, musimy go rzutować na listę studentów
                var students = (T)serializer.Deserialize(streamReader);
                streamReader.Close();
                return students;
            }

        }
    }
}
