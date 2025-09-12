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
        private ProduitsDbContext context;
        private ProduitsController controller;
        private IDataRepository<Produit> dataRepository;

        [TestInitialize]
        public void InitialisationDesTests()
        {
            var builder = new DbContextOptionsBuilder<ProduitsDbContext>().UseNpgsql();
            context = new ProduitsDbContext();
            dataRepository = new ProduitManager(context);
            controller = new ProduitsController(dataRepository);
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Produits.RemoveRange(context.Produits);
            context.SaveChanges();
        }

        [TestMethod()]
        public void GetUtilisateursTest()
        {
            // Given : Un produit en base de données.
            Produit produitInDb = new Produit()
            {
                NomProduit = "Chaise",
                DescriptionProduit = "Une superbe chaise",
                NomPhotoProduit = "Une superbe chaise bleu",
                UriPhotoProduit = "https://ikea.fr/chaise.jpg"
            };

            context.Produits.Add(produitInDb);
            context.SaveChanges();

            // When : J'appelle la méthode get de mon API pour récupérer le produit.
            ActionResult<Produit> action = controller.GetProduitById(produitInDb.IdProduit).Result;

            // Then : On récupère le produit et le code de retour est 200.
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action.Value, typeof(Produit));

            Produit returnProduct = action.Value;
            Assert.AreEqual(produitInDb.NomProduit, returnProduct.NomProduit);
            //Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult));
        }

        [TestMethod()]
        public void ProductShouldReturnNotFound()
        {
            // When : J'appelle la méthode get de mon API pour récupérer le produit.
            ActionResult<Produit> action = controller.GetProduitById(0).Result;

            // Then : On récupère le produit et le code de retour est 200.
            Assert.IsNull(action.Value);
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult));
        }
    }
}