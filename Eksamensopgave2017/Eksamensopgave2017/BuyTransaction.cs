using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    public class BuyTransaction : Transaction
    {
        private const string _toBuy = "Bought item";
        public Product Product { get; }

        public BuyTransaction(User buyer, decimal amount, Product product) : base(buyer, amount)
        {
            Product = product;
        }

        public override string ToString()
        {
            return _toBuy + Product + Id + Buyer + Date;
        }

        // TODO implent execute
    }
}
