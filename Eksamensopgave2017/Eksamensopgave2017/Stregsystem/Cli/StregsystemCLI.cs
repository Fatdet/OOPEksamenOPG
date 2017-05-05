using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace Eksamensopgave2017
{
    class StregsystemCLI : IStregsystemUI
    {
        private bool isRunning;
        private IStregsystem _stregsystem;

        public event StregsystemEvent CommandEntered;

        public void OnCommandEntered(string command)
        {
            if (CommandEntered != null)
            {
                CommandEntered(this, EventArgs.Empty, command);
            }
        }

        #region Not Found errors
        public void DisplayUserNotFound(string username)
        {
            Console.Clear();
            Console.WriteLine($"{username} kunne ikke findes i eksisterende brugere.");
            Console.ReadKey();

        }

        public void DisplayProductNotFound(string product)
        {
            Console.Clear();
            Console.WriteLine($"Product id: {product} kunne ikke findes blandt akive produkter.");
            Console.ReadKey();
        }
        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.Clear();
            Console.WriteLine($"{adminCommand} not found.");
            Console.ReadKey();
        }

        #endregion

        #region Display user action

        public void DisplayUserInfo(User user)
        {
            Console.Clear();
            Console.WriteLine(user.ToString());
            Console.ReadKey();
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.Clear();
            Console.WriteLine("Buy transation:");
            Console.WriteLine(transaction.ToString());
            Console.ReadKey();

        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.Clear();

            Console.WriteLine($"Buy transaction. nr {count}");
            Console.WriteLine(transaction.ToString());

            Console.ReadKey();

        }
        public void DisplayTransaction(Transaction transaction)
        {
            Console.Clear();
            Console.WriteLine(transaction.ToString());
            Console.ReadKey();
        }
        #endregion

        #region Display errors
        public void DisplayTooManyArgumentsError(string command)
        {
            Console.Clear();
            Console.WriteLine($"Command: {command} had to many arguments.");
            Console.ReadKey();

        }
        public void DisplayGeneralError(string errorString)
        {
            Console.Clear();
            Console.WriteLine(errorString);
            Console.ReadKey();
        }

        #endregion

        #region display warnings
        public void DisplayInsufficientCash(User user, Product product)
        {
            Console.Clear();
            Console.WriteLine($"User {user.UserName} has insufficent cash to buy {product.ToString()}");
            Console.ReadKey();

        }
        public void DisplayUserBalanceWarning(User user, decimal balance)
        {
            Console.Clear();
            Console.WriteLine("User balance notification!");
            Console.WriteLine($"User {user.UserName}'s balance is lower than 50 \nBalance is {balance}");
            Console.ReadKey();

        }

        #endregion

        public void ShowHelp()
        {
            Console.Clear();
            Console.WriteLine("Avaliable Commands: \n" +
                              "buy stuff: [username] [product_id]\n" +
                              "buy multiple stuff: [username] [amount] [prodcut_id]\n"+
                              ":q , :quit => To quit \n" +
                              ":activate, :deactivate => :'activate [prodct_id]' to activate/deactivate product\n" +
                              ":crediton, :cretioff => 'credition [product_id]' to turn buy on credit on/off \n" +
                              ":addcredits => ':addcredits [username] [amount]' adds credit to a user");
            Console.ReadKey();
        }
        public void Close()
        {
            Environment.Exit(0);
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
            Console.WriteLine("Awaiting user input:");
            return Console.ReadLine();
        }

        private void DisplayActiveProducts(IEnumerable<Product> activeProductList)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________________________");
            Console.WriteLine("|  Id  |                        Produkt                     | Pris |");
            foreach (Product product in activeProductList)
            {
                Console.WriteLine($"| {product.Id,4} | {product.Name,-51}|{product.Price,6}|");
            }
            Console.WriteLine("|__________________________________________________________________|");
        }
        public StregsystemCLI(IStregsystem stregsystem)
        {
            _stregsystem = stregsystem;
            isRunning = true;
            _stregsystem.UserBalanceWarning += DisplayUserBalanceWarning;

        }
    }
}