using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Model;
using PlaywrightTestDemo.Utilities;
using Reqnroll;
using System.Text.RegularExpressions;

namespace PlaywrightTestDemo.ReusableSteps
{

    [Binding]
    public class Hooks
    {
        private readonly ExtentReportHelper _extentReportHelper;
        private static ExtentReports _extentReports;
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;
        private ExtentTest _scenario;

        public Hooks(
            ExtentReportHelper extentReportHelper, 
            FeatureContext featureContext, 
            ScenarioContext scenarioContext)
        {
            _extentReportHelper = new ExtentReportHelper();
            _extentReports = _extentReportHelper.ExtentReports;
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var feature = _extentReports.CreateTest<Feature>(_featureContext.FeatureInfo.Title);
            _scenario = feature.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);

        }

        [AfterStep]
        public async Task AfterStep()
        {
            var fileName = $"{_featureContext.FeatureInfo.Title.Trim()}_{Regex.Replace(_scenarioContext.ScenarioInfo.Title, @"\s", "")}";

            if (_scenarioContext.TestError == null)
            {
                switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType)
                {
                    case Reqnroll.Bindings.StepDefinitionType.Given:
                        _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case Reqnroll.Bindings.StepDefinitionType.When:
                        _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case Reqnroll.Bindings.StepDefinitionType.Then:
                        _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            } else
            {
                switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType)
                {
                    case Reqnroll.Bindings.StepDefinitionType.Given:
                        _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text)
                            .Fail(_scenarioContext.TestError.Message, new ScreenCapture
                            {
                                Title = "Error Screenshot",
                                Path = await ((PlaywrightDriver)_scenarioContext["PlaywrightDriver"])
                                .TakeScreenshotAsPathAsync(fileName)
                            });
                        break;
                    case Reqnroll.Bindings.StepDefinitionType.When:
                        _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text)
                         .Fail(_scenarioContext.TestError.Message, new ScreenCapture
                         {
                             Title = "Error Screenshot",
                             Path = await ((PlaywrightDriver)_scenarioContext["PlaywrightDriver"])
                                .TakeScreenshotAsPathAsync(fileName)
                         });
                        break;
                    case Reqnroll.Bindings.StepDefinitionType.Then:
                        _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text)
                         .Fail(_scenarioContext.TestError.Message, new ScreenCapture
                         {
                             Title = "Error Screenshot",
                             Path = await ((PlaywrightDriver)_scenarioContext["PlaywrightDriver"])
                                .TakeScreenshotAsPathAsync(fileName)
                         });
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
        }


        [AfterTestRun]
        public static void AfterTest()
        {
            _extentReports.Flush();
        }
    }
}
