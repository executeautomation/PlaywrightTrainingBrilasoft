using AutoFixture.Xunit2;
using Microsoft.Playwright;
using PlaywrightTestDemo.Pages;
using PlaywrightTestDemo.Utilities;


namespace PlaywrightTestDemo
{
    public class TestWithPOM :
        IClassFixture<PlaywrightDriver>,
        IClassFixture<ExtentReportHelper>
    {

        private readonly PlaywrightDriver _driver;
        private readonly ExtentReportHelper _extentReportHelper;
        private IPage page;
        public TestWithPOM(PlaywrightDriver driver, ExtentReportHelper extentReportHelper)
        {
            _driver = driver;
            _extentReportHelper = extentReportHelper;
            page = _driver.LaunchBrowserAsync().Result;
        }


        [Theory]
        [InlineData("admin", "password", "")]
        [InlineData("admin1", "password", "Invalid login attempt.")]
        [InlineData("admin", "password2", "Invalid login attempt.")]
        [InlineData("admin", "", "The Password field is required.")]
        [InlineData("", "password", "The UserName field is required.")]
        public async void LoginTest(string userName, string password, string message)
        {
            LoginPage loginPage = new LoginPage(page);
            await loginPage.PerformLoginAsync();

            if (message != string.Empty)
                Assert.True(await page.IsVisibleAsync($"text={message}"));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public async Task WorkingWithLocator(UserData userData)
        {

            var testReport = _extentReportHelper.ExtentReports
                                .CreateTest($"WorkingWithLocators:{userData.Name}");
            //HomePage
            HomePage homePage = new HomePage(page);
            var loginPage = await homePage.ClickLoginLinkAsync();
            testReport
                .Log(AventStack.ExtentReports.Status.Pass, "Click Login Link");

            //2. Perform Login
            await loginPage.PerformLoginAsync();
            testReport.Log(AventStack.ExtentReports.Status.Pass, "Perform Login Operation");

            //3. Employee List
            var employeeListPage = await homePage.ClickEmployeeListLinkAsync();
            testReport.Log(AventStack.ExtentReports.Status.Pass, "Employee List Clicked");

            //4. Click Create new button
            var createUser = await employeeListPage.ClickCreateNewAsync();
            testReport.Log(AventStack.ExtentReports.Status.Pass, "Create New Button Clicked");

            var isUserCreated = await createUser.CreateUserAysnc(userData);

            if (isUserCreated)
                testReport.Log(AventStack.ExtentReports.Status.Pass, "User Created");
            else
                testReport.Log(AventStack.ExtentReports.Status.Fail, "User is not Created");

        }

        [Theory, AutoData]
        public async Task WorkingWithLocatorWithAutoData(UserData userData)
        {
            LoginPage loginPage = new LoginPage(page);
            await loginPage.PerformLoginAsync();

            //Employee List
            await page.GetByRole(AriaRole.Link, new() { Name = "Employee List" }).ClickAsync();

            CreateUser createUser = new CreateUser(page);
            await createUser.CreateUserAysnc(userData);

        }



        //Data Driven Testing datasource
        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { new UserData("First Data", "2000", 8, "3", "firstdata@gmail.com") };
            yield return new object[] { new UserData("Second Data", "3000", 9, "2", "seconddata@gmail.com") };
            yield return new object[] { new UserData("Third Data", "4000", 10, "1", "thirddata@gmail.com") };
            yield return new object[] { new UserData("Fourth Data", "5000", 11, "4", "forthdata@gmail.com") };
        }
    }


    public record UserData(string Name, string Salary, int DurationWorked, string Grade, string Email);
}