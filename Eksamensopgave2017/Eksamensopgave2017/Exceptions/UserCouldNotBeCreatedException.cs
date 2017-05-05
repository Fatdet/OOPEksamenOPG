using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    class UserCouldNotBeCreatedException :Exception
    {
        public UserCouldNotBeCreatedException() : base("Some input was not valid.")
        {
            
        }
    }
}
