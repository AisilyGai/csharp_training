using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class NavigationHelper : HelperBase
    {

        public NavigationHelper(IWebDriver driver) : base(driver)
        {
        }

        public void GoToHomePage()
        {
            driver.Navigate().GoToUrl("http://localhost/addressbook/");
        }

        public void GoToGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }

        public void GoToNewContactsPage()
        {
            driver.FindElement(By.LinkText("add new")).Click();
        }

        public void GoToContactsPage()
        {
            driver.FindElement(By.LinkText("home")).Click();
        }
    }
}
