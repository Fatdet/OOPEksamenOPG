//20167026_Frederik_Valdemar_Schrøder

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    class Program
    {
        static void Main(string[] args)
        {
            /*IStregsystem stregsystem = new Stregsystem();
            IStregsystemUI ui = new StregsystemCLI(stregsystem);
            StregsystemController sc = new StregsystemController(ui, stregsystem);

            //ui.Start();*/


            User a = new User();
            a.SetFirstName("Frederik");
            a.SetLastName("Schrøder");
            a.SetUserName("Fatdet");
            a.SetEmail("Frederikhtxq12@gmail.com");

            Console.WriteLine(a.UserID);
            Console.WriteLine(a.FirstName);
            Console.WriteLine(a.LastName);
            Console.WriteLine(a.Email);
            Console.WriteLine(a.UserName);
            Console.WriteLine(a.GetHashCode());

            Console.ReadKey();
        }
    }
}
