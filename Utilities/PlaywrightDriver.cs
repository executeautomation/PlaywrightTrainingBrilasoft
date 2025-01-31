using Microsoft.Playwright;
using System.Reflection;

namespace PlaywrightTestDemo.Utilities
{
    public class PlaywrightDriver : IDisposable
    {

        private IPlaywright _playwright;

        private IPage _page;

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
            _page = await browser.NewPageAsync();

            // Step 2: Navigate the URL
            await _page.GotoAsync("http://eaapp.somee.com");

            return _page;
        }


        public async Task<string> TakeScreenshotAsPathAsync(string fileName)
        {
            var path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}//{fileName}.png";

            await _page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = path,
            });

            return path;
        }


        //Automatically called 
        public void Dispose()
        {
            _playwright.Dispose();
        }
    }
}
