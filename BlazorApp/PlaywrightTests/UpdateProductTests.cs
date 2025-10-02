using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorApp.PlaywrightTests
{
    [TestClass]
    public class UpdateProductTests : PageTest
    {
        private const string BaseUrl = "http://localhost:5020";
        private const int TestProductId = 1;

        [TestInitialize]
        public async Task TestInitialize()
        {
            await Page.GotoAsync($"{BaseUrl}/update-product/{TestProductId}");

            await Page.WaitForSelectorAsync("input[placeholder='Name']");
        }

        [TestMethod]
        public async Task UpdateProduct_SuccessfulSubmission_ShowsSuccessToast()
        {
            await Page.FillAsync("input[placeholder='Name']", "Updated Product Name");

            await Page.ClickAsync("button:has-text('Update')");

            var toast = await Page.WaitForSelectorAsync("div.toast-success", new PageWaitForSelectorOptions { Timeout = 5000 });
            Assert.IsNotNull(toast, "Success toast did not appear");

            var toastText = await toast.InnerTextAsync();
            Assert.IsTrue(toastText.Contains("Product updated with success"), "Success toast message not correct");
        }

        [TestMethod]
        public async Task ClearForm_ResetsAllFields()
        {
            await Page.FillAsync("input[placeholder='Name']", "Temporary Name");

            await Page.ClickAsync("button:has-text('Clear Form')");

            string nameValue = await Page.InputValueAsync("input[placeholder='Name']");
            string realStockValue = await Page.InputValueAsync("input[placeholder='Real Stock']");

            Assert.AreEqual(string.Empty, nameValue);
            Assert.AreEqual("0", realStockValue);
        }
    }
}
