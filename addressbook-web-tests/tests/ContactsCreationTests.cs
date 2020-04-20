using System;
using System.IO;
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
        public static IEnumerable<ContactsData> RandomContactsDataProvider()
        {
            List<ContactsData> contacts = new List<ContactsData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactsData(GenerateRandomString(30)) {
                    First_name = GenerateRandomString(100),
                    Last_name = GenerateRandomString(100),
                    Address = GenerateRandomString(100)
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactsData> ContactsDataFromFile()
        {
            List<ContactsData> contacts = new List<ContactsData>();
            string[] lines = File.ReadAllLines(@"contacts.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                contacts.Add(new ContactsData(parts[0])
                {
                    Last_name = parts[1],
                    First_name = parts[2]
                });
            }
            return contacts;
        }

        [Test, TestCaseSource("ContactsDataFromFile")]
        public void ContactsCreateTest(ContactsData contacts)
        {
            //ContactsData contacts = new ContactsData("aaa");
            //contacts.Middle_name = "ddd";
            //contacts.Last_name = "fff";

            List<ContactsData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.CreateC(contacts);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

            List<ContactsData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(contacts);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        //[Test]
        //public void EmptyContactsCreateTest()
        //{
        //    ContactsData contacts = new ContactsData("");
        //    contacts.Middle_name = "";
        //    contacts.Last_name = "";

        //    List<ContactsData> oldContacts = app.Contacts.GetContactsList();

        //    app.Contacts.CreateC(contacts);

        //    Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

        //    List<ContactsData> newContacts = app.Contacts.GetContactsList();
        //    oldContacts.Add(contacts);
        //    oldContacts.Sort();
        //    newContacts.Sort();
        //    Assert.AreEqual(oldContacts, newContacts);
        //}
        //Тест с пустыми именами убираем в 5.5

    }
}


