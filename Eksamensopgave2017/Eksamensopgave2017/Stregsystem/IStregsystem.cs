﻿using System;
using System.Collections.Generic;

namespace Eksamensopgave2017
{
    public interface IStregsystem
    {
        
        IEnumerable<Product> ActiveProducts { get; }
        InsertCashTransaction AddCreditsToAccount(User user, int amount);
        BuyTransaction BuyProduct(User user, Product product);
        Product GetProductByID(int productID);
        IEnumerable<Transaction> GetTransactions(User user, int count);
        IEnumerable<User> GetUsers(Func<User, bool> predicate);
        User GetUserByUsername(string username);
        event UserBalanceNotification UserBalanceWarning;
        
    }

    public delegate void UserBalanceNotification(object sender, EventArgs args);
}