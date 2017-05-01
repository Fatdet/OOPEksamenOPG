using System;
using System.Collections.Generic;

namespace Eksamensopgave2017
{
    public class Stregsystem : IStregsystem
    {
        public IEnumerable<Product> ActiveProducts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        //public delegate void UserBalanceNotification(object obj, EventArgs args);

        public event UserBalanceNotification UserBalanceWarning;

        protected virtual void OnUserBalanceWarning()
        {
            if (UserBalanceWarning != null)
            {
                UserBalanceWarning(this, EventArgs.Empty);
            }
            
        }
        

        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            throw new NotImplementedException();
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByID(int productID)
        {
            //Todo implement own exception
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            throw new NotImplementedException();
        }

        public User GetUser(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}