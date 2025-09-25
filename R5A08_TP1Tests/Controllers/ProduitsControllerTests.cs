using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using R5A08_TP1.Controllers;
using R5A08_TP1.Models.DataManager;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R5A08_TP1.Controllers.Tests
{
    [TestClass()]
    public class ProduitsControllerTests
    {
        private AppDbContext context;
        private ProductsController controller;
        private IWriteDataRepository<Product> dataRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql();
            context = new AppDbContext();
            dataRepository = new GetDataManager(context);
            controller = new ProductsController(dataRepository);
        }

        [TestMethod()]
        public void ShouldGetProduct()
        {
            // Given : Un produit en base de données.
            Product produitInDb = new Product()
            {
                NomProduit = "Chaise",
                DescriptionProduit = "Une superbe chaise",
                NomPhotoProduit = "Une superbe chaise bleu",
                UriPhotoProduit = "https://ikea.fr/chaise.jpg"
            };

            context.Products.Add(produitInDb);
            context.SaveChanges();

            // When : J'appelle la méthode get de mon API pour récupérer le produit.
            ActionResult<Product> action = controller.GetProduitById(produitInDb.IdProduit).Result;

            // Then : On récupère le produit et le code de retour est 200.
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action.Value, typeof(Product));

            Product returnProduct = action.Value;
            Assert.AreEqual(produitInDb.NomProduit, returnProduct.NomProduit);
            //Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult));
        }

        [TestMethod()]
        public void ShouldGetAllProducts()
        {
            // Given : Deux produits en base de données.
            Product produitInDb1 = new Product()
            {
                NomProduit = "Chaise",
                DescriptionProduit = "Une superbe chaise",
                NomPhotoProduit = "Une superbe chaise bleu",
                UriPhotoProduit = "https://ikea.fr/chaise.jpg"
            };
            Product produitInDb2 = new Product()
            {
                NomProduit = "Table",
                DescriptionProduit = "Une superbe table",
                NomPhotoProduit = "Une superbe table ronde",
                UriPhotoProduit = "https://ikea.fr/table.jpg"
            };

            context.Products.Add(produitInDb1);
            context.Products.Add(produitInDb2);
            context.SaveChanges();

            // When : J'appelle la méthode get de mon API pour récupérer le produit.
            ActionResult<IEnumerable<Product>> action = controller.GetProducts().Result;

            // Then : On récupère le produit et le code de retour est 200.
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action.Value, typeof(IEnumerable<Product>));
            List<Product> returnProducts = action.Value.ToList();
            Assert.AreEqual(2, returnProducts.Count);
            Assert.AreEqual(produitInDb1.NomProduit, returnProducts[0].NomProduit);
            Assert.AreEqual(produitInDb2.NomProduit, returnProducts[1].NomProduit);
        }

        [TestMethod()]
        public void GetProductShouldReturnNotFound()
        {
            // When : J'appelle la méthode get de mon API pour récupérer le produit.
            ActionResult<Product> action = controller.GetProductById(0).Result;

            // Then : On récupère le produit et le code de retour est 404.
            Assert.IsNull(action.Value);
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void ShouldCreateProduct()
        {
            // Given : Un produit en base de données.
            Product produitInDb = new Product()
            {
                NomProduit = "Chaise",
                DescriptionProduit = "Une superbe chaise",
                NomPhotoProduit = "Une superbe chaise bleu",
                UriPhotoProduit = "https://ikea.fr/chaise.jpg"
            };

            // When : J'appelle la méthode get de mon API pour récupérer le produit.
            ActionResult<Product> action = controller.PostProduct(produitInDb).Result;

            // Then : On récupère le produit et le code de retour est 200.
            Product productToGet = context.Products.Where(p => p.IdProduit == produitInDb.IdProduit).FirstOrDefault();
            Assert.IsInstanceOfType(action, typeof(ActionResult<Product>), "Result n'est pas un action result.");
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult.");
            Assert.AreEqual(produitInDb, productToGet, "Les produits ne sont pas identiques.");
        }

        [TestMethod()]
        public void ShouldDeleteProduct() 
        { 
            // Given : Un produit en base de données.
            Product produitInDb = new Product()
            {
                NomProduit = "Chaise",
                DescriptionProduit = "Une superbe chaise",
                NomPhotoProduit = "Une superbe chaise bleu",
                UriPhotoProduit = "https://ikea.fr/chaise.jpg"
            };
            context.Products.Add(produitInDb);
            context.SaveChanges();
            // When : J'appelle la méthode get de mon API pour récupérer le produit.
            IActionResult action = controller.DeleteProduit(produitInDb.IdProduit).Result;
            // Then : On récupère le produit et le code de retour est 200.
            Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result n'est pas un OkObjectResult.");
            Product productToGet = context.Products.Where(p => p.IdProduit == produitInDb.IdProduit).FirstOrDefault();
            Assert.IsNull(productToGet, "Le produit n'a pas été supprimé.");
        }

        [TestMethod()]
        public void ShouldNotDeleteProductBecauseProductDoesNotExist()
        {
            // When : J'appelle la méthode get de mon API pour récupérer le produit.
            IActionResult action = controller.DeleteProduct(0).Result;
            // Then : On récupère le produit et le code de retour est 404.
            Assert.IsInstanceOfType(action, typeof(NotFoundResult), "Result n'est pas un NotFoundResult.");
        }   

        [TestMethod()]
        public void ShouldUpdateProduct()
        {
            // Given : Un produit en base de données.
            Product produitInDb = new Product()
            {
                NomProduit = "Chaise",
                DescriptionProduit = "Une superbe chaise",
                NomPhotoProduit = "Une superbe chaise bleu",
                UriPhotoProduit = "https://ikea.fr/chaise.jpg"
            };
            context.Products.Add(produitInDb);
            context.SaveChanges();
            Product produitToUpdate = new Product()
            {
                IdProduit = produitInDb.IdProduit,
                NomProduit = "Chaise Modifiée",
                DescriptionProduit = "Une superbe chaise modifiée",
                NomPhotoProduit = "Une superbe chaise bleu modifiée",
                UriPhotoProduit = "https://ikea.fr/chaise_modifiee.jpg"
            };
            // When : J'appelle la méthode get de mon API pour récupérer le produit.
            IActionResult action = controller.PutProduit(produitInDb.IdProduit, produitToUpdate).Result;
            // Then : On récupère le produit et le code de retour est 200.
            Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result n'est pas un NoContentResult.");
            Product productToGet = context.Products.Where(p => p.IdProduit == produitInDb.IdProduit).FirstOrDefault();
            Assert.AreEqual(produitToUpdate.NomProduit, productToGet.NomProduit, "Le nom du produit n'a pas été modifié.");
            Assert.AreEqual(produitToUpdate.DescriptionProduit, productToGet.DescriptionProduit, "La description du produit n'a pas été modifiée.");
            Assert.AreEqual(produitToUpdate.NomPhotoProduit, productToGet.NomPhotoProduit, "Le nom de la photo du produit n'a pas été modifié.");
            Assert.AreEqual(produitToUpdate.UriPhotoProduit, productToGet.UriPhotoProduit, "L'uri de la photo du produit n'a pas été modifiée.");
        }

        [TestMethod()]
        public void ShouldNotUpdateProductBecauseProductDoesNotExist()
        {
            // Given : Un produit en base de données.
            Product produitToUpdate = new Product()
            {
                IdProduit = 0,
                NomProduit = "Chaise Modifiée",
                DescriptionProduit = "Une superbe chaise modifiée",
                NomPhotoProduit = "Une superbe chaise bleu modifiée",
                UriPhotoProduit = "https://ikea.fr/chaise_modifiee.jpg"
            };
            // When : J'appelle la méthode get de mon API pour récupérer le produit.
            IActionResult action = controller.PutProduit(produitToUpdate.IdProduit, produitToUpdate).Result;
            // Then : On récupère le produit et le code de retour est 404.
            Assert.IsInstanceOfType(action, typeof(NotFoundResult), "Result n'est pas un NotFoundResult.");
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Products.RemoveRange(context.Products);
            context.SaveChanges();
        }
    }
}