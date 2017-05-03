using System;
using System.Collections.Generic;

namespace Eksamensopgave2017
{
    class StregsystemCLI : IStregsystemUI
    {
        private bool isRunning;
        private IStregsystem _stregsystem;
        public StregsystemCLI(IStregsystem stregsystem)
        {
            _stregsystem = stregsystem;
            isRunning = true;
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
            Console.WriteLine($"Product id: {product} kunne ikke findes i eksisterende produkter.");
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
            Console.ReadKey();

        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.Clear();
            Console.WriteLine($"{adminCommand} not found.");
            Console.ReadKey();
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.Clear();
            Console.WriteLine(transaction.ToString());
            Console.ReadKey();

        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
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

        public void OnCommandEntered(string command)
        {
            if (CommandEntered != null)
            {
                CommandEntered(this, EventArgs.Empty, command);
            }
        }

        public void Start()
        {

            while (isRunning)
            {
                DisplayActiveProducts(_stregsystem.ActiveProducts);
                OnCommandEntered(HandleInput());
            }
        }

        private string HandleInput()
        {
            Console.WriteLine("INput somethang biatch:");
            return Console.ReadLine();
        }

        private void DisplayActiveProducts(IEnumerable<Product> productList)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________________________");
            Console.WriteLine("|  Id  |                        Produkt                     | Pris |");
            foreach (Product product in productList)
            {
                Console.WriteLine(string.Format("| {0,4} | {1,51}|{2,6}|",product.Id, product.Name, product.Price));
            }
            Console.WriteLine();
        }
    }
}