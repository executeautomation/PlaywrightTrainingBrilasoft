using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTestDemo.Utilities
{
    public class PlaywrightDriver
    {
        public async Task<IPage> LaunchBrowserAsync()
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



    }
}
