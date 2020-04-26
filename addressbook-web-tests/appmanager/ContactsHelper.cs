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
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactsHelper : HelperBase
    {
        public bool acceptNextAlert = true;

        public ContactsHelper(ApplicationManager manager) 
            : base(manager)
        {
        }

        internal void AddContactToGroup(ContactsData contact, GroupData group)
        {
            manager.Navigator.GoToContactsPage();
            ClearGroupFilter();
            SelectContactToGroups(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        private void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void SelectContactToGroups(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public void DeleteContactFromFroup(ContactsData contact, GroupData group)
        {
            manager.Navigator.GoToContactsPage();
            GroupFilter(group.Name);
            SelectContactToGroups(contact.Id);
            SelectGroupToAdd(group.Name);
            RemoveContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void GroupFilter(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        public void RemoveContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
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

        //public ContactsHelper ModifyToBe(ContactsData contact, ContactsData newData)
        //{
        //    manager.Navigator.GoToContactsPage();
        //    InitGroupModification();
        //    FillContactsForm(newData);
        //    SubmitContactsModification();
        //    ReturnToContactsPage();
        //    return this;
        //}


        public ContactsHelper RemoveTwo(int p)
        {
            manager.Navigator.GoToContactsPage();
            SelectContacts(p);
            RemoveContacts();
            ReturnToContactsPage();
            return this;

        }

        public ContactsHelper RemoveC(ContactsData contact)
        {
            manager.Navigator.GoToContactsPage();

            SelectContacts(contact.Id);
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

        public ContactsHelper SelectContacts(string id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='"+id+"'])")).Click();
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
            //driver.FindElement(By.Name("entry"))[index]
            //    .FindElements(By.TagName("td"))[7]
            //    .FindElement(By.TagName("a")).Click();
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
                    contactsCache.Add(new ContactsData(first_name, last_name)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("vlue")
                    });
                }
            }

            return new List<ContactsData>(contactsCache);
        }

        public int GetContactsCount()
        {
            return driver.FindElements(By.XPath("//tr[@name='entry']")).Count;
        }

        public ContactsData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string last_name = cells[1].Text;
            string first_name = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactsData(first_name, last_name)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails,
            };
        }

        public ContactsData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModifition(0);
            string first_name = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string last_name = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
           
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
           
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactsData(first_name, last_name)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3,
           //Склейка данных со страницы редактирования контакта
                AllInformations = first_name + last_name + "\r\n" + address + "\r\n\r\n" + AdaptationPhone(homePhone)
                + AdaptationPhone(mobilePhone) + AdaptationPhone(workPhone) + "\r\n" + email + "\r\n" + email2 + "\r\n" + email3
            };
            //Проверка телефонов для установки H: W: M:
            string AdaptationPhone(string Phone)
            {
                if (Phone != "")
                {
                    if (Phone == homePhone || Phone == mobilePhone)
                    {
                        if (Phone == homePhone)
                        {
                            return "H:" + Phone + "\r\n";
                        }
                        return "M:" + Phone + "\r\n";
                    }
                    return "W:" + Phone + "\r\n";
                }
                return Phone;
            }
        }

        public void InitContactModifition(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
        //Склейка данных со страницы свойств контакта
        public ContactsData GetContactInformationFromDetailed(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactDetailed(0);

            string DetailedInformation = driver.FindElement(By.CssSelector("div#content")).Text;
            string first_name = "";
            string last_name = "";

            return new ContactsData(first_name, last_name)
            {

                AllInformations = DetailedInformation
            };
        }
        //Переход на страницу свойств контакта
        public void InitContactDetailed(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
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
