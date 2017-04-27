using NUnit.Framework;
using Eksamensopgave2017;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017.Tests
{
    [TestFixture()]
    public class UserTests
    {
        User a = new User();
        [Test()]
        public void SetFirstNameTest()
        {
            a.SetFirstName(null);
            Assert.IsNotNull(a.FirstName);
        }

        [Test()]
        public void SetLastNameTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void SetUserNameTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void SetEmailTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CompareToTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ToStringTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void EqualsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetHashCodeTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void UserTest()
        {
            
            a.SetLastName(null);
            a.SetUserName(null);
            a.SetEmail(null);

            Assert.IsNotNull(a.LastName);
            Assert.IsNotNull(a.UserName);
            Assert.IsNotNull(a.Email);
            Assert.AreEqual(1, a.UserID);

        }
    }
}