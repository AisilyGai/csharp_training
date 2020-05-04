using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class NavigationHelper : HelperBase
    {
        public NavigationHelper(ApplicationManager manager) : base(manager) { }
        public void Open_Homepage()
        {
            if (driver.Url == "http://localhost/mantisbt-2.24.0/my_view_page.php")
            {
                return;
            }
            driver.Navigate().GoToUrl("http://localhost/mantisbt-2.24.0/my_view_page.php");
        }

        public void Open_Loginpage()
        {
            driver.Navigate().GoToUrl("http://localhost/mantisbt-2.24.0/login_page.php");
        }

        public void GoToProjectPage()
        {
            if (driver.Url == "http://localhost/mantisbt-2.24.0/manage_proj_page.php")
            {
                return;
            }
            driver.Navigate().GoToUrl("http://localhost/mantisbt-2.24.0/manage_proj_page.php");
        }
    }
}
