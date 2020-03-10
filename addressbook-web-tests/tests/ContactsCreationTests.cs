using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactsCreateTests : TestBase
    {
        [Test]
        public void ContactsCreateTest()
        {
            ContactsData contacts = new ContactsData("aaa");
            contacts.Middle_name = "ddd";
            contacts.Last_name = "fff";

            app.Contacts.CreateC(contacts);
        }

        [Test]
        public void EmptyContactsCreateTest()
        {
            ContactsData contacts = new ContactsData("");
            contacts.Middle_name = "";
            contacts.Last_name = "";

            app.Contacts.CreateC(contacts);
        }

    }
}


