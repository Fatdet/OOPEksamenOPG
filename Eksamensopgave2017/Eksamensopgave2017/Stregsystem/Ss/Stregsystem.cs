using System;
using System.Collections.Generic;
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
        //TODO transaktion list skal ligges i en log fil
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
                    int id = int.Parse(tss[0]);
                    string name = tss[1];
                    decimal price = Decimal.Parse(tss[2]);
                    bool isActive = (tss[3] == "1");
                    bool canBeBoughtOnCredit = false; //TODO der står ikke noget i filen om dette?
                    string endDate = tss[4];

                    _productList.Add(new SeasonalProduct(id, name, price, isActive, canBeBoughtOnCredit, "2000-11-21 00:00:00", endDate));
                }
                else
                {
                    int id = int.Parse(tss[0]);
                    string name = tss[1];
                    decimal price = Decimal.Parse(tss[2]);
                    bool isActive = (tss[3] == "1");
                    bool canBeBoughtOnCredit = false; //TODO der står ikke noget i filen om dette?

                    _productList.Add(new Product(id, name, price, isActive, canBeBoughtOnCredit));

                }
            } 
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
                    decimal balance = decimal.Parse(uISs[5]);

                    _userList.Add(new User(id, firstName, lastName, userName, eMail, balance));
                }
                
            }
        }
#endregion
        public IEnumerable<Product> ActiveProducts
        {
            get
            {
                return _activeProductsList;
            }
        }

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
        //public delegate void UserBalanceNotification(object obj, EventArgs args);

        public event UserBalanceNotification UserBalanceWarning;

        protected virtual void OnUserBalanceWarning()
        {
            if (UserBalanceWarning != null)
            {
                UserBalanceWarning(this, EventArgs.Empty);
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
            this.UserBalanceWarning += buyTrans.OnUserBalanceWarning;
            _transactionList.Add(buyTrans);
            if (user.Balance < 50)
            {
                OnUserBalanceWarning();
            }
            LogTransaction(buyTrans.ToString());
            return buyTrans;
        }

        private void LogTransaction(string logText)
        {
            //TODO maybe clear log when rpograms starts?
            string path = "../../logfile.log";
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(logText);
            sw.Close();
            
            
        }

        public Product GetProductByID(int productID)
        {
            foreach (Product product in ProductList)
            {
                if (product.Id == productID)
                {
                    return product;
                }
            }
            return null;
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

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            foreach (User user in UserList)
            {
                if (predicate(user))
                {
                yield return user;
                }
            }
            
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

        public Stregsystem()
        {
            GetProductsFromFile();
            GetUsersFromFile();
            UpdateActiveProducts();
        }
    }
}