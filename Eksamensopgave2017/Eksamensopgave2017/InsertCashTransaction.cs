using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    class InsertCashTransaction : Transaction
    {
        private const string _cashIn = "Cash In";
        public InsertCashTransaction(User buyer, decimal amount) : base(buyer, amount)
        {
        }

        public override string ToString()
        {
            return _cashIn + Id + Buyer + Amount.ToString() + Date;
        }
        // TODO Implemt execute
    }
}
