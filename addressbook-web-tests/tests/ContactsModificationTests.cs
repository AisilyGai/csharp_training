using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    
    [TestFixture]
    public class ContactsModificationTests : TestBase
    {

        [Test]
        public void ContactsCreateTest()
        {
            ContactsData newData = new ContactsData("ContactsModifiedData");
            newData.Middle_name = "ContactsModifiedData";
            newData.Last_name = "ContactsModifiedData";

            app.Contacts.ModifyContacts(newData);
        }
    }
}
