using System;
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

            if (parameters.Length == 2)
            {
                string userName = parameters[0];
                string id = parameters[1];
                if (!id.All(c => char.IsDigit(c)))
                {
                    User user = _stregsystem.GetUserByUsername(userName);
                    if (user != null)
                    {
                        Product product = _stregsystem.GetProductByID(int.Parse(id));
                        if (product != null)
                        {
                            _stregsystem.BuyProduct(user, product);
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
                    _ui.DisplayTooManyArgumentsError(command);
                }
            }
            else if (parameters.Length == 3)
            {
                //todo implement
            }
            else
            {
                _ui.DisplayTooManyArgumentsError(command);
            }
        }
    }
}