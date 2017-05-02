using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{

    public class SeasonalProduct : Product
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        // TODO divide into sub methods
        private bool SetDate(string dateString)
        {
            DateTime date;
            string[] dateAndTime = dateString.Split(' ');
            string[] timeSubstring = dateAndTime[1].Split(':');
            int[] time = new int[3];
            for (int i = 0; i < 3; i++)
            {
                time[i] = int.Parse(timeSubstring[i]);
            }
            string[] dateSubstring = dateAndTime[0].Split('-', '/');
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
            if (dates[2] < 1 || dates[2] > 31)
            {
                //TODO better date checking
                return false;
            }
            if (dates[1] < 1 || dates[1] > 12)
            {
                return false;
            }
            if (dates[0] < 1 || dates[0] > 10000)
            {
                return false;
            }
            date = new DateTime(dates[0], dates[1], dates[2],time[0], time[1], time[2]);
            // Checks if the end date is after the start date
            if (StartDate.CompareTo(DateTime.MinValue) != 0 ) // == If startdate is differnet than null
            {
                if (date.CompareTo(StartDate) != -1)
                {
                    //Todo skal nok flyttes
                    EndDate = date;
                    return false;
                }
            }
            StartDate = date;
            return true;
        }
        //TODO stardate skal måske ændres til datetime som paramteret
        // Maybe override Active to be more specific
        /// <summary>
        ///  hallo
        /// </summary>
        /// <param name="id">Uniqe identity of product</param>
        /// <param name="name"> Products name</param>
        /// <param name="price"></param>
        /// <param name="isActive"> Is the product avaliable to buy?</param>
        /// <param name="canBeBoughtOnCredit"> Can this product be bought on credit? </param>
        /// <param name="startDate"> (yyyy/mm/dd) use '/' or '-' to seperate day/month/year </param>
        /// <param name="endDate"> (yyyy/mm/dd) use '/' or '-' to seperate day/month/year</param>
        public SeasonalProduct(int id, string name, decimal price, bool isActive, bool canBeBoughtOnCredit,string startDate, string endDate) 
            : base(id,name, price, isActive, canBeBoughtOnCredit)
        {
            SetDate(startDate);
            SetDate(endDate);
            //TODO more logic
        }

    }
}
