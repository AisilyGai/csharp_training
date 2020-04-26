using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            GroupData group = GroupData.GetAllGroup()[0];
            List<ContactsData> oldList = group.GetContacts();
            ContactsData contact = ContactsData.GetAllContact().Except(oldList).First();

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactsData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
        [Test]
        public void TestdeleteContactFromGroups()
        {
            GroupData group = GroupData.GetAllGroup()[0];
            List<ContactsData> oldList = group.GetContacts();
            ContactsData contact = ContactsData.GetAllContact().First();


            app.Contacts.DeleteContactFromFroup(contact, group);

            List<ContactsData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreNotEqual(oldList, newList);
        }
    }
}
