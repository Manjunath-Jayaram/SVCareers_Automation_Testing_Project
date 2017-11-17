using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVCareers_Automation_Testing_Project
{
    public static class HelperClass
    {
        public static IWebDriver Driver { get; set; }

        static IWebElement _page = null;
        public static void WaitForPageLoad(IWebDriver driver, string element)
        {
            var wait = new WebDriverWait(HelperClass.Driver, TimeSpan.FromMinutes(5));
            Func<IWebDriver, bool> waitForElement = web => driver.PageSource.Contains(element);
            wait.Until(waitForElement);

            //if (_page != null)
            //{
            //    var waitForCurrentPageToStale = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            //    waitForCurrentPageToStale.Until(ExpectedConditions.StalenessOf(_page));
            //}
            //var waitForDocumentReady = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            //waitForDocumentReady.Until((wdriver) => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            //_page = driver.FindElement(By.TagName("html"));
        }
    }
}
