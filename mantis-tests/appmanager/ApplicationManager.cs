using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected StringBuilder verificationErrors;
        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();
        protected NavigationHelper navigationHelper;
        protected LoginHelper loginHelper;
        protected ProjectHelper projectHelper;
        protected string baseUrl;

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseUrl = "http://localhost/mantisbt-2.24.0";
            verificationErrors = new StringBuilder();
            Registration = new RegistrationHelper(this);
            FTP = new FTPHelper(this);
            James = new JamesHelper(this);
            Mail = new MailHelper(this);
            navigationHelper = new NavigationHelper(this);
            loginHelper = new LoginHelper(this);
            projectHelper = new ProjectHelper(this);
            Admin = new AdminHelper(this, baseUrl);
            API = new APIHelper(this);
        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstasnce = new ApplicationManager();
                newInstasnce.driver.Url = newInstasnce.baseUrl + "/login_page.php";
                app.Value = newInstasnce;
            }
            return app.Value;
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public RegistrationHelper Registration { get; set; }

        public FTPHelper FTP { get; set; }

        public JamesHelper James { get; set; }

        public MailHelper Mail { get; set; }
        public AdminHelper Admin { get; private set; }
        internal APIHelper API { get; private set; }

        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }

        public NavigationHelper Navigator
        {
            get
            {
                return navigationHelper;
            }
        }

        public ProjectHelper project
        {
            get
            {
                return projectHelper;
            }
        }
    }
}
