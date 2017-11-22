using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SVCareers_Automation_Testing_Project.Hooks;
using SVCareers_Automation_Testing_Project.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;

namespace SVCareers_Automation_Testing_Project.StepDefinitions
{
    [Binding]
    public class AddToRequisitionSteps
    {
        private IWebDriver webDriver;

        public AddToRequisitionSteps()
        {
            ExcelLibrary.PopulateInCollection(ConfigurationManager.AppSettings["AddToRequisitionFilePath"]);
        }

        [Given(@"Launch the chrome browser window")]
        public void GivenLaunchTheChromeBrowserWindow()
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
        
        [Given(@"Open SVCareers website in the chrome browser")]
        public void GivenOpenSVCareersWebsiteInTheChromeBrowser()
        {
            try
            {
                webDriver.Navigate().GoToUrl("http://spicareers-uat/spicareers/");
                webDriver.Manage().Window.Maximize();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Given(@"Enter the user details in the login form")]
        public void GivenEnterTheUserDetailsInTheLoginForm()
        {
            try
            {
                Thread.Sleep(2000);
                webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSMainFrame")));
                webDriver.FindElement(By.Name("loginId")).SendKeys(ExcelLibrary.ReadData(1, "UserName"));

                IWebElement elPassword = webDriver.FindElement(By.Name("loginScreenPassword"));
                elPassword.SendKeys(ExcelLibrary.ReadData(1, "Password"));

                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Then(@"Login to the SVCareers website with the user details provided")]
        public void ThenLoginToTheSVCareersWebsiteWithTheUserDetailsProvided()
        {
            try
            {
                webDriver.FindElement(By.Id("svCareersLoginId")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Click on Home tab to get the job requests grid")]
        public void ThenClickOnHomeTabToGetTheJobRequestsGrid()
        {
            try
            {
                Thread.Sleep(2000);
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSHeader")));
                webDriver.FindElement(By.ClassName("menuhome")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select the job request with external resource flag set to one and requesition status is not posted")]
        public void ThenSelectTheJobRequestWithExternalResourceFlagSetToOneAndRequesitionStatusIsNotPosted()
        {
            try
            {
                int isJobReqSelected = 0;
                do
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
                        if (index > 1)
                        {
                            rowTD = row.FindElements(By.TagName("td"));
                            if (rowTD.Count > 22)
                            {
                                if (rowTD[12].Text.Equals("1") && (!rowTD[1].Text.ToLower().Contains("posted")))
                                {
                                    rowTD[0].Click();
                                    isJobReqSelected = 1;
                                    break;
                                }
                            }
                        }
                    }

                    if (webDriver.FindElements(By.CssSelector("a[href*=next]")).Count > 0 && isJobReqSelected == 0)
                    {
                        webDriver.FindElement(By.CssSelector("a[href*=next]")).Click();
                    }
                    else if (isJobReqSelected == 1)
                    {
                        break;
                    }
                    else
                    {
                        isJobReqSelected = 1;
                        SpecHooks.extentTest.Warning("There are no job requests available to create the requisition");
                        break;
                    }

                } while (isJobReqSelected == 0);
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }

        }

        [Then(@"Click on add to requisition link")]
        public void ThenClickOnAddToRequisitionLink()
        {
            try
            {
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSMainFrame")));
                webDriver.FindElement(By.CssSelector("a[href*=createReqFrmJbRqst]")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Enter the client code")]
        public void ThenEnterTheClientCode()
        {
            try
            {
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSMainFrame")));
                webDriver.FindElement(By.Name("clientJobCode")).SendKeys(ExcelLibrary.ReadData(1, "ClientJobCode"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select the priority")]
        public void ThenSelectThePriority()
        {
            try
            {
                SelectElement selectPriority = new SelectElement(webDriver.FindElement(By.Name("priority")));
                selectPriority.SelectByText(ExcelLibrary.ReadData(1, "Priority"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Enter the billing rate per hour")]
        public void ThenEnterTheBillingRatePerHour()
        {
            try
            {
                webDriver.FindElement(By.Name("billingRate")).SendKeys(ExcelLibrary.ReadData(1, "BillingRate"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Enter the duration of the project")]
        public void ThenEnterTheDurationOfTheProject()
        {
            try
            {
                webDriver.FindElement(By.Name("prjDuration")).SendKeys(ExcelLibrary.ReadData(1, "ProjectDuration"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Enter if the referral be rewarded")]
        public void ThenEnterIfTheReferralBeRewarded()
        {
            try
            {
                SelectElement selectIfReferralBeRewarded = new SelectElement(webDriver.FindElement(By.Name("referral")));
                selectIfReferralBeRewarded.SelectByText(ExcelLibrary.ReadData(1, "RewardsForReferral"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Enter the date initiated")]
        public void ThenEnterTheDateInitiated()
        {
            try
            {
                webDriver.FindElement(By.Name("dateInitiated")).Clear();
                webDriver.FindElement(By.Name("dateInitiated")).SendKeys(ExcelLibrary.ReadData(1, "DateInitiated"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Enter the candidate needed by")]
        public void ThenEnterTheCandidateNeededBy()
        {
            try
            {
                webDriver.FindElement(By.Name("targetStartDate")).Clear();
                webDriver.FindElement(By.Name("targetStartDate")).SendKeys(ExcelLibrary.ReadData(1, "CandidateNeededBy"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Enter requisition type")]
        public void ThenEnterRequisitionType()
        {
            try
            {
                SelectElement selectRequisitionType = new SelectElement(webDriver.FindElement(By.Name("requisitionType")));
                selectRequisitionType.SelectByText(ExcelLibrary.ReadData(1, "RequisitionType"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Enter the count planned")]
        public void ThenEnterTheCountPlanned()
        {
            try
            {
                webDriver.FindElement(By.Name("countPlanned")).SendKeys(ExcelLibrary.ReadData(1, "CountPlanned"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Enter the total number of positions planned")]
        public void ThenEnterTheTotalNumberOfPositionsPlanned()
        {
            try
            {
                webDriver.FindElement(By.Name("totalNumberOfPositions")).SendKeys(ExcelLibrary.ReadData(1, "NoOfPositions"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select the Rationale")]
        public void ThenSelectTheRationale()
        {
            try
            {
                SelectElement selectRationale = new SelectElement(webDriver.FindElement(By.Name("justification")));
                selectRationale.SelectByText(ExcelLibrary.ReadData(1, "Rationale"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Enter the recruiter name")]
        public void ThenEnterTheRecruiterName()
        {
            try
            {
                webDriver.FindElement(By.Name("recruiterName")).SendKeys(ExcelLibrary.ReadData(1, "Recruiter"));
                Thread.Sleep(1000);
                webDriver.FindElement(By.CssSelector("select[name=recruiterIds] option:nth-child(1)")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
            
        }

        [Then(@"Enter the hiring manager name")]
        public void ThenEnterTheHiringManagerName()
        {
            try
            {
                webDriver.FindElement(By.Name("hiringManager")).SendKeys(ExcelLibrary.ReadData(1, "HiringManager"));
                Thread.Sleep(1000);
                webDriver.FindElement(By.CssSelector("select[name=hiringManagerIds] option:nth-child(1)")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Move to job description screen")]
        public void ThenMoveToJobDescriptionScreen()
        {
            try
            {
                webDriver.FindElement(By.CssSelector("a[href*=jobDetailsNext]")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Move to interview management screen")]
        public void ThenMoveToInterviewManagementScreen()
        {
            try
            {
                webDriver.FindElement(By.CssSelector("a[href*=jobDescriptionNext]")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Click on Add interview rounds button")]
        public void ThenClickOnAddInterviewRoundsButton()
        {
            try
            {
                Thread.Sleep(3000);
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSMainFrame")));
                webDriver.FindElement(By.CssSelector("a[href*=addIntervieweRnd]")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [When(@"Switch focus to interview round popup")]
        public void WhenSwitchFocusToInterviewRoundPopup()
        {
            try
            {
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Add the interview rounds and submit")]
        public void ThenAddTheInterviewRoundsAndSubmit()
        {
            try
            {
                int roundIndex = 0;
                if (ExcelLibrary.ReadData(1, "InterviewType1") != "")
                {
                    roundIndex = roundIndex + 1;
                    SelectElement selectInterviewRoundType1 = new SelectElement(webDriver.FindElement(By.Name("requisitionForm[0].slctdIntrvType[0]")));
                    selectInterviewRoundType1.SelectByText(ExcelLibrary.ReadData(1, "InterviewType1"));

                    SelectElement selectInterviewRound1 = new SelectElement(webDriver.FindElement(By.Name("requisitionForm[0].slctdIntrvRnd[0]")));
                    selectInterviewRound1.SelectByText(roundIndex.ToString());
                }

                if (ExcelLibrary.ReadData(1, "InterviewType2") != "")
                {
                    roundIndex = roundIndex + 1;
                    SelectElement selectInterviewRoundType2 = new SelectElement(webDriver.FindElement(By.Name("requisitionForm[1].slctdIntrvType[1]")));
                    selectInterviewRoundType2.SelectByText(ExcelLibrary.ReadData(1, "InterviewType2"));

                    SelectElement selectInterviewRound2 = new SelectElement(webDriver.FindElement(By.Name("requisitionForm[1].slctdIntrvRnd[1]")));
                    selectInterviewRound2.SelectByText(roundIndex.ToString());
                }

                if (ExcelLibrary.ReadData(1, "InterviewType3") != "")
                {
                    roundIndex = roundIndex + 1;
                    SelectElement selectInterviewRoundType3 = new SelectElement(webDriver.FindElement(By.Name("requisitionForm[2].slctdIntrvType[2]")));
                    selectInterviewRoundType3.SelectByText(ExcelLibrary.ReadData(1, "InterviewType3"));

                    SelectElement selectInterviewRound3 = new SelectElement(webDriver.FindElement(By.Name("requisitionForm[2].slctdIntrvRnd[2]")));
                    selectInterviewRound3.SelectByText(roundIndex.ToString());
                }

                if (ExcelLibrary.ReadData(1, "InterviewType4") != "")
                {
                    roundIndex = roundIndex + 1;
                    SelectElement selectInterviewRoundType4 = new SelectElement(webDriver.FindElement(By.Name("requisitionForm[3].slctdIntrvType[3]")));
                    selectInterviewRoundType4.SelectByText(ExcelLibrary.ReadData(1, "InterviewType4"));

                    SelectElement selectInterviewRound4 = new SelectElement(webDriver.FindElement(By.Name("requisitionForm[3].slctdIntrvRnd[3]")));
                    selectInterviewRound4.SelectByText(roundIndex.ToString());
                }

                webDriver.FindElement(By.CssSelector("a[href*=saveIntrvRnds]")).Click();
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
            
        }

        [Then(@"Click on Add interviewer button")]
        public void ThenClickOnAddInterviewerButton()
        {
            try
            {
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSMainFrame")));
                webDriver.FindElement(By.CssSelector("a[href*=addInterviewer]")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [When(@"Switch focus to interviewer popup")]
        public void WhenSwitchFocusToInterviewerPopup()
        {
            try
            {
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Search and add the interviewer")]
        public void ThenSearchAndAddTheInterviewer()
        {
            try
            {
                webDriver.FindElement(By.CssSelector("input[name=txtSearchKey]")).SendKeys(ExcelLibrary.ReadData(1, "Interviewer"));
                webDriver.FindElement(By.CssSelector("a[href*=search]")).Click();
                webDriver.FindElement(By.CssSelector("table.GreenBorder tr.NewTableDataOdd input[type=checkbox]")).Click();
                webDriver.FindElement(By.CssSelector("a[href*=add]")).Click();
                webDriver.FindElement(By.CssSelector("a[href*=done]")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Move to requisition summary page")]
        public void ThenMoveToRequisitionSummaryPage()
        {
            try
            {
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSMainFrame")));
                webDriver.FindElement(By.CssSelector("a img[title=Next]")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Save and post the requisition request")]
        public void ThenSaveAndPostTheRequisitionRequest()
        {
            try
            {
                webDriver.FindElement(By.CssSelector("a[href*=post]")).Click();
                SpecHooks.extentTest.Pass("Job request has been added to requisition successfully.");
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Move to requisition history page")]
        public void ThenMoveToRequisitionHistoryPage()
        {
            try
            {
                webDriver.FindElement(By.CssSelector("a img[title=Next]")).Click();
                SpecHooks.extentTest.Pass("Job request has been added to requisition successfully.");
                Thread.Sleep(4000);
                webDriver.Close();
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Save and unpost the requisition request")]
        public void ThenSaveAndUnpostTheRequisitionRequest()
        {
            try
            {
                webDriver.FindElement(By.CssSelector("a[href*=unpost]")).Click();
                SpecHooks.extentTest.Pass("Job request which need to be added to requisition has been saved and not posted.");
                Thread.Sleep(4000);
                webDriver.Close();
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
    }
}
