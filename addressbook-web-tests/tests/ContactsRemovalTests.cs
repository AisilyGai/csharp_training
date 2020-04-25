using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactsRemovalTests : ContactsTestBase
    {
        [Test]
        public void ContactsRemovalTest()
        {
            app.Contacts.CheckeContact();

            List<ContactsData> oldContacts = ContactsData.GetAllContact();
            ContactsData toBeRemoved = oldContacts[0];

            app.Contacts.RemoveC(toBeRemoved);

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactsCount());

            List<ContactsData> newContacts = ContactsData.GetAllContact();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactsData contacts in newContacts)
            {
                Assert.AreNotEqual(contacts.Id, toBeRemoved.Id);
            }
        }
    }
}

