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
        public void ContactsModificationTest()
        {
            app.Contacts.CheckeContact();
            ContactsData newData = new ContactsData("ContactsModifiedData");
            newData.Middle_name = null;
            newData.Last_name = null;

            List<ContactsData> oldContacts = app.Contacts.GetContactsList();
            ContactsData oldData = oldContacts[0];

            app.Contacts.ModifyContacts(newData);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactsCount());

            List<ContactsData> newContacts = app.Contacts.GetContactsList();
            oldContacts[0].First_name = newData.First_name;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactsData contacts in newContacts)
            {
                if (contacts.Id == oldData.Id)
                {
                    //Assert.AreNotEqual(contacts.Id, oldContacts[0]);
                    Assert.AreEqual(newData.First_name, contacts.First_name);
                }
            }
        }
    }
}
