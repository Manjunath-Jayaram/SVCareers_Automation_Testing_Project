using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SVCareers_Automation_Testing_Project.Hooks;
using SVCareers_Automation_Testing_Project.Model;
using System;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using System.Configuration;

namespace SVCareers_Automation_Testing_Project.StepDefinitions
{
    [Binding]
    public class ReferFriendSteps
    {
        public ReferFriendSteps()
        {
            ExcelLibrary.PopulateInCollection(ConfigurationManager.AppSettings["ReferFriendFilePath"]);
        }

        private IWebDriver webDriver;

        [Given(@"Launch the browser")]
        public void GivenLaunchTheBrowser()
        {
            try
            {
                webDriver = new ChromeDriver();
                webDriver.Manage().Window.Maximize();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Given(@"Open SVCareers websites")]
        public void GivenOpenSVCareersWebsites()
        {
            webDriver.Navigate().GoToUrl(ConfigurationManager.AppSettings["SVCareersURL"].ToString());
        }
        
        [Given(@"Type your username and password in the login form")]
        public void GivenTypeYourUsernameAndPasswordInTheLoginForm()
        {
            try
            {
                Thread.Sleep(2000);
                webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSMainFrame")));
                IWebElement elUserName = webDriver.FindElement(By.Name("loginId"));
                elUserName.SendKeys(ExcelLibrary.ReadData(1, "UserName"));

                IWebElement elPassword = webDriver.FindElement(By.Name("loginScreenPassword"));
                elPassword.SendKeys(ExcelLibrary.ReadData(1, "Password"));

                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Click on Login form submit button")]
        public void ThenClickOnLoginFormSubmitButton()
        {
            try
            {
                IWebElement elLoginBtn = webDriver.FindElement(By.Id("svCareersLoginId"));
                elLoginBtn.Click();

                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }


        [Then(@"Select the job you want to refer from the grid")]
        public void ThenSelectTheJobYouWantToReferFromTheGrid()
        {
            try
            {
                if (webDriver.FindElements(By.CssSelector("#ShowDesc")).Count > 0)
                {
                    IWebElement elJobRequest = webDriver.FindElement(By.CssSelector("#ShowDesc table tbody tr:nth-child(3) input[type=checkbox]"));
                    elJobRequest.Click();
                    Thread.Sleep(2000);

                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                }
                else
                {
                    SpecHooks.extentTest.Debug("No job requests found");
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Click on refer a friend link")]
        public void ThenClickOnReferAFriendLink()
        {
            try
            {
                IWebElement elReferFrd = webDriver.FindElement(By.CssSelector("#rowForReferAFriendToJob a"));
                elReferFrd.Click();

                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Given(@"Switch the focus to the candidate details form popup")]
        public void GivenSwitchTheFocusToTheCandidateDetailsFormPopup()
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

        [When(@"Switch the focus to the candidate details form popup")]
        public void WhenSwitchTheFocusToTheCandidateDetailsFormPopup()
        {
            try
            {
                Thread.Sleep(2000);
                webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                Thread.Sleep(2000);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }


        [Then(@"Enter candidate firstname")]
        public void ThenEnterCandidateFirstname()
        {
            try
            {
                webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=candFirstName]")).SendKeys(ExcelLibrary.ReadData(1, "FirstName"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Then(@"Enter candidate middlename")]
        public void ThenEnterCandidateMiddlename()
        {
            try
            {
                webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=candMiddleName]")).SendKeys(ExcelLibrary.ReadData(1, "MiddleName"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Enter candidate lastname")]
        public void ThenEnterCandidateLastname()
        {
            try
            {
                webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=candLastName]")).SendKeys(ExcelLibrary.ReadData(1, "LastName"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Select the gender")]
        public void ThenSelectTheGender()
        {
            try
            {
                webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input#" + ExcelLibrary.ReadData(1, "Gender"))).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch (Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Then(@"Enter the candidate email address")]
        public void ThenEnterTheCandidateEmailAddress()
        {
            try
            {
                webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=candEmailId]")).SendKeys(ExcelLibrary.ReadData(1, "Email"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Then(@"Enter the candidate phone number")]
        public void ThenEnterTheCandidatePhoneNumber()
        {
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=candHomePhone]")).SendKeys(ExcelLibrary.ReadData(1, "Phone"));
            try
            {
                if (ExpectedConditions.AlertIsPresent()(webDriver) != null)
                {
                    SpecHooks.extentTest.Pass("Candidate has already been referred");
                    Thread.Sleep(2000);
                    webDriver.Close();

                    Thread.Sleep(4000);
                    webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                    webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSHeader")));
                    webDriver.FindElement(By.CssSelector("a[title=Logout]")).Click();
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Then(@"Select the candidate type as experienced")]
        public void ThenSelectTheCandidateTypeAsExperienced()
        {
            try
            {
                if (ExpectedConditions.AlertIsPresent()(webDriver) != null)
                {
                    SpecHooks.extentTest.Pass("Candidate has already been referred");
                    Thread.Sleep(2000);
                    webDriver.Close();

                    Thread.Sleep(4000);
                    webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                    webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSHeader")));
                    webDriver.FindElement(By.CssSelector("a[title=Logout]")).Click();
                }
                else
                {
                    SelectElement selectCandidateType = new SelectElement(webDriver.FindElement(By.Name("candType")));
                    selectCandidateType.SelectByText(ExcelLibrary.ReadData(1, "CandidateType"));
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                    Thread.Sleep(1000);
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Then(@"Enter the candidate experience")]
        public void ThenEnterTheCandidateExperience()
        {
            try
            {
                if (ExpectedConditions.AlertIsPresent()(webDriver) != null)
                {
                    SpecHooks.extentTest.Pass("Candidate has already been referred");
                    Thread.Sleep(2000);
                    webDriver.Close();

                    Thread.Sleep(4000);
                    webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                    webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSHeader")));
                    webDriver.FindElement(By.CssSelector("a[title=Logout]")).Click();
                }
                else
                {
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                    webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=minExperience]")).SendKeys(ExcelLibrary.ReadData(1, "ExperienceInYrs"));
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Then(@"Select the candidates expertise technology")]
        public void ThenSelectTheCandidatesExpertiseTechnology()
        {
            try
            {
                if (ExpectedConditions.AlertIsPresent()(webDriver) != null)
                {
                    SpecHooks.extentTest.Pass("Candidate has already been referred");
                    Thread.Sleep(2000);
                    webDriver.Close();

                    Thread.Sleep(4000);
                    webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                    webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSHeader")));
                    webDriver.FindElement(By.CssSelector("a[title=Logout]")).Click();
                }
                else
                {
                    SelectElement selectTechnology = new SelectElement(webDriver.FindElement(By.Name("technology")));
                    selectTechnology.SelectByText(ExcelLibrary.ReadData(1, "Technology"));
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Then(@"Enter the notice period")]
        public void ThenEnterTheNoticePeriod()
        {
            try
            {
                if (ExpectedConditions.AlertIsPresent()(webDriver) != null)
                {
                    SpecHooks.extentTest.Pass("Candidate has already been referred");
                    Thread.Sleep(2000);
                    webDriver.Close();

                    Thread.Sleep(4000);
                    webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                    webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSHeader")));
                    webDriver.FindElement(By.CssSelector("a[title=Logout]")).Click();
                }
                else
                {
                    webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=noticePeriod]")).SendKeys(ExcelLibrary.ReadData(1, "NoticePeriodInDays"));
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Then(@"Upload the resume of the candidate")]
        public void ThenUploadTheResumeOfTheCandidate()
        {
            try
            {
                if (ExpectedConditions.AlertIsPresent()(webDriver) != null)
                {
                    SpecHooks.extentTest.Pass("Candidate has already been referred");
                    Thread.Sleep(2000);
                    webDriver.Close();

                    Thread.Sleep(4000);
                    webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                    webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSHeader")));
                    webDriver.FindElement(By.CssSelector("a[title=Logout]")).Click();
                }
                else
                {
                    IWebElement fileUpload = webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[type=file]"));
                    fileUpload.SendKeys(ExcelLibrary.ReadData(1, "Resume"));
                    if (!ExcelLibrary.ReadData(1, "Resume").ToLower().Contains(".pdf") || !ExcelLibrary.ReadData(1, "Resume").ToLower().Contains(".rtl") || !ExcelLibrary.ReadData(1, "Resume").ToLower().Contains(".txt") || !ExcelLibrary.ReadData(1, "Resume").ToLower().Contains(".doc"))
                    {
                        SpecHooks.extentTest.Pass("Resume uploaded is not in .doc or .pdf or .rtl, .txt formats");
                    }
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Then(@"Enter the comments")]
        public void ThenEnterTheComments()
        {
            try
            {
                if (ExpectedConditions.AlertIsPresent()(webDriver) != null)
                {
                    SpecHooks.extentTest.Pass("Candidate has already been referred");
                    Thread.Sleep(2000);
                    webDriver.Close();

                    Thread.Sleep(4000);
                    webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                    webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSHeader")));
                    webDriver.FindElement(By.CssSelector("a[title=Logout]")).Click();
                }
                else
                {
                    webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] textarea[name=comments]")).SendKeys(ExcelLibrary.ReadData(1, "Comments"));
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                    Thread.Sleep(2000);
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Then(@"Submit the referal form")]
        public void ThenSubmitTheReferalForm()
        {
            try
            {
                if (ExpectedConditions.AlertIsPresent()(webDriver) != null)
                {
                    SpecHooks.extentTest.Pass("Candidate has already been referred");
                    Thread.Sleep(2000);
                    webDriver.Close();

                    Thread.Sleep(4000);
                    webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                    webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSHeader")));
                    webDriver.FindElement(By.CssSelector("a[title=Logout]")).Click();
                }
                else
                {
                    webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] a[title=Submit]")).Click();
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                    WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                    wait.Until(ExpectedConditions.AlertIsPresent());
                    IAlert alert = webDriver.SwitchTo().Alert();
                    alert.Accept();

                    Thread.Sleep(4000);
                    webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
                    webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSHeader")));
                    webDriver.FindElement(By.CssSelector("a[title=Logout]")).Click();
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }
        
        [Then(@"Select the candidate type as fresher")]
        public void ThenSelectTheCandidateTypeAsFresher()
        {
            try
            {
                if (ExpectedConditions.AlertIsPresent()(webDriver) != null)
                {
                    SpecHooks.extentTest.Pass("Candidate has already been referred");
                    Thread.Sleep(2000);
                    webDriver.Close();
                }
                else
                {
                    SelectElement selectTechnology = new SelectElement(webDriver.FindElement(By.Name("candType")));
                    selectTechnology.SelectByText(ExcelLibrary.ReadData(2, "CandidateType"));
                    SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Filter the job requests on selecting the respective country")]
        public void ThenFilterTheJobRequestsOnSelectingTheRespectiveCountry()
        {
            try
            {
                SelectElement selectTechnology = new SelectElement(webDriver.FindElement(By.Name("selCountry")));
                selectTechnology.SelectByText(ExcelLibrary.ReadData(1, "Country"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Filter the job requests on keyword search")]
        public void ThenFilterTheJobRequestsOnKeywordSearch()
        {
            try
            {
                webDriver.FindElement(By.CssSelector("form[name=searchJobsForm] input[name=keyword]")).SendKeys(ExcelLibrary.ReadData(1, "SearchKeyword"));
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Click on search button based on filter selection")]
        public void ThenClickOnSearchButtonBasedOnFilterSelection()
        {
            try
            {
                webDriver.FindElement(By.CssSelector("form[name=searchJobsForm] a[title=search]")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Click on clear all filter button")]
        public void ThenClickOnClearAllFilterButton()
        {
            try
            {
                Thread.Sleep(2000);
                webDriver.FindElement(By.CssSelector("form[name=searchJobsForm] a[title*=Clear]")).Click();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Check if the pagination exists")]
        public void ThenCheckIfThePaginationExists()
        {
            try
            {
                if (webDriver.FindElements(By.CssSelector("input[name=pagenum]")).Count == 0)
                {
                    SpecHooks.extentTest.Pass("No job requests found");
                }
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Check if the next button click is working")]
        public void ThenCheckIfTheNextButtonClickIsWorking()
        {
            try
            {
                if (webDriver.FindElements(By.CssSelector("input[name=pagenum]")).Count > 0)
                {
                    webDriver.FindElement(By.CssSelector("a[title*=Next]")).Click();
                }
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Check if the previous button click is working")]
        public void ThenCheckIfThePreviousButtonClickIsWorking()
        {
            try
            {
                if (webDriver.FindElements(By.CssSelector("input[name=pagenum]")).Count > 0)
                {
                    webDriver.FindElement(By.CssSelector("a[title*=Previous]")).Click();
                }
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Check if the last button click is working")]
        public void ThenCheckIfTheLastButtonClickIsWorking()
        {
            try
            {
                if (webDriver.FindElements(By.CssSelector("input[name=pagenum]")).Count > 0)
                {
                    webDriver.FindElement(By.CssSelector("a[title*=Last]")).Click();
                }
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Check if the first button click is working")]
        public void ThenCheckIfTheFirstButtonClickIsWorking()
        {
            try
            {
                if (webDriver.FindElements(By.CssSelector("input[name=pagenum]")).Count > 0)
                {
                    webDriver.FindElement(By.CssSelector("a[title*=First]")).Click();
                }
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

        [Then(@"Click on ok from the alert")]
        public void ThenClickOnOkFromTheAlert()
        {
            try
            {
                Thread.Sleep(3000);
                IAlert alert = webDriver.SwitchTo().Alert();
                alert.Accept();
                SpecHooks.extentTest.Pass(ScenarioContext.Current.StepContext.StepInfo.Text);
            }
            catch(Exception ex)
            {
                SpecHooks.extentTest.Fail(ScenarioContext.Current.StepContext.StepInfo.Text + " " + ex.Message);
            }
        }

    }
}
