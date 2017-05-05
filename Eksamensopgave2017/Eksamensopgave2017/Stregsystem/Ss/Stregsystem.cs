using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Eksamensopgave2017
{
    public class Stregsystem : IStregsystem
    {
        private List<Product> _productList = new List<Product>();
        private List<Product> _activeProductsList = new List<Product>();
        private  List<User> _userList = new List<User>();
        private  List<Transaction> _transactionList = new List<Transaction>();
        public List<User> UserList { get { return _userList; } }
        public List<Product> ProductList { get { return _productList; } }
        public List<Transaction> TranactionsList { get { return _transactionList; } }
        public IEnumerable<Product> ActiveProducts { get { return _activeProductsList; } }

        public event UserBalanceNotification UserBalanceWarning;

        protected virtual void OnUserBalanceWarning(User user, decimal balance)
        {
            if (UserBalanceWarning != null)
            {
                UserBalanceWarning(user, balance);
            }
        }

        #region Get info from files
        private List<string> CsvFileReader(string path)
        {
            List<string> stringList = new List<string>();
            StreamReader fileCsvReader = new StreamReader(path);
            string fileLine = fileCsvReader.ReadLine();
            while (fileLine != null)
            {
                stringList.Add(fileLine);
                fileLine = fileCsvReader.ReadLine();
            }
            fileCsvReader.Close();
            return stringList;
        }
        private string RemoveHTMLTags(string fileDate)
        {
            // Removes " from the text
            fileDate = Regex.Replace(fileDate, "\"", "");
            char[] dateChars = new char[fileDate.Length];
            int lengthOfNewString = 0;
            bool insideHTML = false;

            // Removes html tags a.k.a eveything inside <...> 
            for (int i = 0; i < fileDate.Length; i++)
            {
                char c = fileDate[i];
                if (c == '<')
                {
                    insideHTML = true;
                }
                if (insideHTML == false)
                {
                    dateChars[lengthOfNewString] = c;
                    ++lengthOfNewString;
                } // Important that this condition last or else '>' will be a part of the final string
                if (c == '>')
                {
                    insideHTML = false;
                }


            }

            return new string(dateChars, 0, lengthOfNewString);
        }
        private void GetProductsFromFile()
        {
            List<string> productStringList = CsvFileReader("../../products.csv");
            for (int i = 0; i < productStringList.Count; i++)
            {
                productStringList[i] = RemoveHTMLTags(productStringList[i]);
                string[] tss = productStringList[i].Split(';');
                if (tss[0].All(c => Char.IsLetter(c)))
                {
                    //Makes sure that the header dosen't get included
                }
                else if (tss[4] != "")
                {
                    // Not a seasonel product
                    int id = int.Parse(tss[0]);
                    string name = tss[1];
                    decimal price = Decimal.Parse(tss[2]) / 100;
                    bool isActive = (tss[3] == "1");
                    bool canBeBoughtOnCredit = false; 
                    string endDate = tss[4];

                    _productList.Add(new SeasonalProduct(id, name, price, isActive, canBeBoughtOnCredit, "2000-11-21 00:00:00", endDate));
                }
                else
                {
                    // Seasonel product
                    int id = int.Parse(tss[0]);
                    string name = tss[1];
                    decimal price = Decimal.Parse(tss[2]) / 100;
                    bool isActive = (tss[3] == "1");
                    bool canBeBoughtOnCredit = false; 

                    _productList.Add(new Product(id, name, price, isActive, canBeBoughtOnCredit));

                }
            }
        }
        private void GetUsersFromFile()
        {
            List<String> userFileList = CsvFileReader("../../users.csv");
            for(int i = 0; i < userFileList.Count; i++)
            {
                userFileList[i] = RemoveHTMLTags(userFileList[i]);
                string[] uISs = userFileList[i].Split(';'); // uISs = userInfoSubstring
                if (uISs[0].All(c => char.IsDigit(c)))
                {
                    int id = int.Parse(uISs[0]);
                    string firstName = uISs[1];
                    string lastName = uISs[2];
                    string userName = uISs[3];
                    string eMail = uISs[4];
                    decimal balance = decimal.Parse(uISs[5], CultureInfo.InvariantCulture);
                    // CultureInfo makes the decimal.parse aware of the '.' in the csv file så than it gets the value 100.0 instead of 1000
                    try
                    {
                        _userList.Add(new User(id, firstName, lastName, userName, eMail, balance));
                    }
                    catch (UserCouldNotBeCreatedException)
                    {
                        // Man kunne vel lave en reference til ui men det har jeg ikke så håndtere ikke så meget ved exceptionen
                    }
                    
                }
                
            }
        }
        #endregion

        #region Get thing from id/username

        public User GetUser(Func<User, bool> predicate)
        {

            return _userList.FirstOrDefault(predicate);
        }

        public Product GetProductByID(int productID)
        {
            foreach (Product product in _productList)
            {
                if (product.Id == productID)
                {
                    return product;
                }
            }
            return null;
        }
        public User GetUserByUsername(string username)
        {
            username = username.ToLower();
            foreach (User user in _userList)
            {
                if (user.UserName == username)
                {
                    return user;
                }
            }
            return null;
        }
        #endregion



        public void UpdateActiveProducts()
        {
            _activeProductsList.RemoveAll( item => true);
            foreach (Product product in ProductList)
            {
                if (product.IsActive)
                {
                    _activeProductsList.Add(product);
                }
            }
        }






        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            InsertCashTransaction insCashTrans = new InsertCashTransaction(user, amount);
            _transactionList.Add(insCashTrans);
            LogTransaction(insCashTrans.ToString());

            return insCashTrans;
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction buyTrans = new BuyTransaction(user, product.Price, product);
            _transactionList.Add(buyTrans);
            if (user.Balance < 50)
            {
                OnUserBalanceWarning(user, user.Balance);
            }
            LogTransaction(buyTrans.ToString());
            return buyTrans;
        }

        private void LogTransaction(string logText)
        {
            string path = "../../logfile.log";
            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine(logText);
            sw.Close();
        }


        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            List<Transaction> userTransactions = new List<Transaction>();
            int foundTransacc = 0;
            for (int i = _transactionList.Count - 1; i >= 0; i--)
            {
                if (_transactionList[i].Buyer.Equals(user))
                {
                    userTransactions.Add(_transactionList[i]);
                    ++foundTransacc;
                    if (foundTransacc == count)
                    {
                        break;
                    }
                }
            }
            return userTransactions.AsEnumerable();
        }





        public Stregsystem()
        {
            GetProductsFromFile();
            GetUsersFromFile();
            UpdateActiveProducts();
        }
    }
}