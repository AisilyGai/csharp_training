using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTest : AuthTestBase
    {
        [Test]

        public void TestContactInormation()
        {
            ContactsData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactsData fromFrom = app.Contacts.GetContactInformationFromEditForm(0);

            //verefication
            Assert.AreEqual(fromTable, fromFrom);
            Assert.AreEqual(fromTable.Address, fromFrom.Address);
            Assert.AreEqual(fromTable.AllPhones, fromFrom.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromFrom.AllEmails);
        }
        [Test]
        public void TestContactOfDetailedInformation()
        {
            ContactsData fromTable = app.Contacts.GetContactInformationFromEditForm(0);
            ContactsData fromForm = app.Contacts.GetContactInformationFromDetailed(0);

            //проверка равенства данных страницы редактирования и свойств контакта
            Assert.AreEqual(fromTable.AllInformations, fromForm.AllInformations);
        }
    }
}
