using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace GitMemberSearch.FunctionalTests
{
    [ExcludeFromCodeCoverage]

    [TestFixture("Chrome")]
    [TestFixture("FireFox")]
    public class BaseFunctionalTests
    {
        public IWebDriver driver = null;
        protected string webDriverName = string.Empty;
        public WebDriverWait waitDriver = null;

        public string GitHubURL
        {
            get
            {
                return ConfigurationManager.AppSettings["TestSiteUrl"] + ConfigurationManager.AppSettings["TestSiteURLExtension"];
            }
        }

        public IWebElement SearchField 
        { 
            get 
            {
                return FindElementBy(By.Id("userName"));
            } 
        }

        public IWebElement SearchButton
        {
            get
            {
                return FindElementBy(By.Id("Search"));
            }
        }

        public IWebElement ErrorLabel
        {
            get
            {
                return FindElementBy(By.Id("userName-error"));
            }
        }

        public IWebElement NameField
        {
            get
            {
                
                return FindElementBy(By.XPath("//*[@id='userDetailsTable']/tr[1]/th"));
            }
        }

        public IWebElement SearchError
        {
            get
            {
                return FindElementBy(By.XPath("//*[@id='userDetailsTable']/tbody/tr/td"));
            }
        }

        public IWebElement NoRepositoryItems
        {
            get
            {
                
                return FindElementBy(By.XPath("/html/body/app/div[2]/div[2]/table[2]/tbody/tr/td"));
            }
        }

        public IWebElement ErrorPageText
        {
            get
            {
                return FindElementBy(By.XPath("/html/body/div[2]/hgroup/h1"));
            }
        }

        public IWebElement BadUserNameInputMessage
        {
            get
            {
                return FindElementBy(By.ClassName("validation-message"));
            }
        }

        public IWebElement FindElementBy(By by)
        {
            try
            {
                IWebElement findElement = waitDriver.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));

                return findElement;
            }
            catch
            {
                return null;
            }
        }

        [OneTimeSetUp]
        public void Open()
        {
            string browser = string.Empty;

            switch (webDriverName)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;

                case "FireFox":
                    driver = new FirefoxDriver();
                    break;

            }
            
            driver.Manage().Window.Maximize();
        }


        [SetUp]
        public void ReloadPage()
        {
            waitDriver = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Url = ConfigurationManager.AppSettings["TestSiteUrl"];
        }
        


        [OneTimeTearDown]
        public void Close()
        {
            driver.Close();
            driver.Dispose();
        }

        #region Helpers
        /// <summary>
        /// Sets the time for the implicit wait for the current instance
        /// </summary>
        /// <param name="seconds">The seconds to set the wait</param>
        protected void SetImplicitWait(double seconds)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
        }
        #endregion
    }
}
