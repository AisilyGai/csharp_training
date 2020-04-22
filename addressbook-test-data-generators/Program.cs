using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using WebAddressbookTests;

namespace addressbook_test_data_generators
{
    public class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            StreamWriter writer = new StreamWriter(args[1]);
            string format = args[2];

            List<ContactsData> contacts = new List<ContactsData>();
            List<GroupData> groups = new List<GroupData>();
            if (count == 2)
            {
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactsData(TestBase.GenerateRandomString(10))
                    {
                        Last_name = TestBase.GenerateRandomString(10),
                        First_name = TestBase.GenerateRandomString(10)
                    });
                }
                if (format == "csv")
                {
                    writeContactsToCsvFile(contacts, writer);
                }
                else if (format == "xml")
                {
                    writeContactsToXmlFile(contacts, writer);
                }
                else if (format == "json")
                {
                    writeContactsToJsonFile(contacts, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecognizwd format" + format);
                }
                writer.Close();
            }
            else
            {
                {
                    for (int i = 0; i < count; i++)
                    {
                        groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                        {
                            Header = TestBase.GenerateRandomString(10),
                            Footer = TestBase.GenerateRandomString(10)
                        });
                        //writer.WriteLine(String.Format("${0},${1},${2}",
                        //    TestBase.GenerateRandomString(10),
                        //    TestBase.GenerateRandomString(10),
                        //    TestBase.GenerateRandomString(10)));
                    }
                    if (format == "csv")
                    {
                        writeGroupsToCsvFile(groups, writer);
                    }
                    else if (format == "xml")
                    {
                        writeGroupsToXmlFile(groups, writer);
                    }
                    else if (format == "json")
                    {
                        writeGroupsToJsonFile(groups, writer);
                    }

                    else
                    {
                        System.Console.Out.Write("Unrecognizwd format" + format);
                    }
                    writer.Close();
                }
            }
        }

        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

        static void writeContactsToCsvFile(List<ContactsData> contacts, StreamWriter writer)
        {
            foreach (ContactsData contact in contacts)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    contact.Last_name, contact.First_name, contact.Address));
            }
        }

        static void writeContactsToXmlFile(List<ContactsData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactsData>)).Serialize(writer, contacts);
        }

        static void writeContactsToJsonFile(List<ContactsData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

    }
}
