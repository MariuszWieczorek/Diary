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

      
        private FileHelper<List<Student>> _fileHelper = 
            new FileHelper<List<Student>>(Program.FilePath);

        public Main()
        {
            InitializeComponent();
            //PopulateStudents();
            //DeserializeAndShowStudents();

            RefreshDiary();
            SetColumnHeader();
            SetColumnProperities();



            this.Text = "Dziennik Ucznia";
        }

        private void RefreshDiary()
        {
            var students = _fileHelper.DeserializeFromFile();
            dgvDiary.DataSource = students;
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
    
    
    }
}
