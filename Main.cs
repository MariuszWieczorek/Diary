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
using System.Threading;
using Diary.Properties;
using Diary.Model;

namespace Diary
{
    public partial class Main : Form
    {

        private delegate void DisplayMessage(string message);
        private FileHelper<List<Student>> _fileHelper = 
            new FileHelper<List<Student>>(Program.FilePath);
        
        public List<string> GroupOfStudentsNames = Groups.GroupOfStudents.Select(x => x.Nazwa).ToList();

        public bool isMaximize 
        { get
            {
                return Settings.Default.isMaximize;
            }
          set
            {
                Settings.Default.isMaximize = value;
            }
        }

        public Main()
        {
          
            InitializeComponent();
             Text = "Dziennik Ucznia";

            cboGroupOfStudent.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGroupOfStudent.DataSource = Groups.GroupOfStudents;
            cboGroupOfStudent.DisplayMember = "Nazwa";
            cboGroupOfStudent.ValueMember = "Id";
            cboGroupOfStudent.Visible = false;

            if (isMaximize)
            {
                WindowState = FormWindowState.Maximized;
            }
            
            //PopulateStudents();
            //DeserializeAndShowStudents();
            
            RefreshDiary();
            SetColumnHeader();
            SetColumnProperities();
            
            // Other.TestLinq();
            // Other.TestInheritance();
            
            // wykorzystanie kompozycji
            // var student = new Student();
            // student.Address.City = "Szczecin";

            //var messages1 = new DisplayMessage(DisplayMessage1);
            //var messages2 = new Action<string>(DisplayMessage2);
            //messages1("aaaa");
            //MetodUseDelegate2(messages2);

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






        private void RefreshDiary()
        {
            var students = _fileHelper.DeserializeFromFile();
            var nameOfStudentGroup = cboGroupOfStudent.Items[cboGroupOfStudent.SelectedIndex].ToString();
            
            var selectedGroupId = (cboGroupOfStudent.SelectedItem as GroupOfStudent).Id;


            var studentsQueryable = students
                .OrderBy(x => x.Id)
                .Select(x => new 
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Math = x.Math,
                    Physics = x.Physics,
                    Technology = x.Technology,
                    PolishLang = x.PolishLang,
                    ForeignLang = x.ForeignLang,
                    Comments = x.Comments,
                    AdditionalClasses = x.AdditionalClasses,
                    GroupOfStudentsId = x.GroupOfStudentsId,
                    GroupOfStudents = x.GroupOfStudents,
                }).AsQueryable();


            var studentsQueryable2 = students
            .Join(Groups.GroupOfStudents, left => left.GroupOfStudentsId, right => right.Id,
               (left, right) => new { StudentColumns = left, GroupsColumns = right })
            .Select(x => new 
            { 
                Id = x.StudentColumns.Id,
                FirstName = x.StudentColumns.FirstName,
                Math = x.StudentColumns.Math,
                Physics = x.StudentColumns.Physics,
                Technology = x.StudentColumns.Technology,
                PolishLang = x.StudentColumns.PolishLang,
                ForeignLang = x.StudentColumns.ForeignLang,
                Comments = x.StudentColumns.Comments,
                AdditionalClasses = x.StudentColumns.AdditionalClasses,
                GroupOfStudentsId = x.StudentColumns.GroupOfStudentsId,
                GroupOfStudents = x.GroupsColumns.Nazwa,

            }).AsQueryable();

            // .Where(x => x.GroupOfStudents == nameOfStudent || !chkGroupOfStudents.Checked)


            if (chkGroupOfStudents.Checked)
            {
              // studentsQueryable = studentsQueryable.Where(x => x.GroupOfStudents == nameOfStudentGroup);
                 studentsQueryable = studentsQueryable.Where(x => x.GroupOfStudentsId == selectedGroupId);
            }

            dgvDiary.DataSource = studentsQueryable.ToList();
        }

