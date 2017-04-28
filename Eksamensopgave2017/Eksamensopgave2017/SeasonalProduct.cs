using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{

    public class SeasonalProduct : Product
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        private bool SetDate(string startDate, DateTime date)
        {
            string[] dateSubstring = startDate.Split('-', '/');
            // Checks how many parts the string is split into

            if (dateSubstring.Length != 3)
            {
                return false;
            }
            // Checks that it can only be digits
            foreach (var subString in dateSubstring)
            {
                if (!subString.All(c => Char.IsDigit(c)))
                {
                    return false;
                }
            }
            // Converts to ints
            int[] dates = new int[3];
            for (int i = 0; i < 3; i++)
            {
                dates[i] = int.Parse(dateSubstring[i]);
            }
            // Checks that the dates are valid
            if (dates[0] < 1 || dates[0] > 31)
            {
                //TODO better date checking
                return false;
            }
            if (dates[1] < 1 || dates[1] > 12)
            {
                return false;
            }
            date = new DateTime(dates[2], dates[1], dates[0]);
            // Checks if the end date is after the start date
            if (StartDate != null)
            {
                if (date.CompareTo(StartDate) != 1)
                {
                    return false;
                }
            }

            return true;
        }
        // Maybe override Active to be more specific
        /// <summary>
        ///  hallo
        /// </summary>
        /// <param name="name"> Products name</param>
        /// <param name="price"></param>
        /// <param name="isActive"> Is the product avaliable to buy?</param>
        /// <param name="canBeBoughtOnCredit"> Can this product be bought on credit? </param>
        /// <param name="startDate"> (dd/mm/yyyy) use '/' or '-' to seperate day/month/year </param>
        /// <param name="endDate"> (dd/mm/yyyy) use '/' or '-' to seperate day/month/year</param>
        public SeasonalProduct(string name, decimal price, bool isActive, bool canBeBoughtOnCredit,string startDate, string endDate) 
            : base(name, price, isActive, canBeBoughtOnCredit)
        {
            SetDate(startDate, StartDate);
            SetDate(endDate, EndDate);
            //TODO more logic
        }

    }
}
