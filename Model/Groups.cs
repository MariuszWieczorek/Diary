using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Model
{
    public static class Groups
    {
        public static List<GroupOfStudent> GroupOfStudents = new List<GroupOfStudent>
        {
           new GroupOfStudent { Id = 0, Nazwa = "Brak Grupy" },
           new GroupOfStudent { Id = 1, Nazwa = "Klasa 1A" },
           new GroupOfStudent { Id = 2, Nazwa = "Klasa 1B" },
           new GroupOfStudent { Id = 3, Nazwa = "Klasa 2A" },
           new GroupOfStudent { Id = 4, Nazwa = "Klasa 2B" },
           new GroupOfStudent { Id = 5, Nazwa = "Klasa 3A" },
           new GroupOfStudent { Id = 6, Nazwa = "Klasa 3B" },
        };
    }
}
