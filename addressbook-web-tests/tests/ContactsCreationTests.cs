using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactsCreateTests : AuthTestBase
    {
        [Test]
        public void ContactsCreateTest()
        {
            ContactsData contacts = new ContactsData("aaa");
            contacts.Middle_name = "ddd";
            contacts.Last_name = "fff";

            List<ContactsData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.CreateC(contacts);

            List<ContactsData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(contacts);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void EmptyContactsCreateTest()
        {
            ContactsData contacts = new ContactsData("");
            contacts.Middle_name = "";
            contacts.Last_name = "";

            List<ContactsData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.CreateC(contacts);

            List<ContactsData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(contacts);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

    }
}


