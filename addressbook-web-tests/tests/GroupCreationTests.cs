using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData>GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            return (List<GroupData>) 
                new XmlSerializer(typeof(List<GroupData>))
                    .Deserialize(new StreamReader(@"groups.xml"));
        }

        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"groups.json"));
        }

        [Test, TestCaseSource("GroupDataFromJsonFile")]
        public void GroupCreationTest(GroupData group)
        {
            //GroupData group = new GroupData("aaa");
            //group.Header = "ddd";
            //group.Footer = "fff";

            List<GroupData> oldGroups = GroupData.GetAllGroup();

            app.Groups.CreateG(group);

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAllGroup();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void TestGroupDBConnectivity()
        {
            DateTime start = DateTime.Now;
            List<GroupData> fromUi = app.Groups.GetGroupList();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<GroupData> fromDb = GroupData.GetAllGroup();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
        }


        //[Test]
        //public void EmptyGroupCreationTest()
        //{
        //    GroupData group = new GroupData("");
        //    group.Header = "";
        //    group.Footer = "";

        //    List<GroupData> oldGroups = app.Groups.GetGroupList();

        //    app.Groups.CreateG(group);

        //    Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

        //    List<GroupData> newGroups = app.Groups.GetGroupList();
        //    oldGroups.Add(group);
        //    oldGroups.Sort();
        //    newGroups.Sort();
        //    Assert.AreEqual(oldGroups, newGroups);
        //}

        //[Test]
        //public void BadNameGroupCreationTest()
        //{
        //GroupData group = new GroupData("a 'a");
        //group.Header = "";
        //group.Footer = "";

        //List<GroupData> oldGroups = app.Groups.GetGroupList();

        //app.Groups.CreateG(group);

        //List<GroupData> newGroups = app.Groups.GetGroupList();
        //Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);
        //}
    }
}
