using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace SVCareers_Automation_Testing_Project.StepDefinitions
{
    [Binding]
    public class NavigationSteps
    {
        private IWebDriver webDriver;

        [BeforeScenario]
        public void InitScenario()
        {
            webDriver = new ChromeDriver();
        }

        [AfterScenario]
        public void TearDownScenario()
        {
            //webDriver.Dispose();
        }

        [Given(@"Browser is open")]
        public void GivenBrowserIsOpen()
        {
            webDriver.Navigate().GoToUrl("http://spicareers.spi.com/spicareers/");
        }
        
        [Given(@"Navigation address is entered in the browser address bar")]
        public void GivenNavigationAddressIsEnteredInTheBrowserAddressBar()
        {
            //webDriver.FindElement(By.)
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"On Enter i should land in the SVCareers login page")]
        public void ThenOnEnterIShouldLandInTheSVCareersLoginPage()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
