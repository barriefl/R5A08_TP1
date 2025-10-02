using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorApp.PlaywrightTests
{
    [TestClass]
    public class CreateProductTests : PageTest
    {
        private const string BaseUrl = "http://localhost:5020";

        [TestInitialize]
        public async Task TestInitialize()
        {
            await Page.GotoAsync($"{BaseUrl}/create-product");
        }

        [TestMethod]
        public async Task CreateProduct_SuccessfulSubmission_ShowsSuccessToast()
        {
            await Page.GotoAsync($"{BaseUrl}/create-product");

            await Page.FillAsync("input[placeholder='Name']", "Test Product");
            await Page.FillAsync("input[placeholder='Description']", "A nice test product");
            await Page.FillAsync("input[placeholder='Brand']", "Test Brand");
            await Page.FillAsync("input[placeholder='Product Type']", "Test Type");
            await Page.FillAsync("input[placeholder='Photo Name']", "photo.jpg");
            await Page.FillAsync("input[placeholder='Uri Photo']", "/images/photo.jpg");
            await Page.FillAsync("input[placeholder='Real Stock']", "10");
            await Page.FillAsync("input[placeholder='Min Stock']", "5");
            await Page.FillAsync("input[placeholder='Max Stock']", "15");

            await Page.ClickAsync("button:has-text('Create')");

            var toast = await Page.WaitForSelectorAsync("div.toast-success", new PageWaitForSelectorOptions { Timeout = 5000 });
            Assert.IsNotNull(toast, "Success toast did not appear");

            var toastText = await toast.InnerTextAsync();
            Assert.IsTrue(toastText.Contains("Product created with success"), "Success toast message not correct");
        }

        [TestMethod]
        public async Task ClearForm_ResetsAllFields()
        {
            await Page.GotoAsync($"{BaseUrl}/create-product");

            await Page.FillAsync("input[placeholder='Name']", "Test Product");
            await Page.FillAsync("input[placeholder='Description']", "A nice test product");

            await Page.ClickAsync("button:has-text('Clear Form')");

            string nameValue = await Page.InputValueAsync("input[placeholder='Name']");
            string descValue = await Page.InputValueAsync("input[placeholder='Description']");
            string realStockValue = await Page.InputValueAsync("input[placeholder='Real Stock']");

            Assert.AreEqual(string.Empty, nameValue);
            Assert.AreEqual(string.Empty, descValue);
            Assert.AreEqual("0", realStockValue);
        }
    }
}
