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

            IStregsystem stregsystem = new Stregsystem();
            IStregsystemUI ui = new StregsystemCLI(stregsystem);

            Console.ReadKey();
        }
    }
}
