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
    public class CreateJobRequestSteps
    {
        private IWebDriver webDriver;

        public CreateJobRequestSteps()
        {
            ExcelLibrary.PopulateInCollection(ConfigurationManager.AppSettings["CreateJobRequestFilePath"]);
        }

        [Given(@"Launch the web browser")]
        public void GivenLaunchTheWebBrowser()
        {
            try
            {
                webDriver = new ChromeDriver();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Given(@"Open SVCareers website")]
        public void GivenOpenSVCareersWebsite()
        {
            try
            {
                webDriver.Navigate().GoToUrl(ConfigurationManager.AppSettings["SVCareersURL"].ToString());
                webDriver.Manage().Window.Maximize();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Given(@"Type username and password in the login form")]
        public void GivenTypeUsernameAndPasswordInTheLoginForm()
        {
            try
            {
                Thread.Sleep(2000);
                webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSMainFrame")));
                webDriver.FindElement(By.Name("loginId")).SendKeys(ExcelLibrary.ReadData(1, "UserName"));
                webDriver.FindElement(By.Name("loginScreenPassword")).SendKeys(ExcelLibrary.ReadData(1, "Password"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Then(@"Click on Login submit button")]
        public void ThenClickOnLoginSubmitButton()
        {
            try
            {
                IWebElement elLoginBtn = webDriver.FindElement(By.Id("svCareersLoginId"));
                elLoginBtn.Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);

                if (webDriver.FindElements(By.ClassName("Mandatory")).Count > 0)
                {
                    if (webDriver.FindElement(By.ClassName("Mandatory")).Text != "")
                    {
                        SpecHooks.extentTest.Fail("Invalid username or password");
                        Thread.Sleep(5000);
                        webDriver.Close();
                        webDriver.Quit();
                    }
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Given(@"Check if the user is authroized to create a new job request")]
        public void GivenCheckIfTheUserIsAuthroizedToCreateANewJobRequest()
        {
            try
            {
                Thread.Sleep(2000);
                if (webDriver.FindElements(By.CssSelector("a[href*=createJbRqst]")).Count == 0)
                {
                    SpecHooks.extentTest.Fail("User is not authorized to create a new job request");
                }
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Click on Create Job Request link")]
        public void ThenClickOnCreateJobRequestLink()
        {
            try
            {
                Thread.Sleep(2000);
                webDriver.FindElement(By.CssSelector("a[href*=createJbRqst]")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Switch the focus to the Create new Job Request form popup")]
        public void ThenSwitchTheFocusToTheCreateNewJobRequestFormPopup()
        {
            try
            {
                Thread.Sleep(2000);
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                Thread.Sleep(2000);
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select the organization")]
        public void ThenSelectTheOrganization()
        {
            try
            {
                
                if (ExcelLibrary.ReadData(1, "Organization") != "")
                {
                    Thread.Sleep(2000);
                    SelectElement selectOrganization = new SelectElement(webDriver.FindElement(By.Name("organization")));
                    selectOrganization.SelectByText(ExcelLibrary.ReadData(1, "Organization"));
                    Thread.Sleep(1000);
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select the Job location")]
        public void ThenSelectTheJobLocation()
        {
            try
            {
                if (ExcelLibrary.ReadData(1, "JobLocation") != "") {
                    Thread.Sleep(2000);
                    SelectElement selectJobLocation = new SelectElement(webDriver.FindElement(By.Name("location")));
                    selectJobLocation.SelectByText(ExcelLibrary.ReadData(1, "JobLocation"));
                    Thread.Sleep(2000);
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select client name")]
        public void ThenSelectClientName()
        {
            try
            {
                if (ExcelLibrary.ReadData(1, "NewClient") != "")
                {
                    Thread.Sleep(2000);
                    if (ExcelLibrary.ReadData(1, "NewClient") != "")
                    {
                        webDriver.FindElement(By.CssSelector("a[onclick*=newclient]")).Click();
                        webDriver.FindElement(By.CssSelector("input[name=newClient]")).SendKeys(ExcelLibrary.ReadData(1, "NewClient"));
                    }

                }
                else
                {
                    Thread.Sleep(2000);
                    if (ExcelLibrary.ReadData(1, "Client") != "")
                    {
                        SelectElement selectClient = new SelectElement(webDriver.FindElement(By.Name("client")));
                        selectClient.SelectByText(ExcelLibrary.ReadData(1, "Client"));
                    }
                }

                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select project name")]
        public void ThenSelectProjectName()
        {
            try
            {
                Thread.Sleep(2000);
                if (webDriver.FindElements(By.CssSelector("a[onclick*=oldclient]")).Count > 0)
                {
                    if(ExcelLibrary.ReadData(1, "NewProject") != "")
                    webDriver.FindElement(By.CssSelector("input[name=projectTitleForNew]")).SendKeys(ExcelLibrary.ReadData(1, "NewProject"));
                }
                else if (webDriver.FindElements(By.CssSelector("a[onclick*=newproject]")).Count > 0)
                {
                    if (ExcelLibrary.ReadData(1, "Project") != "")
                    {
                        SelectElement selectProject = new SelectElement(webDriver.FindElement(By.Name("projectTitle")));
                        selectProject.SelectByText(ExcelLibrary.ReadData(1, "Project"));
                    }
                }
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select the technology")]
        public void ThenSelectTheTechnology()
        {
            try
            {
                Thread.Sleep(2000);
                if (ExcelLibrary.ReadData(1, "Technology") != "")
                {
                    SelectElement selectTechnology = new SelectElement(webDriver.FindElement(By.Name("technology")));
                    selectTechnology.SelectByText(ExcelLibrary.ReadData(1, "Technology"));
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                }
            }
            catch (Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select the Role")]
        public void ThenSelectTheRole()
        {
            try
            {
                Thread.Sleep(2000);
                if (ExcelLibrary.ReadData(1, "Role") != "")
                {
                    SelectElement selectRole = new SelectElement(webDriver.FindElement(By.Name("jobTitle")));
                    selectRole.SelectByText(ExcelLibrary.ReadData(1, "Role"));
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select the Job title")]
        public void ThenSelectTheJobTitle()
        {
            try
            {
                Thread.Sleep(2000);
                if (ExcelLibrary.ReadData(1, "JobTitle") != "")
                {
                    SelectElement selectJobTile = new SelectElement(webDriver.FindElement(By.Name("roleDesc")));
                    selectJobTile.SelectByText(ExcelLibrary.ReadData(1, "JobTitle"));
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Search for Mandatory skills")]
        public void ThenSearchForMandatorySkills()
        {
            try
            {
                Thread.Sleep(2000);
                webDriver.FindElement(By.CssSelector("a[href*=reqSkills]")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch (Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Switch the focus to the Madatory skills popup")]
        public void ThenSwitchTheFocusToTheMadatorySkillsPopup()
        {
            try
            {
                Thread.Sleep(2000);
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                Thread.Sleep(2000);
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select the Mandatory skills")]
        public void ThenSelectTheMandatorySkills()
        {
            try
            {
                if (ExcelLibrary.ReadData(1, "MandatorySkills") != "")
                {
                    string[] mandatorySkills = ExcelLibrary.ReadData(1, "MandatorySkills").Split(new char[1] { ',' });
                    for (int i = 0; i < mandatorySkills.Length; i++)
                    {
                        Thread.Sleep(2000);
                        SelectElement selectMandatorySkills = new SelectElement(webDriver.FindElement(By.Name("srcList")));
                        selectMandatorySkills.SelectByText(mandatorySkills[i]);
                        webDriver.FindElement(By.CssSelector("a[href*=addSrcToDestList]")).Click();
                    }

                    webDriver.FindElement(By.CssSelector("a[href*=skillDone]")).Click();
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
            
        }

        [Then(@"Search for Nice to have skills")]
        public void ThenSearchForNiceToHaveSkills()
        {
            try
            {
                Thread.Sleep(2000);
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                webDriver.FindElement(By.CssSelector("a[href*=prefSkills]")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Switch the focus to the Nice to have skills popup")]
        public void ThenSwitchTheFocusToTheNiceToHaveSkillsPopup()
        {
            try
            {
                Thread.Sleep(2000);
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                Thread.Sleep(2000);
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select the Nice to have skills")]
        public void ThenSelectTheNiceToHaveSkills()
        {
            try
            {
                if (ExcelLibrary.ReadData(1, "NiceToHaveSkills") != "")
                {
                    string[] niceToHaveSkills = ExcelLibrary.ReadData(1, "NiceToHaveSkills").Split(new char[1] { ',' });
                    for (int i = 0; i < niceToHaveSkills.Length; i++)
                    {
                        Thread.Sleep(2000);
                        SelectElement selectMandatorySkills = new SelectElement(webDriver.FindElement(By.Name("srcList")));
                        selectMandatorySkills.SelectByText(niceToHaveSkills[i]);
                        webDriver.FindElement(By.CssSelector("a[href*=addSrcToDestList]")).Click();
                    }

                    webDriver.FindElement(By.CssSelector("a[href*=skillDone]")).Click();
                }
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Enter the number of resources required")]
        public void ThenEnterTheNumberOfResourcesRequired()
        {
            try
            {
                if (ExcelLibrary.ReadData(1, "ResourcesRequired") != "")
                {
                    Thread.Sleep(2000);
                    webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                    webDriver.FindElement(By.CssSelector("input[name=noOfCandidatesNeeded]")).SendKeys(ExcelLibrary.ReadData(1, "ResourcesRequired"));
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select the date by which the candidate is required")]
        public void ThenSelectTheDateByWhichTheCandidateIsRequired()
        {
            try
            {
                if (ExcelLibrary.ReadData(1, "CandidatesNeededBy") != "")
                {
                    webDriver.FindElement(By.CssSelector("img[onclick*=candidatesNeededBy]")).Click();
                    webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());

                    string candidateRequiredDate = ExcelLibrary.ReadData(1, "CandidatesNeededBy");
                    string[] candidateRequiredDateArr = candidateRequiredDate.Split(new char[] { '/' });
                    string day = candidateRequiredDateArr[1];
                    string month = candidateRequiredDateArr[0];
                    string year = candidateRequiredDateArr[2].Substring(0, 4);

                    SelectElement selectMonth = new SelectElement(webDriver.FindElement(By.CssSelector("select[onchange*=changeMonth]")));
                    selectMonth.SelectByIndex(Convert.ToInt32(month) - 1);
                    SelectElement selectYear = new SelectElement(webDriver.FindElement(By.CssSelector("select[onchange*=changeYear]")));
                    selectYear.SelectByText(year);
                    webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                    webDriver.FindElement(By.LinkText(day)).Click();
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select the billable status")]
        public void ThenSelectTheBillableStatus()
        {
            try
            {
                if (ExcelLibrary.ReadData(1, "BillabilityStatus") != "")
                {
                    Thread.Sleep(2000);
                    webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                    SelectElement selectBillableStatus = new SelectElement(webDriver.FindElement(By.Name("billabilityStatus")));
                    selectBillableStatus.SelectByText(ExcelLibrary.ReadData(1, "BillabilityStatus"));
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select the billedTypes")]
        public void ThenSelectTheBilledTypes()
        {
            try
            {
                if (ExcelLibrary.ReadData(1, "BillabilityType") != "")
                {
                    Thread.Sleep(2000);
                    webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                    SelectElement selectBillableType = new SelectElement(webDriver.FindElement(By.Name("billedTypes")));
                    selectBillableType.SelectByText(ExcelLibrary.ReadData(1, "BillabilityType"));
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }


        [Then(@"Select if client interview is required")]
        public void ThenSelectIfClientInterviewIsRequired()
        {
            try
            {
                if (ExcelLibrary.ReadData(1, "ClientInterview") != "")
                {
                    Thread.Sleep(2000);
                    SelectElement selectClientInterview = new SelectElement(webDriver.FindElement(By.Name("clientInterview")));
                    selectClientInterview.SelectByText(ExcelLibrary.ReadData(1, "ClientInterview"));
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select the certainity")]
        public void ThenSelectTheCertainity()
        {
            if (ExcelLibrary.ReadData(1, "Certainity") != "")
            {
                Thread.Sleep(2000);
                SelectElement selectClientInterview = new SelectElement(webDriver.FindElement(By.Name("resCertainity")));
                selectClientInterview.SelectByText(ExcelLibrary.ReadData(1, "Certainity"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Enter the max budget")]
        public void ThenEnterTheMaxBudget()
        {
            if (ExcelLibrary.ReadData(1, "BudgetUpto") != "")
            {
                Thread.Sleep(2000);
                webDriver.FindElement(By.CssSelector("input[name=jbRqstBudget]")).SendKeys(ExcelLibrary.ReadData(1, "BudgetUpto"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Enter the job description")]
        public void ThenEnterTheJobDescription()
        {
            if (ExcelLibrary.ReadData(1, "JobDescription") != "")
            {
                Thread.Sleep(2000);
                webDriver.FindElement(By.CssSelector("textarea[name=comments]")).SendKeys(ExcelLibrary.ReadData(1, "JobDescription"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Enter the billing rate")]
        public void ThenEnterTheBillingRate()
        {
            if (ExcelLibrary.ReadData(1, "BillingRate") != "") {
                Thread.Sleep(2000);
                webDriver.FindElement(By.CssSelector("input[name=billingRate]")).SendKeys(ExcelLibrary.ReadData(1, "BillingRate"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Select the billing start date")]
        public void ThenSelectTheBillingStartDate()
        {
            if (ExcelLibrary.ReadData(1, "BillingStartDate") != "")
            {
                webDriver.FindElement(By.CssSelector("img[onclick*=billingStartDt]")).Click();
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                Thread.Sleep(2000);
                string billingDate = ExcelLibrary.ReadData(1, "BillingStartDate");
                string[] billingDateArr = billingDate.Split(new char[] { '/' });
                string day = billingDateArr[1];
                string month = billingDateArr[0];
                string year = billingDateArr[2].Substring(0, 4);

                SelectElement selectMonth = new SelectElement(webDriver.FindElement(By.CssSelector("select[onchange*=changeMonth]")));
                selectMonth.SelectByIndex(Convert.ToInt32(month) - 1);
                SelectElement selectYear = new SelectElement(webDriver.FindElement(By.CssSelector("select[onchange*=changeYear]")));
                selectYear.SelectByText(year);
                webDriver.FindElement(By.LinkText(day)).Click();
                Thread.Sleep(2000);
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Select the requirement type")]
        public void ThenSelectTheRequirementType()
        {
            if (ExcelLibrary.ReadData(1, "RequirementType") != "")
            {
                Thread.Sleep(2000);
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                SelectElement selectMandatorySkills = new SelectElement(webDriver.FindElement(By.Name("reqTypeExp")));
                selectMandatorySkills.SelectByText(ExcelLibrary.ReadData(1, "RequirementType"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Search for account manager")]
        public void ThenSearchForAccountManager()
        {
            webDriver.FindElement(By.CssSelector("a[href*=searchPopupManager]")).Click();
            SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
        }

        [Then(@"Switch the focus to the Account manager popup")]
        public void ThenSwitchTheFocusToTheAccountManagerPopup()
        {
            Thread.Sleep(2000);
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            Thread.Sleep(2000);
            SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
        }

        [Then(@"Select the account manager")]
        public void ThenSelectTheAccountManager()
        {
            if (ExcelLibrary.ReadData(1, "AccountManager") != "")
            {
                Thread.Sleep(2000);
                webDriver.FindElement(By.CssSelector("input[name=txtSearchKey]")).SendKeys(ExcelLibrary.ReadData(1, "AccountManager"));
                webDriver.FindElement(By.CssSelector("a[href*=search]")).Click();
                Thread.Sleep(2000);
                webDriver.FindElement(By.CssSelector(".NewTableDataOdd input[type=checkbox]")).Click();
                webDriver.FindElement(By.CssSelector("a[href*=add]")).Click();
                Thread.Sleep(2000);
                webDriver.FindElement(By.CssSelector("a[href*=done]")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Search for reporting manager")]
        public void ThenSearchForReportingManager()
        {
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            webDriver.FindElement(By.CssSelector("a[href*=searchReportingManager]")).Click();
            SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
        }

        [Then(@"Switch the focus to the Reporting manager popup")]
        public void ThenSwitchTheFocusToTheReportingManagerPopup()
        {
            Thread.Sleep(2000);
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            Thread.Sleep(2000);
        }

        [Then(@"Select the reporting manager")]
        public void ThenSelectTheReportingManager()
        {
            if (ExcelLibrary.ReadData(1, "ReportingManager") != "")
            {
                webDriver.FindElement(By.CssSelector("input[name=txtSearchKey]")).SendKeys(ExcelLibrary.ReadData(1, "ReportingManager"));
                Thread.Sleep(2000);
                webDriver.FindElement(By.CssSelector("a[href*=search]")).Click();
                webDriver.FindElement(By.CssSelector(".NewTableDataOdd input[type=checkbox]")).Click();
                Thread.Sleep(2000);
                webDriver.FindElement(By.CssSelector("a[href*=add]")).Click();
                Thread.Sleep(2000);
                webDriver.FindElement(By.CssSelector("a[href*=done]")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Enter the minimum experience")]
        public void ThenEnterTheMinimumExperience()
        {
            if (ExcelLibrary.ReadData(1, "MinimumExperience") != "")
            {
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                webDriver.FindElement(By.CssSelector("input[name=minExperience]")).SendKeys(ExcelLibrary.ReadData(1, "MinimumExperience"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Enter the preferred experience")]
        public void ThenEnterThePreferredExperience()
        {
            if (ExcelLibrary.ReadData(1, "PreferredExperience") != "")
            {
                Thread.Sleep(2000);
                webDriver.FindElement(By.CssSelector("input[name=preExperience]")).SendKeys(ExcelLibrary.ReadData(1, "PreferredExperience"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Enter the seat ID")]
        public void ThenEnterTheSeatID()
        {
            if (ExcelLibrary.ReadData(1, "SeatID") != "")
            {
                Thread.Sleep(2000);
                webDriver.FindElement(By.CssSelector("input[name=seatIds]")).SendKeys(ExcelLibrary.ReadData(1, "SeatID"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Search for any certifications")]
        public void ThenSearchForAnyCertifications()
        {
            Thread.Sleep(2000);
            webDriver.FindElement(By.CssSelector("a[href*=showLicense]")).Click();
            SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
        }

        [Then(@"Switch the focus to the certifications popup")]
        public void ThenSwitchTheFocusToTheCertificationsPopup()
        {
            Thread.Sleep(2000);
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            Thread.Sleep(2000);
        }

        [Then(@"Select the required certification")]
        public void ThenSelectTheRequiredCertification()
        {
            if (ExcelLibrary.ReadData(1, "Certifications") != "")
            {
                Thread.Sleep(2000);
                string[] niceToHaveSkills = ExcelLibrary.ReadData(1, "Certifications").Split(new char[1] { ',' });
                for (int i = 0; i < niceToHaveSkills.Length; i++)
                {
                    SelectElement selectMandatorySkills = new SelectElement(webDriver.FindElement(By.Name("srcList")));
                    selectMandatorySkills.SelectByText(niceToHaveSkills[i]);
                    webDriver.FindElement(By.CssSelector("a[href*=addSrcToDestList]")).Click();
                }
                Thread.Sleep(2000);
                webDriver.FindElement(By.CssSelector("a[href*=licenseDone]")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Enter the work location")]
        public void ThenEnterTheWorkLocation()
        {
            if (ExcelLibrary.ReadData(1, "WorkLocation") != "")
            {
                Thread.Sleep(2000);
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                webDriver.FindElement(By.CssSelector("input[name=wrkLocation]")).SendKeys(ExcelLibrary.ReadData(1, "WorkLocation"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Enter the project duration")]
        public void ThenEnterTheProjectDuration()
        {
            if (ExcelLibrary.ReadData(1, "ProjectDurationInMonths") != "")
            {
                Thread.Sleep(2000);
                webDriver.FindElement(By.CssSelector("input[name=projectDuration]")).SendKeys(ExcelLibrary.ReadData(1, "ProjectDurationInMonths"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Select will the resource lead a team Yes or No")]
        public void ThenSelectWillTheResourceLeadATeamYesOrNo()
        {
            if (ExcelLibrary.ReadData(1, "WillResourceLeadATeam") != "")
            {
                Thread.Sleep(2000);
                SelectElement selectIsLead = new SelectElement(webDriver.FindElement(By.Name("directOffshoreTeam")));
                selectIsLead.SelectByText(ExcelLibrary.ReadData(1, "WillResourceLeadATeam"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Select will the resume be sent to client Yes or No")]
        public void ThenSelectWillTheResumeBeSentToClientYesOrNo()
        {
            if (ExcelLibrary.ReadData(1, "WillResumeBeSubmittedToClient") != "")
            {
                Thread.Sleep(2000);
                SelectElement selectIsResumeSentToClient = new SelectElement(webDriver.FindElement(By.Name("resumeToClient")));
                selectIsResumeSentToClient.SelectByText(ExcelLibrary.ReadData(1, "WillResumeBeSubmittedToClient"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Select system type")]
        public void ThenSelectSystemType()
        {
            if (ExcelLibrary.ReadData(1, "SystemType") != "")
            {
                Thread.Sleep(2000);
                SelectElement selectSystemType = new SelectElement(webDriver.FindElement(By.Name("systemType")));
                selectSystemType.SelectByText(ExcelLibrary.ReadData(1, "SystemType"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Select configurations")]
        public void ThenSelectConfigurations()
        {
            if (ExcelLibrary.ReadData(1, "Configurations") != "")
            {
                Thread.Sleep(2000);
                SelectElement selectConfiguration = new SelectElement(webDriver.FindElement(By.Name("configuration")));
                selectConfiguration.SelectByText(ExcelLibrary.ReadData(1, "Configurations"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Enter the comments if any for the job request")]
        public void ThenEnterTheCommentsIfAnyForTheJobRequest()
        {
            if (ExcelLibrary.ReadData(1, "Comments") != "")
            {
                webDriver.FindElement(By.CssSelector("textarea[name=addComments]")).SendKeys(ExcelLibrary.ReadData(1, "Comments"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
        }

        [Then(@"Submit the job request form")]
        public void ThenSubmitTheJobRequestForm()
        {
            try
            {
                webDriver.FindElement(By.CssSelector("a[href*=done]")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                Thread.Sleep(2000);
                if (ExpectedConditions.AlertIsPresent()(webDriver) != null)
                {
                    Thread.Sleep(2000);
                    IAlert alert = webDriver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    SpecHooks.extentTest.Fail(alertText);
                    alert.Accept();
                    
                }
                else
                {
                    Thread.Sleep(4000);
                    webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                    webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSHeader")));
                    webDriver.FindElement(By.CssSelector("a[title=Logout]")).Click();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Thread.Sleep(5000);
                webDriver.Quit();
            }
        }

    }
}
