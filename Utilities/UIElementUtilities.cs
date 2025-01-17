using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace PlaywrightTestDemo.Utilities
{
    public class UIElementUtilities
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UIElementUtilities(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        public async Task ClearAndFillAsync(ILocator locator, string inputText)
        {
            _testOutputHelper.WriteLine($"Clearing the text");
            await locator.ClearAsync();
            await locator.FillAsync(inputText);
            _testOutputHelper.WriteLine($"Filled the text: {inputText}");
        }

        public async Task ClickAsync(ILocator locator)
        {
            _testOutputHelper.WriteLine($"Clicking on the element");
            await locator.ClickAsync();
            _testOutputHelper.WriteLine($"Clicked on the element");
        }

        public async Task SelectDropDownWithIndexAsync(ILocator locator, int index)
        {
            _testOutputHelper.WriteLine($"Selecting the value via Index: {index}");
            await locator.SelectOptionAsync(new SelectOptionValue { Index = index });
        }

        public async Task SelectDropDownWithValueAsync(ILocator locator, string value)
        {
            _testOutputHelper.WriteLine($"Selecting the value via Value: {value}");
            await locator.SelectOptionAsync(value);
        }

    }
}
