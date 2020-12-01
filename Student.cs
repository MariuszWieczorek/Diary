using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{

    // Uwaga w realnej aplikacji każda klasa w osobnym pliku
     

   // typ bazowy, nadtyp
    public abstract class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comments { get; set; }

        public abstract string GetInfo();
        
    }


    // typ pochodny, podtyp
    public class Student : Person
    {
        public string Math { get; set; }
        public string Physics { get; set; }
        public string Technology { get; set; }
        public string PolishLang { get; set; }
        public string ForeignLang { get; set; }

        public string GetStudentInfo()
        {
            return $"Student: {FirstName} {LastName} oceny z matematyki {Math}";
        }

        public override string GetInfo()
        {
            return $"Student: {FirstName} {LastName} oceny z matematyki {Math}";
        }
    }

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
