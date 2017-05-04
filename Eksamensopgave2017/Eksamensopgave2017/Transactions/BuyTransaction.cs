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

        public BuyTransaction(User buyer, decimal amount, Product product) : base(buyer, amount)
        {
            Product = product;
            Execute(buyer,amount);
            
        }

        public override string ToString()
        {
            return
             $"Bought item {Product.ToString()} \nBuoght by {Buyer.ToString()}\nTransaction id: {Id}.  Time: {Date}";
        }

        // TODO implent execute
        public override void Execute(User buyer, decimal amount)
        {
            try
            {
                decimal credit = buyer.Balance;
                credit -= amount;
                if (credit < 0)
                {
                    throw new InsufficentCreditsException();
                }
                else
                {
                    buyer.Balance = credit;
                    TransactionWasSuccesfull = true;
                }

            }
            catch (InsufficentCreditsException e)
            {
                //TODO excemptions
            }
            finally
            {
                TransactionWasSuccesfull = false;
            }
            //TODO insufficentCreditException 

        }

        public void OnUserBalanceWarning(object source, EventArgs args)
        {
            // TODO onUserBalanceWarning
        }
    }
}
