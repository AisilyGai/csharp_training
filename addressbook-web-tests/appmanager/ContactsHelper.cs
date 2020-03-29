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
            contactsCache = null;
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
            contactsCache = null;
            return this;
        }

        public ContactsHelper SubmitContactsModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactsCache = null;
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

        private List<ContactsData> contactsCache = null;

        public List<ContactsData> GetContactsList()
        {
            if (contactsCache == null)
            {
                contactsCache = new List<ContactsData>();
                manager.Navigator.GoToContactsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.XPath("//tr[@name='entry']"));
                foreach (IWebElement element in elements)
                {
                    var cells = element.FindElements(By.XPath("./td"));
                    var last_name = cells[1].Text;
                    var first_name = cells[2].Text;
                    //contactsCache.contact = new ContactsData(first_name, last_name)
                    contactsCache.Add(new ContactsData(first_name, last_name)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("vlue")
                        //contactsCache.Add(new ContactsData(element.Text));
                    });
                }
            }

            //List<ContactsData> contacts = new List<ContactsData>();
            //manager.Navigator.GoToContactsPage();
            //ICollection<IWebElement> elements = driver.FindElements(By.XPath("//tr[@name='entry']"));
            //foreach (IWebElement element in elements)
            //{
            //    var cells = element.FindElements(By.XPath("./td"));
            //    var last_name = cells[1].Text;
            //    var first_name = cells[2].Text;
            //    ContactsData contact = new ContactsData(first_name, last_name);
            //    contacts.Add(contact);
            //}
            return new List<ContactsData>(contactsCache);
        }

        public int GetContactsCount()
        {
            return driver.FindElements(By.XPath("//tr[@name='entry']")).Count;
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
