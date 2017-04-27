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


            User a = new User("frederik","schrøder","fatdet","asd@asd.c");


            Console.WriteLine(a.UserID);
            Console.WriteLine(a.FirstName);
            Console.WriteLine(a.LastName);
            Console.WriteLine(a.Email);
            Console.WriteLine(a.UserName);
            Console.WriteLine(a.GetHashCode());

            SeasonalProduct b = new SeasonalProduct("test", 25,false,false,"26-12-2017","17-12-2017" );
           
            
            Console.ReadKey();
        }
    }
}
