using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    public class User : IComparable<User>
    {
        private  static List<string> _UsedUserNames = new List<string>();
        public int Id { get; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public decimal Balance { get; set; }
        public  bool AllInfoCorrecet { get; }
        //TODO implemt static list of usernames and Id
        #region Setters

        private bool SetFirstName(string firstName)
        {
            if (firstName == null)
            {
                return false;
            }
            else
            {
                FirstName = firstName;
                return true;
            }
        }
        private bool SetLastName(string lastName)
        {
            if (lastName == null)
            {
                return false;
            }
            else
            {
                LastName = lastName;
                return true;
            }
        }
        // Returns false when name could not be set.
        private bool SetUserName(string name)
        {
            if (name == null
                || !name.All(c => Char.IsLower(c)
                || Char.IsDigit(c)
                || !c.Equals('_')))
            {
                return false;
            }
            else
            {
                if (_UsedUserNames.Contains(UserName))
                {
                    throw new UserAlreadyExistException();
                }

                UserName = name;
                _UsedUserNames.Add(UserName);
                return true;
            }
        }

        private bool SetEmail(string email)
        {
            string local;
            string domain;
            string[] emailSubstring = email.Split('@');
            // Is it split in two?
            if (emailSubstring.Length != 2)
            {
                return false;
            }
            local = emailSubstring[0];
            domain = emailSubstring[1];
            // Testing local for [A-Z][a-z][0-9][.,-,_]
            if (!local.All(c => Char.IsLetterOrDigit(c)
                || (new[] {'.','_','-'}).Contains(c)))
            {
                // Maybe some exception that local was not correct
                return false;
            }
            // Testing domain for [A-Z][a-z][0-9][.,_]
            if (!domain.All(c => char.IsLetterOrDigit(c)
                || (new[] {'.','_'}).Contains(c)))
            {
                Console.WriteLine(domain);
                return false;
            }
            if (domain.StartsWith("_")
                ||domain.StartsWith(".")
                ||domain.EndsWith("_")
                ||domain.EndsWith("."))
            {

                return false;
            }
            Email = email;
            return true;
        }
        #endregion
        #region Implemented methods

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}, Email: {Email}, \nBalance: {Balance} ";
        }
        public int CompareTo(User user)
        {
            return user.Id - this.Id;
        }

        public override bool Equals(object obj)
        {
            User user = obj as User;
            if (user == null) 
            {
                return false;
            }
            return user.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return UserName.GetHashCode() + this.Id;
        }
        #endregion
        /// <summary>
        /// Creates a user for the Stregsystem
        /// </summary>
        /// <param name="id">Gets checked if it is uniqe if not it will change</param>
        /// <param name="firstName">may not be null</param>
        /// <param name="lastName">may not be null</param>
        /// <param name="userName">may only contain [a-z], [0-9] and '_'</param>
        /// <param name="email">standard email</param>
        /// <param name="balance">users current balance</param>
        public User(int id, string firstName, string lastName, string userName, string email , decimal balance)
        {
            if (!(SetFirstName(firstName)
                && SetLastName(lastName) 
                && SetUserName(userName)
                &&SetEmail(email)))
            {
                throw new UserCouldNotBeCreatedException();
            }
            Balance = balance;

            Id = id;
        }
    }
}
