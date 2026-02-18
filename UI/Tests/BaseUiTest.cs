using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.Config;
using NUnit.Framework;
using System.Security.Permissions;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Runtime.InteropServices;



namespace AutomationExerciseDemo.UI.Tests
{
    public abstract class BaseUiTest
    {
        protected IPlaywright? Playwright;
        protected IBrowser? Browser;
        protected IPage? Page;
        protected EnvironmentConfig? Config;

        //Extent Report fields:
        protected static ExtentReports? Extent;
        
        protected ExtentTest? Test;

  


  
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            var reportPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, $"TestReport_{DateTime.Now:yyyyMMdd_HHmmss}.html" );

            var spark = new ExtentSparkReporter(reportPath);

            Extent = new ExtentReports();
            Extent.AttachReporter(spark);


            Extent.AddSystemInfo("Environment", "QA");
            Extent.AddSystemInfo("Browser", "Chromium");
            Extent.AddSystemInfo("OS", Environment.OSVersion.ToString());

        }

        // setup
        [SetUp] 
        public async Task SetupAsync()
        {
           
           // create Test Node:
           Test = Extent?.CreateTest(TestContext.CurrentContext.Test.Name);
           
            Config = ConfigLoader.Load();

            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Browser = await Playwright.Chromium.LaunchAsync(new() {Headless = false});
           
           //ad blocking context
           var context = await Browser.NewContextAsync(new()
           {
               BypassCSP = true,
               IgnoreHTTPSErrors = true,
               ServiceWorkers = ServiceWorkerPolicy.Block 
           });    

            Page = await context.NewPageAsync();
            
        }





        
        [TearDown]
        public async Task TeardownAsync()
        {
            //generate test report:

                //get test outcome and  results message:
                var testOutcome = TestContext.CurrentContext.Result.Outcome.Status;
                var resultMessage = TestContext.CurrentContext.Result.Message;

                // if test failed, get screenshot
                if(testOutcome == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    var screenshotPath = await TakeScreenshotAsync(TestContext.CurrentContext.Test.Name);
                    Test?.Fail(resultMessage);
                    Test?.AddScreenCaptureFromPath(screenshotPath);
                }
                else
                {
                    Test?.Pass("Test Passed");
                }


            // close Browser and dispose playwright
            try
            {
                if(Browser != null)
                {
                    await Browser!.CloseAsync();
                }
            }
            catch(Exception ex)
            {
                Test?.Warning($"Browser close failed: {ex.Message}");
            }
            Playwright!.Dispose();
        }


        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            // flush the Extent Reporter
            Extent?.Flush();
        }

        /**************************************************************************************************
        Helper Methods:
        ***************************************************************************************************/

        //take screenshot 
        protected async Task<string> TakeScreenshotAsync(string testName)
        {
            var screenshotsDirectory = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Screenshots");

            // if directory does not exist, create it
            if (!Directory.Exists(screenshotsDirectory))
            {
                Directory.CreateDirectory(screenshotsDirectory);                
            }

            //create file pathL
            var filePath = Path.Combine(screenshotsDirectory, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

            await Page!.ScreenshotAsync(new()
            {
                Path = filePath,
                FullPage = true
            });

            return filePath;
        }

    }
}