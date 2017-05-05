using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    public class BuyTransaction : Transaction
    {
        public bool TransactionWasSuccesfull;
        public Product Product { get; }

        public override string ToString()
        {
            return
             $"id: {Id} Bought item {Product.ToString()} \nBuoght by {Buyer.ToString()}\nTransaction Time: {Date}\n\n";
        }

        public override void Execute(User buyer, decimal amount)
        {
            decimal credit = buyer.Balance;
            credit -= amount;
            if (credit < 0 && Product.CanBeBoughtOnCredit == false)
            {
                throw new InsufficentCreditsException();
            }
            else
            {
                buyer.Balance = credit;
                TransactionWasSuccesfull = true;
            }
        }


        public BuyTransaction(User buyer, decimal amount, Product product) : base(buyer, amount)
        {
            Product = product;
            Execute(buyer, amount);

        }
    }
}
