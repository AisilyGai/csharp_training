using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]

    public class GroupModificationTests : GroupTestBase
    {
        
        [Test]
        public void GroupModificationTest()
        {
            app.Groups.CheckeGroup(0);
            GroupData newData = new GroupData("GroupModifiedData");
            newData.Header = null;
            newData.Footer = null;

            List<GroupData> oldGroups = GroupData.GetAllGroup();
            //GroupData oldData = oldGroups[0];
            GroupData toBeModifyed = oldGroups[0];

            app.Groups.ModifyToBe(toBeModifyed, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAllGroup();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == toBeModifyed.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
}
