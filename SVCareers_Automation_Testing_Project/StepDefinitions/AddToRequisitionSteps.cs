using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;

namespace SVCareers_Automation_Testing_Project.StepDefinitions
{
    [Binding]
    public class AddToRequisitionSteps
    {
        private IWebDriver webDriver;
        [Given(@"Launch the chrome browser window")]
        public void GivenLaunchTheChromeBrowserWindow()
        {
            webDriver = new ChromeDriver();
        }
        
        [Given(@"Open SVCareers website in the chrome browser")]
        public void GivenOpenSVCareersWebsiteInTheChromeBrowser()
        {
            webDriver.Navigate().GoToUrl("http://spicareers-uat/spicareers/");
            webDriver.Manage().Window.Maximize();
        }
        
        [Given(@"Enter the user details in the login form")]
        public void GivenEnterTheUserDetailsInTheLoginForm()
        {
            Thread.Sleep(2000);
            webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSMainFrame")));
            IWebElement elUserName = webDriver.FindElement(By.Name("loginId"));
            elUserName.SendKeys("shashi.srinivas");

            IWebElement elPassword = webDriver.FindElement(By.Name("loginScreenPassword"));
            elPassword.SendKeys("sv123");
        }
        
        [Then(@"Login to the SVCareers website with the user details provided")]
        public void ThenLoginToTheSVCareersWebsiteWithTheUserDetailsProvided()
        {
            webDriver.FindElement(By.Id("svCareersLoginId")).Click();
        }

        [Then(@"Click on Home tab to get the job requests grid")]
        public void ThenClickOnHomeTabToGetTheJobRequestsGrid()
        {
            Thread.Sleep(2000);
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSHeader")));
            webDriver.FindElement(By.ClassName("menuhome")).Click();
        }

        [Then(@"Select the job request with external resource flag set to one and requesition status is not posted")]
        public void ThenSelectTheJobRequestWithExternalResourceFlagSetToOneAndRequesitionStatusIsNotPosted()
        {
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSMainFrame")));
            IWebElement iframeJobRequests = webDriver.FindElement(By.Id("frmJbRqst"));
            webDriver.SwitchTo().Frame(iframeJobRequests);

            IWebElement jobRequestsTable = webDriver.FindElement(By.CssSelector("table.greenborder"));
            IList<IWebElement> tableRow = jobRequestsTable.FindElements(By.TagName("tr"));
            IList<IWebElement> rowTD;
            int index = -1;
            foreach (IWebElement row in tableRow)
            {
                index = index + 1;
                if(index > 1)
                {
                    rowTD = row.FindElements(By.TagName("td"));
                    if (rowTD.Count > 22)
                    {
                        if (rowTD[12].Text.Equals("1") && (!rowTD[1].Text.Contains("Posted")))
                        {
                            rowTD[0].Click();
                            break;
                        }
                    }
                }
            }
        }

        [Then(@"Click on add to requisition link")]
        public void ThenClickOnAddToRequisitionLink()
        {
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSMainFrame")));
            webDriver.FindElement(By.CssSelector("a[href*=createReqFrmJbRqst]")).Click();
        }


    }
}
