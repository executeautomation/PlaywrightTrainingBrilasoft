using Microsoft.Playwright;

namespace PlaywrightTestDemo.Utilities
{
    //1. Make the class static 
    //2. Make the methods as static
    //3. Add a "this" keyword to the first parameter of the method
    public static class UIElementUtilities
    {
        public static async Task ClearAndFillAsync(this ILocator locator, string inputText)
        {
            await locator.ClearAsync();
            await locator.FillAsync(inputText);
        }

        public static async Task ClickAsync(this ILocator locator)
        {
            await locator.ClickAsync();
        }

        public static async Task SelectDropDownWithIndexAsync(this ILocator locator, int index)
        {
            await locator.SelectOptionAsync(new SelectOptionValue { Index = index });
        }

        public static async Task SelectDropDownWithValueAsync(this ILocator locator, string value)
        {
            await locator.SelectOptionAsync(value);
        }

    }
}
