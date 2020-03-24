using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    
    [TestFixture]
    public class ContactsModificationTests : AuthTestBase
    {

        [Test]
        public void ContactsCreateTest()
        {
            app.Contacts.CheckeContact();
            ContactsData newData = new ContactsData("ContactsModifiedData");
            newData.Middle_name = null;
            newData.Last_name = null;

            List<ContactsData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.ModifyContacts(newData);

            List<ContactsData> newContacts = app.Contacts.GetContactsList();
            Assert.AreEqual(oldContacts.Count, newContacts.Count);
        }
    }
}
