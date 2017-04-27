﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    public class User : IComparable
    {
        private static int _id = 1;
        //---------------
        // TODO Maybe make get only and set them in the constructor eller private
        //ToDO Hvordan skal man håndtere invalide input til klassen!?!? Burde nok laves i konstruktoren
        //---------------
        public int UserID { get; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public decimal Balance { get; set; }

        #region Setters

        public bool SetFirstName(string firstName)
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
        public bool SetLastName(string lastName)
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
        public bool SetUserName(string name)
        {
            //-------
            // TODO Dobbelt tjek at ! er de rigtige steder
            //-------
            if (name == null
                || !name.All(c => Char.IsLower(c)
                || Char.IsDigit(c)
                || !c.Equals('_')))
            {
                return false;
            }
            else
            {
                UserName = name;
                return true;
            }
        }

        public bool SetEmail(string email)
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

        // TODO find bedre Icomparable der er mere hensigtmæssig (generisk)
        public int CompareTo(Object obj)
        {

            try
            {
                //lav exception på det her
            }
            catch (Exception message)
            {
                
            }
            User user = (User) obj;
            return user.UserID - UserID;
        }
        //TODO Maybe add spaces between strings
        public override string ToString()
        {
            return FirstName + LastName + Email;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(User)) //simplificere?
            {
                return false;
            }
            User user = obj as User;
                
            return user.UserID == this.UserID;
        }

        public override int GetHashCode()
        {
            // TODO check that the variables here is readonly so that the hashode never changes
            return UserName.GetHashCode() + UserID;
        }
#endregion
        public User()
        {
            UserID = _id;
            _id = ++_id;
        }
    }
}