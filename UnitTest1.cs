using AutoFixture.Xunit2;
using Microsoft.Playwright;
using PlaywrightTestDemo.Utilities;
using Xunit.Abstractions;


namespace PlaywrightTestDemo
{
    public class UnitTest1
    {

        private ITestOutputHelper _testOutputHelper;
        private UIElementUtilities _uiElementUtilities;
        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _uiElementUtilities = new UIElementUtilities(testOutputHelper);
        }


        [Theory]
        [InlineData("admin", "password", "")]
        [InlineData("admin1", "password", "Invalid login attempt.")]
        [InlineData("admin", "password2", "Invalid login attempt.")]
        [InlineData("admin", "", "The Password field is required.")]
        [InlineData("", "password", "The UserName field is required.")]
        public async void LoginTest(string userName, string password, string message)
        {

            var page = await LaunchBrowserAsync();

            //Explicit wait
            await page.WaitForSelectorAsync("text=Login");

            // Step 3: Click login link
            await _uiElementUtilities.ClickAsync(page.Locator("text=Login"));

            await page.WaitForURLAsync("**/Login");

            // Step 4: Enter username and password (data)
            await page.FillAsync("input[name=UserName]", userName);
            await page.FillAsync("input[name=Password]", password);

            // Step 5: Click login button
            await page.ClickAsync("input[type=submit]");

            if (message != string.Empty)
                Assert.True(await page.IsVisibleAsync($"text={message}"));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public async Task WorkingWithLocator(UserData userData)
        {
            //1. Launch the browser
            var page = await LaunchBrowserAsync();

            //2. Perform Login
            await PerformLogin(page);

            //3. Create User
            await CreateUser(page, userData);
        }

        private async Task CreateUser(IPage page, UserData userData)
        {

            _testOutputHelper.WriteLine($"Creating user with Name: {userData.Name}");
            _testOutputHelper.WriteLine($"Creating user with Salary: {userData.Salary}");
            _testOutputHelper.WriteLine($"Creating user with DurationWorked: {userData.DurationWorked}");
            _testOutputHelper.WriteLine($"Creating user with Grade: {userData.Grade}");
            _testOutputHelper.WriteLine($"Creating user with Email: {userData.Email}");

            //Employee List
            await page.GetByRole(AriaRole.Link, new() { Name = "Employee List" }).ClickAsync();

            //Create New
            await page.GetByRole(AriaRole.Link, new() { Name = "Create New" }).ClickAsync();

            await _uiElementUtilities.ClearAndFillAsync(page.GetByLabel("Name"), userData.Name);

            await page.GetByLabel("Salary").Nth(0).FillAsync(userData.Salary);

            await page.GetByRole(AriaRole.Spinbutton, new() { Name = "DurationWorked" }).FillAsync(userData.DurationWorked.ToString());

            await page.GetByRole(AriaRole.Combobox, new() { Name = "Grade" }).SelectOptionAsync(new SelectOptionValue { Index = 3 });

            await page.GetByLabel("Email").FillAsync(userData.Email);

            await page.GetByRole(AriaRole.Button, new() { Name = "Create" }).ClickAsync();
        }

        private async Task PerformLogin(IPage page)
        {
            //Explicit wait
            await page.WaitForSelectorAsync("text=Login");

            // Step 3: Click login link
            await page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();

            await _uiElementUtilities.ClearAndFillAsync(page.GetByRole(AriaRole.Textbox, new() { Name = "UserName" }), "admin");

            await page.FillAsync("//input[@id='Password']", "password");

            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        }


        [Theory, AutoData]
        public async Task WorkingWithLocatorWithAutoData(UserData userData)
        {
            var page = await LaunchBrowserAsync();

            //Explicit wait
            await page.WaitForSelectorAsync("text=Login");

            // Step 3: Click login link
            await page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();

            await page.GetByRole(AriaRole.Textbox, new() { Name = "UserName" }).FillAsync("admin");

            await page.FillAsync("//input[@id='Password']", "password");

            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();

            //Employee List
            await page.GetByRole(AriaRole.Link, new() { Name = "Employee List" }).ClickAsync();

            //Create New
            await page.GetByRole(AriaRole.Link, new() { Name = "Create New" }).ClickAsync();

            await page.GetByLabel("Name").FillAsync(userData.Name);

            await page.GetByLabel("Salary").Nth(0).FillAsync(userData.Salary);

            await page.GetByRole(AriaRole.Spinbutton, new() { Name = "DurationWorked" }).FillAsync(userData.DurationWorked.ToString());

            await page.GetByRole(AriaRole.Combobox, new() { Name = "Grade" }).SelectOptionAsync(new SelectOptionValue { Index = 3 });

            await page.GetByLabel("Email").FillAsync(userData.Email);

            await page.GetByRole(AriaRole.Button, new() { Name = "Create" }).ClickAsync();

        }


        private async Task<IPage> LaunchBrowserAsync()
        {
            // Step 1: Launch the browser
            var playwright = await Playwright.CreateAsync();

            var browserLaunchOption = new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 50
            };

            var browser = await playwright.Chromium.LaunchAsync(browserLaunchOption);
            var page = await browser.NewPageAsync();

            // Step 2: Navigate the URL
            await page.GotoAsync("http://eaapp.somee.com/");

            return page;
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