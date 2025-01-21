using AutoFixture.Xunit2;
using Microsoft.Playwright;
using PlaywrightTestDemo.Pages;
using PlaywrightTestDemo.Utilities;
using Xunit.Abstractions;


namespace PlaywrightTestDemo
{
    public class TestWithPOM
    {

        private ITestOutputHelper _testOutputHelper;
        private PlaywrightDriver _driver;
        public TestWithPOM(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _driver = new PlaywrightDriver();
        }


        [Theory]
        [InlineData("admin", "password", "")]
        [InlineData("admin1", "password", "Invalid login attempt.")]
        [InlineData("admin", "password2", "Invalid login attempt.")]
        [InlineData("admin", "", "The Password field is required.")]
        [InlineData("", "password", "The UserName field is required.")]
        public async void LoginTest(string userName, string password, string message)
        {

            var page = await _driver.LaunchBrowserAsync();

            LoginPage loginPage = new LoginPage(page);
            await loginPage.PerformLoginAsync();

            if (message != string.Empty)
                Assert.True(await page.IsVisibleAsync($"text={message}"));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public async Task WorkingWithLocator(UserData userData)
        {
            //1. Launch the browser
            var page = await _driver.LaunchBrowserAsync();

            //HomePage
            HomePage homePage = new HomePage(page);
            var loginPage = await homePage.ClickLoginLinkAsync();

            //2. Perform Login
            await loginPage.PerformLoginAsync();

            //3. Employee List
            var employeeListPage = await homePage.ClickEmployeeListLinkAsync();
            
            //4. Click Create new button
            var createUser = await employeeListPage.ClickCreateNewAsync();

            await createUser.CreateUserAysnc(userData);
        }

        [Theory, AutoData]
        public async Task WorkingWithLocatorWithAutoData(UserData userData)
        {
            var page = await _driver.LaunchBrowserAsync();

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