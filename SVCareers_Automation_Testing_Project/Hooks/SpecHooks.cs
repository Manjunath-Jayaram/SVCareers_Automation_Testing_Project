using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SVCareers_Automation_Testing_Project.Hooks
{
    [Binding]
    public class SpecHooks
    {

        public static ExtentReports extent;
        public static ExtentHtmlReporter htmlReporter;
        public static ExtentTest extentTest;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            //ObjectRepository.ProjectName = "COMET";
            //string curPath = GenericHelper.CurrentDirectory();
            DirectoryInfo currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss");
            string ResultPath = currentDir.FullName+@"\ResultFolder";

            //ObjectRepository.ParentResultPath = ResultPath;
            //GenericHelper.CreatFolder(ResultPath);

            // Creating the HTML extent report
            htmlReporter = new ExtentHtmlReporter(ResultPath + "\\Report.html");
            htmlReporter.Configuration().Theme = Theme.Standard;
            htmlReporter.Configuration().DocumentTitle = "HTML Report";

            htmlReporter.Configuration().ReportName = "Execution Report";
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            // Creating the Log file
            // SeleniumWebDriver.Reports.LogReport.CreateLogFile();
        }

        [BeforeScenario]
        public void InitScenario()
        {
            extentTest = extent.CreateTest(ScenarioContext.Current.ScenarioInfo.Title);

            //webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [AfterScenario]
        public void TearDownScenario()
        {
            extent.Flush();
        }
    }
}
