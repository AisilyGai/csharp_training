using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactsData : IEquatable<ContactsData>, IComparable<ContactsData>
{
        private string allPhones;
        private string allEmails;
        //private string first_name;
        //private string middle_name = "";
        //private string last_name = "";

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
            return "first_name=" + First_name + "; last_name=" + Last_name;
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
        public string First_name { get; set; }

        public string Middle_name { get; set; }

        public string Last_name { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }

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
            return phone.Replace("", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
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

        public string Id { get; set; }
    }
}
