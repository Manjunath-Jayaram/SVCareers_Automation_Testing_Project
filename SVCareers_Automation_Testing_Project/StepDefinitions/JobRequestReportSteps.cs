using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SVCareers_Automation_Testing_Project.Hooks;
using SVCareers_Automation_Testing_Project.Model;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;

namespace SVCareers_Automation_Testing_Project.StepDefinitions
{
    [Binding]
    public class JobRequestReportSteps
    {
        private IWebDriver webDriver;

        public JobRequestReportSteps()
        {
            ExcelLibrary.PopulateInCollection(ConfigurationManager.AppSettings["JobRequestReportFilePath"]);
        }

        [Given(@"Launch the web browser window")]
        public void GivenLaunchTheWebBrowserWindow()
        {
            webDriver = new ChromeDriver();
        }
        
        [Given(@"Open SVCareers website in the browser")]
        public void GivenOpenSVCareersWebsiteInTheBrowser()
        {
            webDriver.Navigate().GoToUrl("http://spicareers-uat/spicareers/");
            webDriver.Manage().Window.Maximize();
        }
        
        [Given(@"Enter the username and password in the login form")]
        public void GivenEnterTheUsernameAndPasswordInTheLoginForm()
        {
            Thread.Sleep(2000);
            webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSMainFrame")));
            IWebElement elUserName = webDriver.FindElement(By.Name("loginId"));
            elUserName.SendKeys(ExcelLibrary.ReadData(1, "UserName"));

            IWebElement elPassword = webDriver.FindElement(By.Name("loginScreenPassword"));
            elPassword.SendKeys(ExcelLibrary.ReadData(1, "Password"));
        }
        
        [Then(@"Click on Login button to login to SVCareers website")]
        public void ThenClickOnLoginButtonToLoginToSVCareersWebsite()
        {
            IWebElement elLoginBtn = webDriver.FindElement(By.Id("svCareersLoginId"));
            elLoginBtn.Click();
        }

