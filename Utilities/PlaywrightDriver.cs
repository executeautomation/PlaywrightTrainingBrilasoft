using Microsoft.Playwright;

namespace PlaywrightTestDemo.Utilities
{
    public class PlaywrightDriver : IDisposable
    {

        private IPlaywright _playwright;

        public async Task<IPage> LaunchBrowserAsync()
        {
            // Step 1: Launch the browser
            _playwright = await Playwright.CreateAsync();

            var browserLaunchOption = new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 50
            };

            var browser = await _playwright.Chromium.LaunchAsync(browserLaunchOption);
            var page = await browser.NewPageAsync();

            // Step 2: Navigate the URL
            await page.GotoAsync("http://eaapp.somee.com");

            return page;
        }


        //Automatically called 
        public void Dispose()
        {
            _playwright.Dispose();
        }
    }
}
