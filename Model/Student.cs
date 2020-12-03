namespace Diary.Model
{

    // typ pochodny, podtyp
    public class Student : Person
    {
 
        public string Math { get; set; }
        public string Physics { get; set; }
        public string Technology { get; set; }
        public string PolishLang { get; set; }
        public string ForeignLang { get; set; }
        public bool AdditionalClasses { get; set; }
        public string GroupOfStudents { get; set; }
        public int GroupOfStudentsId { get; set; }

        public string GetStudentInfo()
        {
            return $"Student: {FirstName} {LastName} oceny z matematyki {Math}";
        }

        public override string GetInfo()
        {
            return $"Student: {FirstName} {LastName} oceny z matematyki {Math}";
        }

    }

}