        [Then(@"Check if the filter icon is available for the logged in user")]
        public void ThenCheckIfTheFilterIconIsAvailableForTheLoggedInUser()
        {
            try
            {
                //webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSMainFrame")));
                IWebElement iframeJobRequests = webDriver.FindElement(By.Id("frmJbRqst"));
                webDriver.SwitchTo().Frame(iframeJobRequests);
                if (webDriver.FindElements(By.CssSelector("img[onclick*=openFilter]")).Count == 0)
                {
                    Exception ex = new Exception("User is not authorized to get the job requests report");
                    throw ex;
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Warning(ex.Message);
            }
        }

        [Then(@"Click on filter icon from the job requests grid")]
        public void ThenClickOnFilterIconFromTheJobRequestsGrid()
        {
            webDriver.FindElement(By.CssSelector("img[onclick*=openFilter]")).Click();
        }

        [Then(@"Swich the focus to the report list window")]
        public void ThenSwichTheFocusToTheReportListWindow()
        {
            Thread.Sleep(2000);
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            Thread.Sleep(2000);
        }

        [Then(@"Select the location for the job request report")]
        public void ThenSelectTheLocationForTheJobRequestReport()
        {
            SelectElement selectRole = new SelectElement(webDriver.FindElement(By.Name("location")));
            selectRole.SelectByText(ExcelLibrary.ReadData(1, "Location"));
        }

        [Then(@"Select the organization for the job request report")]
        public void ThenSelectTheOrganizationForTheJobRequestReport()
        {
            SelectElement selectRole = new SelectElement(webDriver.FindElement(By.Name("organization")));
            selectRole.SelectByText(ExcelLibrary.ReadData(1, "Organization"));
        }

        [Then(@"Select the client for the job request report")]
        public void ThenSelectTheClientForTheJobRequestReport()
        {
            SelectElement selectRole = new SelectElement(webDriver.FindElement(By.Name("client")));
            selectRole.SelectByText(ExcelLibrary.ReadData(1, "Client"));
        }

        [Then(@"Select the status for the job request report")]
        public void ThenSelectTheStatusForTheJobRequestReport()
        {
            SelectElement selectRole = new SelectElement(webDriver.FindElement(By.Name("statusFilter")));
            selectRole.SelectByText(ExcelLibrary.ReadData(1, "Status"));
        }

        [Then(@"Select the technology for the job request report")]
        public void ThenSelectTheTechnologyForTheJobRequestReport()
        {
            SelectElement selectRole = new SelectElement(webDriver.FindElement(By.Name("technology")));
            selectRole.SelectByText(ExcelLibrary.ReadData(1, "Technology"));
        }

        [Then(@"Select the certainty for the job request report")]
        public void ThenSelectTheCertaintyForTheJobRequestReport()
        {
            SelectElement selectRole = new SelectElement(webDriver.FindElement(By.Name("resCertainity")));
            selectRole.SelectByText(ExcelLibrary.ReadData(1, "Certainty"));
        }

        [Then(@"Select the staffing type for the job request report")]
        public void ThenSelectTheStaffingTypeForTheJobRequestReport()
        {
            SelectElement selectRole = new SelectElement(webDriver.FindElement(By.Name("stuffingType")));
            selectRole.SelectByText(ExcelLibrary.ReadData(1, "StaffingTypes"));
        }

        [Then(@"Select the created by for the job request report")]
        public void ThenSelectTheCreatedByForTheJobRequestReport()
        {
            SelectElement selectRole = new SelectElement(webDriver.FindElement(By.Name("jbRqstFltrCreatedBy")));
            selectRole.SelectByText(ExcelLibrary.ReadData(1, "CreatedBy"));
        }

        [Then(@"Select the date range")]
        public void ThenSelectTheDateRange()
        {
            webDriver.FindElement(By.CssSelector("img[onclick*=jbRqstFilterFrmDt]")).Click();
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            SelectElement selectFrmMonth = new SelectElement(webDriver.FindElement(By.CssSelector("select[onchange*=changeMonth]")));
            selectFrmMonth.SelectByIndex(1);
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            webDriver.FindElement(By.CssSelector("a[href*=changeDay]")).Click();

            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            webDriver.FindElement(By.CssSelector("img[onclick*=jbRqstFilterToDt]")).Click();
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            SelectElement selectToMonth = new SelectElement(webDriver.FindElement(By.CssSelector("select[onchange*=changeMonth]")));
            selectToMonth.SelectByIndex(11);
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            webDriver.FindElement(By.CssSelector("a[href*=changeDay]")).Click();
        }

        [Then(@"Select the filter based on NeededBy, CreatedOn, ModifiedOn date range")]
        public void ThenSelectTheFilterBasedOnNeededByCreatedOnModifiedOnDateRange()
        {
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            SelectElement dateCriteria = new SelectElement(webDriver.FindElement(By.Name("jbRqstFilterDtCriteria")));
            dateCriteria.SelectByText(ExcelLibrary.ReadData(1, "DateCriteria"));
        }

        [Then(@"Generate the excel report of the job requests")]
        public void ThenGenerateTheExcelReportOfTheJobRequests()
        {
            Thread.Sleep(2000);
            webDriver.FindElement(By.CssSelector("a[href*=exportToExcel]")).Click();
        }

        [Then(@"Select the all job requests export data option")]
        public void ThenSelectTheAllJobRequestsExportDataOption()
        {
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            webDriver.FindElement(By.CssSelector("input[value=AllRequest]")).Click();
            webDriver.FindElement(By.CssSelector("a[href*=exportDataFunc]")).Click();
            SpecHooks.extentTest.Pass("Export of all the job requests was successful");
        }

        [Then(@"Select the external hiring job requests export data option")]
        public void ThenSelectTheExternalHiringJobRequestsExportDataOption()
        {
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            webDriver.FindElement(By.CssSelector("input[value=ExternalRequests]")).Click();
            webDriver.FindElement(By.CssSelector("a[href*=exportDataFunc]")).Click();
            SpecHooks.extentTest.Pass("Export of all the job requests was successful");
        }

        [Then(@"Select the action required job requests export data option")]
        public void ThenSelectTheActionRequiredJobRequestsExportDataOption()
        {
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            webDriver.FindElement(By.CssSelector("input[value=ActionRequired]")).Click();
            webDriver.FindElement(By.CssSelector("a[href*=exportDataFunc]")).Click();
            SpecHooks.extentTest.Pass("Export of all the job requests was successful");
        }


    }
}
