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

        public ContactsHelper ModifyContacts(int p, ContactsData newData)
        {
            manager.Navigator.GoToContactsPage();
            SelectContacts(p);
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
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contacts.First_name);
            driver.FindElement(By.Name("middlename")).Click();
            driver.FindElement(By.Name("middlename")).Clear();
            driver.FindElement(By.Name("middlename")).SendKeys(contacts.Middle_name);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contacts.Last_name);
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
            driver.FindElement(By.LinkText("Logout")).Click();
            return this;
        }

        public ContactsHelper SelectContacts(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
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
