using System;
using System.Collections.Generic;
using System.Net.Mime;

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
            Environment.Exit(0);
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            throw new NotImplementedException();
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.Clear();
            Console.WriteLine(errorString);
            Console.ReadKey();
        }

        public event StregsystemEvent CommandEntered;

        public void OnCommandEntered(string command)
        {
            if (CommandEntered != null)
            {
                CommandEntered(this, EventArgs.Empty, command);
            }
        }

        public void DisplayUserBalanceWarning()
        {
            
        }
        public void DisplayTransaction(Transaction transaction)
        {
            Console.Clear();
            Console.WriteLine(transaction.ToString());
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

        private void DisplayActiveProducts(IEnumerable<Product> activeProductList)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________________________");
            Console.WriteLine("|  Id  |                        Produkt                     | Pris |");
            foreach (Product product in activeProductList)
            {
                Console.WriteLine($"| {product.Id,4} | {product.Name,51}|{product.Price,6}|");
            }
            Console.WriteLine();
        }
    }
}