        /// <summary>
        /// Ustawienie tytułów kolumn oraz przypisanie pól do kolumn w DataGridView
        /// </summary>
        private void SetColumnHeader()
        {
            dgvDiary.Columns[nameof(Student.Id)].DisplayIndex = 0;
            dgvDiary.Columns[nameof(Student.FirstName)].DisplayIndex = 1;
            dgvDiary.Columns[nameof(Student.LastName)].DisplayIndex = 2;
            dgvDiary.Columns[nameof(Student.Comments)].DisplayIndex = 3;
            dgvDiary.Columns[nameof(Student.Math)].DisplayIndex = 4;
            dgvDiary.Columns[nameof(Student.Physics)].DisplayIndex = 5;
            dgvDiary.Columns[nameof(Student.Technology)].DisplayIndex = 6;
            dgvDiary.Columns[nameof(Student.PolishLang)].DisplayIndex = 7;
            dgvDiary.Columns[nameof(Student.ForeignLang)].DisplayIndex = 8;
            dgvDiary.Columns[nameof(Student.AdditionalClasses)].DisplayIndex = 9;
            dgvDiary.Columns[nameof(Student.GroupOfStudentsId)].DisplayIndex = 10;
            dgvDiary.Columns[nameof(Student.GroupOfStudents)].DisplayIndex = 11;

            dgvDiary.Columns[0].HeaderText = "Numer";
            dgvDiary.Columns[1].HeaderText = "Imię";
            dgvDiary.Columns[2].HeaderText = "Nazwisko";
            dgvDiary.Columns[3].HeaderText = "Komentarz";
            dgvDiary.Columns[4].HeaderText = "Matematyka";
            dgvDiary.Columns[5].HeaderText = "Fizyka";
            dgvDiary.Columns[6].HeaderText = "Technologia";
            dgvDiary.Columns[7].HeaderText = "Język Polski";
            dgvDiary.Columns[8].HeaderText = "Język Obcy";
            dgvDiary.Columns[9].HeaderText = "Dodatkowe Kursy";
            dgvDiary.Columns[10].HeaderText = "Id Grupy";
            dgvDiary.Columns[11].HeaderText = "Grupa Studentów";
            


        }

        /// <summary>
        /// Ustawienie cech DataGridView
        /// </summary>
        private void SetColumnProperities()
        {
            dgvDiary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDiary.RowHeadersVisible = false;
            dgvDiary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDiary.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        /// <summary>
        /// wypełnienie listy studentów przykładowymi danymi
        /// </summary>        
        public void PopulateStudents()
        {
            var students = new List<Student>();
            // w tym przypadku może też być z nawiasami po new Student() lub bez 
            students.Add(new Student { Id = 1, FirstName = "Jan", LastName = "Kowalski", Address = new Address { City = "Szczecin" } });
            students.Add(new Student { Id = 2, FirstName = "Jan", LastName = "Nowak" });
            students.Add(new Student { Id = 3, FirstName = "Alfred", LastName = "Kowalski" });
            students.Add(new Student { Id = 4, FirstName = "Joanna", LastName = "Bartkowiak" });
            _fileHelper.SerializeToFile(students);
        }


        public void DeserializeAndShowStudents()
        {
            var students = _fileHelper.DeserializeFromFile();
            foreach (var item in students)
            {
                MessageBox.Show($"{item.FirstName} {item.LastName}");
            }
        }


        /// <summary>
        /// Dodawanie Ucznia do Listy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // okno jest zwykłą klasą więc tworzymy jego instancję
            var addEditStudent = new AddEditStudent();

            // event krok 6:
            // subskrybujemy zdarzenie - do zdarzenia zdefiniowanego w oknie w którym ono wystąpi
            // przypisujemy metodę ma się uruchomić w momencie wystąpienia zdarzenia
            addEditStudent.StudentAddedEvent += AddEditStudent_StudentAdd;

            addEditStudent.FormClosing += AddEditStudent_FormClosing;

            // okno w którym wystąpi zdarzenie
            addEditStudent.ShowDialog();

            // event krok 7:
            // dobrą praktyką jest odsubskryptowanie się od zdarzenia
            addEditStudent.StudentAddedEvent -= AddEditStudent_StudentAdd;
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
            int id = Convert.ToInt32(dgvDiary.SelectedRows[0].Cells[0].Value);
            int rowIndex = dgvDiary.CurrentCell.RowIndex;
            var addEditStudent = new AddEditStudent(id);
            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            addEditStudent.ShowDialog();
            addEditStudent.FormClosing -= AddEditStudent_FormClosing;
            if (dgvDiary.RowCount >= (rowIndex + 1) )
                dgvDiary.CurrentCell = dgvDiary.Rows[rowIndex].Cells[0]; //czyli wiersz z indexem id
            
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
            _fileHelper.SerializeToFile(students);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiary();
        }


        private void AddEditStudent_FormClosing(object sender, FormClosingEventArgs e)
        {
            // MessageBox.Show("AddEditStudent_FormClosing");
            RefreshDiary();
        }
                
        // event krok 5:
        // definicja metody, którą za chwilę przypiszemy do zdarzenia 
        private void AddEditStudent_StudentAdd()
        {
            // MessageBox.Show("AddEditStudent_StudentAdd()");
            RefreshDiary();
        }

        /// <summary>
        /// Zapamiętanie ustawienia okna przy zamknięciu aplikacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                Settings.Default.isMaximize = true;
            else
                Settings.Default.isMaximize = false;

            Settings.Default.Save();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void chkGroupOfStudents_Click(object sender, EventArgs e)
        {
            if (chkGroupOfStudents.Checked)
                cboGroupOfStudent.Visible = true;
            else
                cboGroupOfStudent.Visible = false;
            
            RefreshDiary();
        }

        private void cboGroupOfStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDiary();
        }
    }
}
