using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    public class InsertCashTransaction : Transaction
    {
        private const string _cashIn = "Cash In";
        public InsertCashTransaction(User buyer, decimal amount) : base(buyer, amount)
        {
            Execute(buyer, amount);
        }

        public override string ToString()
        {
            return $"id: {Id} Added {Amount} credits to user{Buyer} \nTransaction Time: {Date}";
        }

        public override void Execute(User buyer, decimal amount)
        {
            buyer.Balance += amount;
        }
    }
}
