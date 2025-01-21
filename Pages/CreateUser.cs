using Microsoft.Playwright;
using PlaywrightTestDemo.Utilities;

namespace PlaywrightTestDemo.Pages
{
    public class CreateUser
    {
        private readonly IPage _page;

        //Constructor
        public CreateUser(IPage page) => _page = page;

        //Locators to be declared in the page level
        private ILocator _name => _page.GetByLabel("Name");
        private ILocator _salary => _page.GetByLabel("Salary");
        private ILocator _duration => _page.GetByRole(AriaRole.Spinbutton, new() { Name = "DurationWorked" });
        private ILocator _grade => _page.GetByRole(AriaRole.Combobox, new() { Name = "Grade" });
        private ILocator _mail => _page.GetByLabel("Email");
        private ILocator _create => _page.GetByRole(AriaRole.Button, new() { Name = "Create" });


        //Action methods
        public async Task CreateUserAysnc(UserData userData)
        {

            await _name.ClearAndFillAsync(userData.Name);

            await _salary.ClearAndFillAsync(userData.Salary);

            await _duration.ClearAndFillAsync(userData.DurationWorked.ToString());

            await _grade.SelectOptionAsync("Middle");

            await _mail.ClearAndFillAsync(userData.Email);

            await _create.ClickAsync();
        }
    }
}
