using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactsRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactsRemovalTest()
        {
            app.Contacts.CheckeContact();

            List<ContactsData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.RemoveTwo(0);

            List<ContactsData> newContacts = app.Contacts.GetContactsList();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}

