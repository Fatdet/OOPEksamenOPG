using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    public class Product
    {
        public int Id { get; }
        public string Name { get; private set; }
        public decimal Price { get; private set ; }
        public  bool IsActive { get; private set ; }
        public bool CanBeBoughtOnCredit { get; set; }

        private bool SetName(string name)
        {
            if (name == null)
            {
                return false;
            }
            else
            {
                Name = name;
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
                Price = price;
                return true;
            }
        }

        public override string ToString()
        {
            string template = "Product ID: {0}, name: {1} and price: {2}";
            return string.Format(template, Id, Name, Price);
        }

        public Product(int id, string name, decimal price, bool isActive, bool canBeBoughtOnCredit)
        {

            SetName(name);
            SetPrice(price);
            IsActive = isActive;
            CanBeBoughtOnCredit = canBeBoughtOnCredit;
            Id = id;
           
        }
    }
}
