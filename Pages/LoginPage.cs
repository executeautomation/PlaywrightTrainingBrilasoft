using Microsoft.Playwright;
using PlaywrightTestDemo.Utilities;


namespace PlaywrightTestDemo.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page) => _page = page;

        //Locators to be declared in the page level
        private ILocator _userName => _page.GetByRole(AriaRole.Textbox, new() { Name = "UserName" });

        private ILocator _password => _page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });

        private ILocator _btnLogin => _page.GetByRole(AriaRole.Button, new() { Name = "Log in" });

        public async Task<HomePage> PerformLoginAsync()
        {
            //Read the JSON file
            var data = DDTJsonHelper.ReadJsonFile();

            await _userName.FillAsync(data.UserName);

            await _password.FillAsync(data.Password);

            await _btnLogin.ClickAsync();

            return new HomePage(_page);
        }

        public async Task<HomePage> PerformLoginAsync(string userName, string password)
        {
            await _userName.FillAsync(userName);

            await _password.FillAsync(password);

            await _btnLogin.ClickAsync();

            return new HomePage(_page);
        }

    }
}
