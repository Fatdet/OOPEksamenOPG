using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    //todo mAYBE IMPLEMET AS ABSTRACT
    public class Transaction
    {
        private static int _id = 1;
        public int Id { get; }

        public  User Buyer {get;}

        public DateTime Date { get; }

        public  Decimal Amount { get; }

        public override string ToString()
        {
            return Id.ToString() + Buyer + Amount.ToString() + Date;
        }

        public Transaction(User buyer, decimal amount)
        {
            Id = _id;
            ++_id;
            Date = DateTime.Now;
            Buyer = buyer;
            Amount = amount;
            Execute(buyer, amount);
        }

        public void Execute(User buyer, decimal amount)
        {
            buyer.Balance -= amount;
        }

    }
}
