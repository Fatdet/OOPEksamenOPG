using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Eksamensopgave2017
{
    class StregsystemController
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
                    if (product != null)
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
                        if (product != null)
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
    }
}