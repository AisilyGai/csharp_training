using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]

    public class GroupModificationTests : AuthTestBase
    {
        
        [Test]
        public void GroupModificationTest()
        {
            app.Groups.CheckeGroup(1);
            GroupData newData = new GroupData("GroupModifiedData");
            newData.Header = null;
            newData.Footer = null;
        }
    }
}
