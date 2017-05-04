using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Eksamensopgave2017
{
    public class StregsystemController
    {
        private IStregsystem _stregsystem;
        private IStregsystemUI _ui;
        public StregsystemController(IStregsystemUI ui, IStregsystem stregsystem)
        {
            _ui = ui;
            _stregsystem = stregsystem;
            _ui.CommandEntered += ParseCommand;

        }

        public void ParseCommand(object source, EventArgs args, string command)
        {
            command = command.Trim(); // fjerner whitesplace fra start og slut
            if (command.StartsWith(":"))
            {
                AdminCommand(command);
            }
            else
            {
                string[] parameters = command.Split(' ');

                if (parameters.Length == 1)
                {
                    FindUserInfo(parameters[0]);
                }
                else if (parameters.Length == 2)
                {
                    BuyOneItem(parameters);
                }
                else if (parameters.Length == 3)
                {
                    MultibuyItem(parameters);
                }
                else
                {
                    _ui.DisplayTooManyArgumentsError(command);
                }
            }

        }

        private void AdminCommand(string commandparameters)
        {
            string[] parameters = commandparameters.Split(' ');
            string command = parameters[0];

            

            parameters[0] = parameters[0].ToLower();
            Dictionary<string, Action> adminCommands = new Dictionary<string, Action>();

            switch (parameters.Length)
            {
                case 1:
                    adminCommands.Add(":q", () => _ui.Close());
                    adminCommands.Add(":quit", () => _ui.Close());
                    if (adminCommands.ContainsKey(command))
                    {
                        adminCommands[command]();
                    }
                    else
                    {
                        _ui.DisplayAdminCommandNotFoundMessage(commandparameters);
                    }
                    break;
                case 2:
                    int id = int.Parse(parameters[1]);
                    adminCommands.Add(":active", () => SetProductActive(id, true));
                    adminCommands.Add(":deactive", () => SetProductActive(id, false));
                    adminCommands.Add(":crediton", () => SetProductCredit(id, true));
                    adminCommands.Add(":creditoff", () => SetProductCredit(id, false));
                    Product product = _stregsystem.GetProductByID(id);
                    if (product != null)
                    {
                        if (adminCommands.ContainsKey(command))
                        {
                            adminCommands[command]();
                        }
                        else
                        {
                            _ui.DisplayAdminCommandNotFoundMessage(commandparameters);
                        }
                    }
                    else
                    {
                        _ui.DisplayProductNotFound(id.ToString());
                    }
                    break;
                case 3:
                    string userName = parameters[1];
                    int credit = int.Parse(parameters[2]);
                    adminCommands.Add(":addcredit", () => InsertCashTransaction(userName, credit));
                    User user = _stregsystem.GetUserByUsername(userName);
                    if (user != null)
                    {
                        if (adminCommands.ContainsKey(command))
                        {
                            adminCommands[command]();
                        }
                        else
                        {
                            _ui.DisplayAdminCommandNotFoundMessage(commandparameters);
                        }
                    }
                    else
                    {
                        _ui.DisplayUserNotFound(userName);
                    }
                    break;
                default:
                    _ui.DisplayAdminCommandNotFoundMessage(commandparameters);
                    break;
            }
        }

        private void FindUserInfo(string username)
        {
            User user = _stregsystem.GetUserByUsername(username);
            if (user != null)
            {
                _ui.DisplayUserInfo(user);
                // TODO måske implemeter så man kan vælge hvor mange tranaktioner man vil se?
                // maks 10 tranaktioner må vises i følge opg beskrivelsen
                IEnumerable<Transaction> userTransactions = _stregsystem.GetTransactions(user, 10);
                foreach (Transaction transaction in userTransactions)
                {
                    _ui.DisplayTransaction(transaction);
                }
                if (user.Balance < 50)
                {
                    //_ui.displa
                    //TODO implemet userbalance warning
                }
            }
            else
            {
                _ui.DisplayUserNotFound(username);
            }
        }

        private void BuyOneItem(string[] parameters)
        {
            string userName = parameters[0];
            string id = parameters[1];
            if (id.All(c => char.IsDigit(c)))
            {
                User user = _stregsystem.GetUserByUsername(userName);
                if (user != null)
                {
                    Product product = _stregsystem.GetProductByID(int.Parse(id));
                    if (product != null || product.IsActive)
                    {
                        BuyTransaction bt = _stregsystem.BuyProduct(user, product);
                        _ui.DisplayUserBuysProduct(bt);
                    }
                    else
                    {
                        _ui.DisplayProductNotFound(id);
                    }
                }
                else
                {
                    _ui.DisplayUserNotFound(userName);
                }
            }
            else
            {
                _ui.DisplayGeneralError($"Product id was not a number, you wrote: {id}");
            }
        }

        private void MultibuyItem(string[] paramaters)
        {
            string userName = paramaters[0];
            string count = paramaters[1];
            string id = paramaters[2];
            if (count.All(c => char.IsDigit(c)))
            {
                if (id.All(c => char.IsDigit(c)))
                {
                    User user = _stregsystem.GetUserByUsername(userName);
                    if (user != null)
                    {
                        Product product = _stregsystem.GetProductByID(int.Parse(id));
                        if (product != null || product.IsActive)
                        {
                            BuyTransaction bt = _stregsystem.BuyProduct(user, product);
                            _ui.DisplayUserBuysProduct(int.Parse(count),bt);
                            //TODO måske implemeter userbalance warning her
                        }
                        else
                        {
                            _ui.DisplayProductNotFound(id);
                        }
                    }
                    else
                    {
                        _ui.DisplayUserNotFound(userName);
                    }
                }
                else
                {
                    _ui.DisplayGeneralError($"Product id was not a number, you wrote: {id}");
                }
            }
            else
            {
                _ui.DisplayGeneralError($"The count was not a number, you wrote: {count}");
            }


        }
        private void SetProductActive(int id, bool isActive)
        {
            Product product = _stregsystem.GetProductByID(id);
            if (product != null)
            {
                product.IsActive = isActive;
                _stregsystem.UpdateActiveProducts();
            }
            else
            {
                _ui.DisplayProductNotFound(id.ToString());
            }
        }

        private void SetProductCredit(int id, bool onCredit)
        {
            Product product = _stregsystem.GetProductByID(id);
            if (product != null)
            {
                product.CanBeBoughtOnCredit = onCredit;
            }
        }

        private void InsertCashTransaction(string username, int amount)
        {
            User user = _stregsystem.GetUserByUsername(username);
            if (user != null)
            {
                _stregsystem.AddCreditsToAccount(user, amount);
            }
        }

    }
}