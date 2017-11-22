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
            webDriver = new ChromeDriver();
            webDriver.Manage().Window.Maximize();
        }
        
        [Given(@"Open SVCareers websites")]
        public void GivenOpenSVCareersWebsites()
        {
            webDriver.Navigate().GoToUrl(ConfigurationManager.AppSettings["SVCareersURL"].ToString());
        }
        
        [Given(@"Type your username and password in the login form")]
        public void GivenTypeYourUsernameAndPasswordInTheLoginForm()
        {
            Thread.Sleep(2000);
            webDriver.SwitchTo().Frame(webDriver.FindElement(By.Name("JRAMPSMainFrame")));
            IWebElement elUserName = webDriver.FindElement(By.Name("loginId"));
            elUserName.SendKeys(ExcelLibrary.ReadData(1, "UserName"));

            IWebElement elPassword = webDriver.FindElement(By.Name("loginScreenPassword"));
            elPassword.SendKeys(ExcelLibrary.ReadData(1, "Password"));
        }

        [Then(@"Click on Login form submit button")]
        public void ThenClickOnLoginFormSubmitButton()
        {
            IWebElement elLoginBtn = webDriver.FindElement(By.Id("svCareersLoginId"));
            elLoginBtn.Click();
        }


        [Then(@"Select the job you want to refer from the grid")]
        public void ThenSelectTheJobYouWantToReferFromTheGrid()
        {
            if (webDriver.FindElements(By.CssSelector("#ShowDesc")).Count > 0)
            {
                IWebElement elJobRequest = webDriver.FindElement(By.CssSelector("#ShowDesc table tbody tr:nth-child(3) input[type=checkbox]"));
                elJobRequest.Click();
                Thread.Sleep(2000);
            }
            else
            {
                SpecHooks.extentTest.Warning("No job requests found");
            }
        }

        [Then(@"Click on refer a friend link")]
        public void ThenClickOnReferAFriendLink()
        {
            IWebElement elReferFrd = webDriver.FindElement(By.CssSelector("#rowForReferAFriendToJob a"));
            elReferFrd.Click();
        }

        [Given(@"Switch the focus to the candidate details form popup")]
        public void GivenSwitchTheFocusToTheCandidateDetailsFormPopup()
        {
            Thread.Sleep(2000);
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            Thread.Sleep(2000);
        }

        [When(@"Switch the focus to the candidate details form popup")]
        public void WhenSwitchTheFocusToTheCandidateDetailsFormPopup()
        {
            Thread.Sleep(2000);
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            Thread.Sleep(2000);
        }


        [Then(@"Enter candidate firstname")]
        public void ThenEnterCandidateFirstname()
        {
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=candFirstName]")).SendKeys(ExcelLibrary.ReadData(1, "FirstName"));
        }
        
        [Then(@"Enter candidate middlename")]
        public void ThenEnterCandidateMiddlename()
        {
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=candMiddleName]")).SendKeys(ExcelLibrary.ReadData(1, "MiddleName"));
        }

        [Then(@"Enter candidate lastname")]
        public void ThenEnterCandidateLastname()
        {
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=candLastName]")).SendKeys(ExcelLibrary.ReadData(1, "LastName"));
        }

        [Then(@"Select the gender")]
        public void ThenSelectTheGender()
        {
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input#"+ ExcelLibrary.ReadData(1, "Gender"))).Click();
        }
        
        [Then(@"Enter the candidate email address")]
        public void ThenEnterTheCandidateEmailAddress()
        {
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=candEmailId]")).SendKeys(ExcelLibrary.ReadData(1, "Email"));
        }
        
        [Then(@"Enter the candidate phone number")]
        public void ThenEnterTheCandidatePhoneNumber()
        {
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=candHomePhone]")).SendKeys(ExcelLibrary.ReadData(1, "Phone"));
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(0));
                //wait.Until(ExpectedConditions.AlertIsPresent());
                Thread.Sleep(5000);
                IAlert alert = webDriver.SwitchTo().Alert();
                string alertText = alert.Text;
                alert.Accept();
            }
            catch(Exception ex)
            {
                return;
            }
        }
        
        [Then(@"Select the candidate type as experienced")]
        public void ThenSelectTheCandidateTypeAsExperienced()
        {
            SelectElement selectCandidateType = new SelectElement(webDriver.FindElement(By.Name("candType")));
            selectCandidateType.SelectByText(ExcelLibrary.ReadData(1, "CandidateType"));
            Thread.Sleep(1000);
        }
        
        [Then(@"Enter the candidate experience")]
        public void ThenEnterTheCandidateExperience()
        {
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=minExperience]")).SendKeys(ExcelLibrary.ReadData(1, "ExperienceInYrs"));
        }
        
        [Then(@"Select the candidates expertise technology")]
        public void ThenSelectTheCandidatesExpertiseTechnology()
        {
            SelectElement selectTechnology = new SelectElement(webDriver.FindElement(By.Name("technology")));
            selectTechnology.SelectByText(ExcelLibrary.ReadData(1, "Technology"));
        }
        
        [Then(@"Enter the notice period")]
        public void ThenEnterTheNoticePeriod()
        {
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[name=noticePeriod]")).SendKeys(ExcelLibrary.ReadData(1, "NoticePeriodInDays"));
        }
        
        [Then(@"Upload the resume of the candidate")]
        public void ThenUploadTheResumeOfTheCandidate()
        {
            IWebElement fileUpload = webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] input[type=file]"));
            fileUpload.SendKeys(ExcelLibrary.ReadData(1, "Resume"));
        }
        
        [Then(@"Enter the comments")]
        public void ThenEnterTheComments()
        {
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] textarea[name=comments]")).SendKeys(ExcelLibrary.ReadData(1, "Comments"));
            Thread.Sleep(2000);
        }
        
        [Then(@"Submit the referal form")]
        public void ThenSubmitTheReferalForm()
        {
            webDriver.FindElement(By.CssSelector("form[name=referAFriendForm] a[title=Submit]")).Click();
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = webDriver.SwitchTo().Alert();
            alert.Accept();
        }
        
        [Then(@"Select the candidate type as fresher")]
        public void ThenSelectTheCandidateTypeAsFresher()
        {
            SelectElement selectTechnology = new SelectElement(webDriver.FindElement(By.Name("candType")));
            selectTechnology.SelectByText(ExcelLibrary.ReadData(2, "CandidateType"));
        }

        [Then(@"Filter the job requests on selecting the respective country")]
        public void ThenFilterTheJobRequestsOnSelectingTheRespectiveCountry()
        {
            SelectElement selectTechnology = new SelectElement(webDriver.FindElement(By.Name("selCountry")));
            selectTechnology.SelectByText(ExcelLibrary.ReadData(1, "Country"));
        }

        [Then(@"Filter the job requests on keyword search")]
        public void ThenFilterTheJobRequestsOnKeywordSearch()
        {
            webDriver.FindElement(By.CssSelector("form[name=searchJobsForm] input[name=keyword]")).SendKeys(ExcelLibrary.ReadData(1, "SearchKeyword"));
        }

        [Then(@"Click on search button based on filter selection")]
        public void ThenClickOnSearchButtonBasedOnFilterSelection()
        {
            webDriver.FindElement(By.CssSelector("form[name=searchJobsForm] a[title=search]")).Click();
        }

        [Then(@"Click on clear all filter button")]
        public void ThenClickOnClearAllFilterButton()
        {
            Thread.Sleep(2000);
            webDriver.FindElement(By.CssSelector("form[name=searchJobsForm] a[title*=Clear]")).Click();
        }

        [Then(@"Check if the pagination exists")]
        public void ThenCheckIfThePaginationExists()
        {
            if (webDriver.FindElements(By.CssSelector("input[name=pagenum]")).Count == 0)
            {
                SpecHooks.extentTest.Warning("No job requests found");
            }
        }

        [Then(@"Check if the next button click is working")]
        public void ThenCheckIfTheNextButtonClickIsWorking()
        {
            webDriver.FindElement(By.CssSelector("a[title*=Next]")).Click();
        }

        [Then(@"Check if the previous button click is working")]
        public void ThenCheckIfThePreviousButtonClickIsWorking()
        {
            webDriver.FindElement(By.CssSelector("a[title*=Previous]")).Click();
        }

        [Then(@"Check if the last button click is working")]
        public void ThenCheckIfTheLastButtonClickIsWorking()
        {
            webDriver.FindElement(By.CssSelector("a[title*=Last]")).Click();
        }

        [Then(@"Check if the first button click is working")]
        public void ThenCheckIfTheFirstButtonClickIsWorking()
        {
            webDriver.FindElement(By.CssSelector("a[title*=First]")).Click();
        }
    }
}
