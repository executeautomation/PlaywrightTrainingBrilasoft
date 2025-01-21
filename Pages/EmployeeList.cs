using Microsoft.Playwright;

namespace PlaywrightTestDemo.Pages
{
    public class EmployeeList
    {

        private readonly IPage _page;

        public EmployeeList(IPage page) => _page = page;

        private ILocator _btnCreate => _page.GetByRole(AriaRole.Button, new() { Name = "Create New" });

        public async Task<CreateUser> ClickCreateNewAsync()
        {
            await _btnCreate.ClickAsync();
            return new CreateUser(_page);
        }
    }
}
