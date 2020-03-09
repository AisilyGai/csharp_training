using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactsData
    {
        private string first_name;
        private string middle_name = "";
        private string last_name = "";

        public ContactsData(string first_name)
        {
            this.first_name = first_name;
        }

        public string First_name
        {
            get
            {
                return first_name;
            }
            set
            {
                first_name = value;
            }
        }
        public string Middle_name
        {
            get
            {
                return middle_name;
            }
            set
            {
                middle_name = value;
            }
        }
        
        public string Last_name
        {
            get
            {
                return last_name;
            }
            set
            {
                last_name = value;
            }
        }
    }
}
