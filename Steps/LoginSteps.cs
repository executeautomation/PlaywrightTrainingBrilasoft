using FluentAssertions;
using Microsoft.Playwright;
using PlaywrightTestDemo.Pages;
using PlaywrightTestDemo.Utilities;
using Reqnroll;

namespace PlaywrightTestDemo.Steps
{

    [Binding]
    public class LoginSteps
    {
        private readonly PlaywrightDriver _playwrightDriver;
        private IPage _page;
        private HomePage _homePage;
        private LoginPage _loginPage;

        public LoginSteps(PlaywrightDriver playwrightDriver)
        {
            _playwrightDriver = playwrightDriver;
        }

        [Given("I navigate to the site")]
        public async Task GivenINavigateToTheSite()
        {
            _page = await _playwrightDriver.LaunchBrowserAsync();
        }

        [Given("I click login link")]
        public async Task GivenIClickLoginLink()
        {
            _homePage = new HomePage(_page);
            _loginPage= await _homePage.ClickLoginLinkAsync();
        }

        [When("I enter login details like username as {string} and password as {string}")]
        public async Task WhenIEnterLoginDetailsLikeUsernameAsAndPasswordAs(string userName, string password)
        {
            _homePage = await _loginPage.PerformLoginAsync(userName, password);
        }

        [Then("I should see the log off link in the page")]
        public async Task ThenIShouldSeeTheLogOffLinkInThePage()
        {
            await _homePage.ClickLogOff();
        }

        [Then("I should not see the log off link in the page")]
        public async Task ThenIShouldNotSeeTheLogOffLinkInThePage()
        {
            (await _homePage.IsLogoffLinkExist()).Should().NotBe(true);
        }


    }
}
