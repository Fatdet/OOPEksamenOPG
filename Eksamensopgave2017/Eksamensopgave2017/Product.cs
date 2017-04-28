using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    public class Product
    {
        private static int _id = 1;
        public int Id { get; }
        public string Name { get; private set; }
        public decimal Price { get; private set ; }
        public  bool IsActive { get; set; }
        public bool CanBeBoughtOnCredit { get; set; }

        private bool SetName(string name)
        {
            if (name == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool SetPrice(decimal price)
        {
            if (price < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override string ToString()
        {
            return Id + Name + Price;
        }

        public Product(string name, decimal price, bool isActive, bool canBeBoughtOnCredit)
        {
            SetName(name);
            Id = _id;
            ++_id;
        }
    }
}
