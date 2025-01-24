using Microsoft.Playwright;

namespace PlaywrightTestDemo.Utilities
{
    public class PlaywrightAPIDriver
    {
        private readonly Task<IAPIRequestContext> _requestContext;

        public PlaywrightAPIDriver()
        {
            _requestContext = Task.Run(CreateApiContext);
        }

        public IAPIRequestContext APIRequestContext => _requestContext.Result;


        public async Task<IAPIRequestContext> CreateApiContext()
        {
            //1. First Create an instance of Playwright
            var playwright = await Playwright.CreateAsync();

            //2. Create instance of API Request
            var apiRequestContext = new APIRequestNewContextOptions
            {
                BaseURL = "http://localhost:8001/"
            };

            return await playwright.APIRequest.NewContextAsync(apiRequestContext);

        }
    }
}
