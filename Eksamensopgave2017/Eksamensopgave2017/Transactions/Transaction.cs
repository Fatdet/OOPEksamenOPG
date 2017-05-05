using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    public abstract class Transaction
    {
        private static int _id = 1;
        public int Id { get; }

        public  User Buyer {get;}

        public DateTime Date { get; }

        public  Decimal Amount { get; }

        public override string ToString()
        {
            return $"Id: {Id}, transaction was made by: {Buyer} \nAmount: {Amount} Transaction Time: {Date}";
        }

        public abstract void Execute(User buyer, decimal amount);
        protected Transaction(User buyer, decimal amount)
        {
            Id = _id;
            ++_id;
            Date = DateTime.Now;
            Buyer = buyer;
            Amount = amount;
        }
    }
}
