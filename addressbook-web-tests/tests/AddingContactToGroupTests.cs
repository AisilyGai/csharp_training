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
            app.Contacts.ContactsProv();
            app.Groups.GroupProv();

            GroupData group = GroupData.GetAllGroup()[0];
            List<ContactsData> oldList = group.GetContacts();
            ContactsData contact = ContactsData.GetAllContact().Except(oldList).First();

            for (int i = 0; i < oldList.Count(); i++)
            {
                if (oldList[i].Id.Equals(contact.Id))
                {
                    contact = new ContactsData("aaa", "ddd");
                    app.Contacts.CreateC(contact);
                    contact.Id = app.Contacts.GetContactId();
                }
            }
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
            app.Contacts.ContactsProv();
            app.Groups.GroupProv();

            GroupData group = GroupData.GetAllGroup()[0];
            List<ContactsData> oldList = group.GetContacts();
            ContactsData contact = ContactsData.GetAllContact().First();

            if (group.GetContacts().Count() == 0)
            {
                app.Contacts.AddContactToGroup(contact, group);
            }
            app.Contacts.DeleteContactFromFroup(contact, group);

            List<ContactsData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreNotEqual(oldList, newList);
        }
    }
}
