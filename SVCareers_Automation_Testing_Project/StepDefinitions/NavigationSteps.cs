using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SVCareers_Automation_Testing_Project.Hooks;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TechTalk.SpecFlow;

namespace SVCareers_Automation_Testing_Project.StepDefinitions
{
    [Binding]
    public class NavigationSteps
    {
        private IWebDriver webDriver;
        
        [Given(@"Open the browser")]
        public void GivenOpenTheBrowser()
        {
            webDriver = new ChromeDriver();
        }

        [Given(@"SVCareers page is already open")]
        public void GivenSVCareersPageIsAlreadyOpen()
        {
            webDriver.Navigate().GoToUrl("http://spicareers-uat/spicareers/");
            webDriver.Manage().Window.Maximize();
        }
        
        [Given(@"Fill the (.*), (.*)")]
        public void GivenFillThe(string username, string password)
        {
            Thread.Sleep(2000);
            webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSMainFrame")));
            IWebElement elUserName = webDriver.FindElement(By.Name("loginId"));
            elUserName.SendKeys("Manjunath.Jayaram");

            IWebElement elPassword = webDriver.FindElement(By.Name("loginScreenPassword"));
            elPassword.SendKeys("sv123");
            SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
        }
        
        [Then(@"Click on Login button")]
        public void ThenClickOnLoginButton(Table table)
        {
            IWebElement elLoginBtn = webDriver.FindElement(By.Id("svCareersLoginId"));
            elLoginBtn.Click();
        }

        [Then(@"Select the Job you want to refer")]
        public void ThenSelectTheJobYouWantToRefer()
        {
            IWebElement elJobRequest = webDriver.FindElement(By.CssSelector("#ShowDesc table tbody tr:nth-child(3) input[type=checkbox]"));
            elJobRequest.Click();
        }

        [Then(@"Click on refer a friend")]
        public void ThenClickOnReferAFriend()
        {
            IWebElement elReferFrd = webDriver.FindElement(By.CssSelector("#rowForReferAFriendToJob a"));
            elReferFrd.Click();
        }

        [Given(@"Switch the focus to the candidate details form")]
        public void GivenSwitchTheFocusToTheCandidateDetailsForm()
        {
            Thread.Sleep(2000);
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            Thread.Sleep(2000);
        }

        [Then(@"Fill candidate details form")]
        public void ThenFillCandidateDetailsForm()
        {
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=candFirstName]")).SendKeys("George");
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=candMiddleName]")).SendKeys("");
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=candLastName]")).SendKeys("Thompson");
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input#male")).Click();
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=candEmailId]")).SendKeys("GeorgeThompson@gmail.com");
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=candHomePhone]")).SendKeys("8888888888");
            SelectElement selectCandidateType = new SelectElement(webDriver.FindElement(By.Name("candType")));
            selectCandidateType.SelectByIndex(2);
            Thread.Sleep(1000);
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=minExperience]")).SendKeys("5");
            SelectElement selectTechnology = new SelectElement(webDriver.FindElement(By.Name("technology")));
            selectTechnology.SelectByIndex(2);
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=noticePeriod]")).SendKeys("30");
            IWebElement fileUpload = webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[type=file]"));
            fileUpload.SendKeys(@"C:\Work\Resume.pdf");
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] textarea[name=comments]")).SendKeys("Candidate has a H1B Visa");
            Thread.Sleep(2000);
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] a[title=Submit]")).Click();
        }


    }
}
