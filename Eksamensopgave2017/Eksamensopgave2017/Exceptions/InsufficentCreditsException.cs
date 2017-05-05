using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    public class InsufficentCreditsException : Exception
    {
        #region Constructors
        public InsufficentCreditsException() : base("lol you are too poor to buy this xD")
        {
            
        }

        public InsufficentCreditsException(string message) : base(message)
        {
            
        }

        public InsufficentCreditsException(string message, Exception innerException) : base(message, innerException)
        {
            
        }

        public InsufficentCreditsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            
        }
#endregion
    }
}
