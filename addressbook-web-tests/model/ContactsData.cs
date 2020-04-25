using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactsData : IEquatable<ContactsData>, IComparable<ContactsData>
    {
        private string allPhones;
        private string allEmails;
        private string allInformations;
        //private string first_name;
        //private string middle_name = "";
        //private string last_name = "";

        public ContactsData()
        {
        }

        public ContactsData(string first_name)
        {
            First_name = first_name;
        }
        public ContactsData(string first_name, string last_name)
        {
            First_name = first_name;
            Last_name = last_name;
        }

        public bool Equals(ContactsData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return First_name == other.First_name && Last_name == other.Last_name;
        }
        public override int GetHashCode()
        {
            return First_name.GetHashCode() + Last_name.GetHashCode();
        }

        public override string ToString()
        {
            return "first_name=" + First_name + "\nlast_name=" + Last_name + "\naddress=" + Address;
        }

        public int CompareTo(ContactsData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Last_name.CompareTo(other.Last_name) == 0)
                return First_name.CompareTo(other.First_name);

            return Last_name.CompareTo(other.Last_name);
        }
        [Column(Name = "firstname")]
        public string First_name { get; set; }

        [Column(Name = "middlename")]
        public string Middle_name { get; set; }

        [Column(Name = "lastname")]
        public string Last_name { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "email2")]
        public string Email2 { get; set; }

        [Column(Name = "email3")]
        public string Email3 { get; set; }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -() ]", "") + "\r\n";
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUp(Email) + CleanUp(Email2) + CleanUp(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }
        // метод для очистки данных от лишних скобок()-
        public string AllInformations
        {
            get
            {
                return (CleanUp(allInformations)).Trim();
            }
            set
            {
                allInformations = value;
            }
        }
        public static List<ContactsData> GetAllContact()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Contacts select g).ToList();
                //return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }
    }
}
