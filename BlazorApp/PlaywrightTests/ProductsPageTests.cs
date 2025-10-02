using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace BlazorApp.PlaywrightTests
{
    [TestClass]
    public class ProductsPageTests : PageTest
    {
        private const string ProductsUrl = "http://localhost:5000/products";

        [TestInitialize]
        public async Task SetUpAsync()
        {
            await Page.GotoAsync(ProductsUrl);
        }

        [TestMethod]
        public async Task Should_Load_Products_Page()
        {
            var title = await Page.TitleAsync();
            Assert.AreEqual("Products", title);

            var rows = await Page.QuerySelectorAllAsync("table tbody tr");
            Assert.IsTrue(rows.Count > 0, "La liste des produits devrait contenir au moins une ligne.");
        }

        [TestMethod]
        public async Task Should_Search_Product_By_Name()
        {
            var input = await Page.QuerySelectorAsync("input[placeholder='Search a product...']");
            await input.FillAsync("Test");
            await input.PressAsync("Enter");

            var rows = await Page.QuerySelectorAllAsync("table tbody tr");
            Assert.IsTrue(rows.Count > 0, "Aucun produit trouvé après la recherche.");
        }

        [TestMethod]
        public async Task Should_Navigate_To_Update_Page_On_Click()
        {
            var updateButton = await Page.QuerySelectorAsync("table tbody tr:first-child td button:has-text(\"Update\")");
            await updateButton.ClickAsync();

            StringAssert.Contains(Page.Url, "/update-product/");
        }

        [TestMethod]
        public async Task Should_Confirm_And_Delete_Product()
        {
            var deleteButton = await Page.QuerySelectorAsync("table tbody tr:first-child td button:has-text(\"Delete\")");
            await deleteButton.ClickAsync();

            var dialog = await Page.WaitForSelectorAsync("div[role='dialog']");
            Assert.IsNotNull(dialog, "La boîte de confirmation n'est pas apparue.");

            var confirmButton = await dialog.QuerySelectorAsync("button:has-text(\"Yes\")");
            if (confirmButton != null)
                await confirmButton.ClickAsync();

            var toast = await Page.WaitForSelectorAsync("div.toast-message:has-text(\"Product deleted\")");
            Assert.IsNotNull(toast, "Le toast de succès n'a pas été affiché.");
        }

        [TestMethod]
        public async Task Debug_With_Pause()
        {
            await Page.PauseAsync();
        }
    }
}
