using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diary
{
    static class Program
    {

        // dodajemy zmienną globalną widoczną w całym projekcie
        // $@"{Environment.CurrentDirectory}\students.txt"

        public static string FilePath = Path.Combine(Environment.CurrentDirectory, "students.txt");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
