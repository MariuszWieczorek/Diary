namespace Diary.Model
{
    public class Teacher : Person
    {
        public string Position { get; set; }

        public string GetTeacherInfo()
        {
            return $"Nauczyciel: {FirstName} {LastName}";
        }

        public override string GetInfo()
        {
            return $"Nauczyciel: {FirstName} {LastName}";
        }
    }
}
