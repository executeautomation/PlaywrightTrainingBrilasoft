using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using PlaywrightTestDemo.Utilities;
using Reqnroll;

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
        public void AfterStep()
        {
            switch(_scenarioContext.StepContext.StepInfo.StepDefinitionType)
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
            
        }


        [AfterTestRun]
        public static void AfterTest()
        {
            _extentReports.Flush();
        }
    }
}
