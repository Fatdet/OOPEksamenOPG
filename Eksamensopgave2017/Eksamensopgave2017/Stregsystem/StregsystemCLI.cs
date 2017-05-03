using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace Eksamensopgave2017
{
    class StregsystemCLI : IStregsystemUI
    {
        // TODO might now be nessarry
        private IStregsystem _stregsystem;
        public StregsystemCLI(IStregsystem stregsystem)
        {
            _stregsystem = stregsystem;
            Start();
        }

        public void DisplayUserNotFound(string username)
        {
            Console.Clear();
            Console.WriteLine($"{username} kunne ikke findes i eksisterende brugere.");
            Console.ReadKey();

        }

        public void DisplayProductNotFound(string product)
        {
            Console.Clear();
            Console.WriteLine($"{product} kunne ikke findes i eksisterende produkter.");
            Console.ReadKey();
        }

        public void DisplayUserInfo(User user)
        {
            Console.Clear();
            Console.WriteLine(user.ToString());
            Console.ReadKey();
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Console.Clear();
            Console.WriteLine("to many arguments");
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.Clear();
            Console.WriteLine($"{adminCommand} not found.");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.Clear();
            Console.WriteLine(transaction.ToString());
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            Environment.Exit(0);
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            throw new NotImplementedException();
        }

        public void DisplayGeneralError(string errorString)
        {
            throw new NotImplementedException();
        }

        public event StregsystemEvent CommandEntered;

        public void Start()
        {
            ListActiveProducts(_stregsystem.ActiveProducts);
        }

        private void ListActiveProducts(IEnumerable<Product> productList)
        {
            Console.WriteLine     (" __________________________________");
            Console.WriteLine("\n\n | Id |         Produkt     | Pris  |");
            foreach (Product product in productList)
            {
                Console.WriteLine(string.Format("{0} | {1}|{2}|",product.Id, product.Name, product.Price));
            }
            Console.WriteLine();
        }
    }
}