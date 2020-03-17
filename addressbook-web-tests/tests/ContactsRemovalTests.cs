using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactsRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactsRemovalTest()
        {
            app.Contacts.RemoveTwo();
        }
    }
}

