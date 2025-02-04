﻿using Microsoft.Playwright;
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

        private ILocator _lnkLogOff => _page.GetByRole(AriaRole.Link, new() { Name = "Log off" });

        private ILocator _lnkProduct => _page.GetByRole(AriaRole.Link, new() { Name = "Product" }).Nth(0);



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

        public async Task<HomePage> ClickLogOff()
        {
            await _lnkLogOff.ClickAsync();
            return new HomePage(_page);
        }

        public async Task<bool> IsLogoffLinkExist()
        {
            return await _lnkLogOff.IsVisibleAsync();
        }

        public async Task<CreateProduct> ClickProduct()
        {
            await _lnkProduct.ClickAsync();
            return new CreateProduct(_page);
        }
    }
}
