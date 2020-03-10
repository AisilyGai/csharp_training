using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactsRemovalTests : TestBase
    {
        [Test]
        public void ContactsRemovalTest()
        {
            app.Navigator.GoToHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Navigator.GoToContactsPage();
            app.Contacts.SelectContacts(1);
            app.Contacts.RemoveContacts();
            app.Contacts.ReturnToContactsPage();
        }
    }
}

