using Microsoft.Playwright;

namespace PlaywrightTestDemo.Utilities
{
    public class PlaywrightAPIDriver : IDisposable
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
            var bearerToken = "324234234lkjdfgsdgsdsdfsdfsd";

            //2. Create instance of API Request
            var apiRequestContext = new APIRequestNewContextOptions
            {
                BaseURL = "http://localhost:8001/",
                ExtraHTTPHeaders = new Dictionary<string, string>
                {
                    { "Authentication", $"Bearer{bearerToken}" }
                }
            };

            return await playwright.APIRequest.NewContextAsync(apiRequestContext);

        }

        public void Dispose()
        {
            if(_requestContext.IsCompleted is true)
                _requestContext.Dispose();
        }
    }
}
