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
            GoToHomePage();
            Login(new AccountData("admin", "secret"));
            GoToNewContactsPage();
            ContactsData contacts = new ContactsData("aaa");
            contacts.Middle_name = "ddd";
            contacts.Last_name = "fff";
            FillContactsForm(contacts);
            SubmitContactsCreation();
            ReturnToContactsPage();
        }
    }
}


