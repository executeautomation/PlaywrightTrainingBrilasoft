using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTestDemo.Pages
{
    public class HomePage
    {
        private readonly IPage _page;

        public HomePage(IPage page) => _page = page;

        private ILocator _lnkLogin => _page.GetByRole(AriaRole.Link, new() { Name = "Login" });

        private ILocator _lnkEmployeeList => _page.GetByRole(AriaRole.Link, new() { Name = "Employee List" });

        public async Task<LoginPage> ClickLoginLinkAsync()
        {
            await _lnkLogin.ClickAsync();
            return new LoginPage(_page);
        }

        public async Task<EmployeeList> ClickEmployeeListLinkAsync()
        {
            await _lnkEmployeeList.ClickAsync();
            return new EmployeeList(_page);
        }
    }
}
