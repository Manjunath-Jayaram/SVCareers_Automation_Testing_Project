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
            webDriver.Navigate().GoToUrl("http://spicareers-uat/spicareers/");
            //webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [AfterScenario]
        public void TearDownScenario()
        {
        }

        [Given(@"SVCareers page is already open")]
        public void GivenSVCareersPageIsAlreadyOpen()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"Fill the (.*), (.*)")]
        public void GivenFillThe(string username, string password)
        {
            IWebElement elUserName = webDriver.FindElement(By.Name("loginId"));
            elUserName.SendKeys("Manju");

            IWebElement elPassword = webDriver.FindElement(By.Name("loginScreenPassword"));
            elPassword.SendKeys("sss");
        }
        
        [Then(@"Click on Login button")]
        public void ThenClickOnLoginButton(Table table)
        {
            IWebElement elLoginBtn = webDriver.FindElement(By.Id("svCareersLoginId"));
            elLoginBtn.Click();
            //ScenarioContext.Current.Pending();
        }
    }
}
