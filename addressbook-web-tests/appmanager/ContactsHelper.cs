using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox; 
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactsHelper : HelperBase
    {
        public bool acceptNextAlert = true;

        public ContactsHelper(ApplicationManager manager) 
            : base(manager)
        {
        }

        public ContactsHelper CreateC(ContactsData contacts)
        {
            manager.Navigator.GoToNewContactsPage();
            FillContactsForm(contacts);
            SubmitContactsCreation();
            ReturnToContactsPage();
            return this;
        }

        public ContactsHelper ModifyContacts(ContactsData newData)
        {
            manager.Navigator.GoToContactsPage();
            InitGroupModification();
            FillContactsForm(newData);
            SubmitContactsModification();
            ReturnToContactsPage();
            return this;
        }


        public ContactsHelper RemoveTwo(int p)
        {
            manager.Navigator.GoToContactsPage();
            SelectContacts(p);
            RemoveContacts();
            ReturnToContactsPage();
            return this;

        }

        public ContactsHelper FillContactsForm(ContactsData contacts)
        {
            Type(By.Name("firstname"),contacts.First_name);
            Type(By.Name("middlename"), contacts.Middle_name);
            Type(By.Name("lastname"), contacts.Last_name);
            return this;
        }


        public ContactsHelper SubmitContactsCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            return this;
        }

        public ContactsHelper ReturnToContactsPage()
        {
            driver.FindElement(By.LinkText("home")).Click();
            return this;
        }

        public ContactsHelper SelectContacts(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
            return this;
        }

        public ContactsHelper RemoveContacts()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Delete 1 addresses[\\s\\S]$"));
            return this;
        }

        public ContactsHelper SubmitContactsModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactsHelper InitGroupModification()
        {
            driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
            return this;
        }

        public void CheckeContact()
        {
            if (!IsElementPresent(By.XPath("//img[@alt='Edit']")))
            {
                ContactsData contacts = new ContactsData("nnn");
                CreateC(contacts);

            }
        }

        public List<ContactsData> GetContactsList()
        {
            List<ContactsData> contacts = new List<ContactsData>();
            manager.Navigator.GoToContactsPage();
            ICollection<IWebElement> elements = driver.FindElements(By.XPath("//tr[@name='entry']"));
            //var rows = driver.FindElement(By.XPath(Patch("//tr[@name='entry']"));
            foreach (IWebElement element in elements)
            {
                var cells = element.FindElements(By.XPath("./td"));
                var last_name = cells[1].Text;
                var first_name = cells[2].Text;

                //var first_name = element.Text.Split(' ')[0];
                //string last_name1 = element.FindElements(By.XPath("./td"))[2].Text;
                //var last_name = element.Text.Split(' ')[1];
                ContactsData contact = new ContactsData(first_name, last_name);
                contacts.Add(contact);
            }
            return contacts;
        }


        public string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
