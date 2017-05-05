using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    class UserAlreadyExistException : Exception
    {
        public UserAlreadyExistException() : base("UserName already exist.")
        {
        }

        public UserAlreadyExistException(string message) : base(message)
        {
            
        }
    }
}
