using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using R5A08_TP1.Controllers;
using R5A08_TP1.Models.DataManager;
using R5A08_TP1.Models.DTO.Products;
using R5A08_TP1.Models.DTO.ProductTypes;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Mapper;
using R5A08_TP1.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R5A08_TP1.Controllers.Tests
{
    [TestClass()]
    [TestCategory("Integration")]
    public class ProductTypesControllerTests
    {
        private AppDbContext context;
        private ProductTypesController controller;
        private IProductTypeRepository productTypeRepository;
        private IMapper mapper;

        public ProductTypesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql();
            context = new AppDbContext();

            productTypeRepository = new ProductTypeManager(context);

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile(context));
            });
            mapper = config.CreateMapper();

            controller = new ProductTypesController(productTypeRepository);
        }

        private ProductType productTypeInDb1 = new ProductType();
        private ProductType productTypeInDb2 = new ProductType();
        private int numberOfProductTypesInDb = 0;

        [TestInitialize]
        public void TestInitialize()
        {
            productTypeInDb1 = new ProductType()
            {
                NameProductType = "Nourriture"
            };

            productTypeInDb2 = new ProductType()
            {
                NameProductType = "Extérieur"
            };

            context.ProductTypes.Add(productTypeInDb1);
            context.ProductTypes.Add(productTypeInDb2);

            context.SaveChanges();

            numberOfProductTypesInDb = context.ProductTypes.Count();
        }

        [TestMethod()]
        public void ShouldGetProductType()
        {
            // Given : Un type de produit en base de données.
            // Pas nécessaire, fait dans le TestInitialize.

            // When : J'appelle la méthode get de mon API pour récupérer le type de produit.
            ActionResult<ProductTypeDetailsDTO> action = controller.GetProductTypeById(productTypeInDb1.IdProductType).Result;

            // Then : On récupère le type de produit et le code de retour est 200.
            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(ActionResult<ProductTypeDetailsDTO>), "L'action doit être de type ActionResult<ProductTypeDetailsDTO>");
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "Le result doit être de type OkObjectResult.");

            ProductTypeDetailsDTO returnProductType = (action.Result as OkObjectResult)?.Value as ProductTypeDetailsDTO;
            ProductTypeDetailsDTO productTypeInDb = mapper.Map<ProductTypeDetailsDTO>(((OkObjectResult)action.Result).Value);
            Assert.AreEqual(productTypeInDb, returnProductType);
        }

        [TestMethod()]
        public void GetProductTypeShouldReturnNotFound()
        {
            // When : J'appelle la méthode get de mon API pour récupérer le type de produit.
            ActionResult<ProductTypeDetailsDTO> action = controller.GetProductTypeById(0).Result;

            // Then : On récupère le type de produit et le code de retour est 404.
            Assert.IsNull(action.Value, "L'action doit être nulle.");
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "Le result doit être de type NotFoundResult.");
        }

        [TestMethod()]
        public void ShouldGetAllProductTypes()
        {
            // Given : Deux type de produits en base de données.
            // Pas nécessaire, fait dans le TestInitialize.

            // When : J'appelle la méthode get de mon API pour récupérer le type de produit.
            ActionResult<IEnumerable<ProductTypeDTO>> action = controller.GetProductTypes().Result;

            // Then : On récupère le type de produit et le code de retour est 200.
            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(ActionResult<IEnumerable<ProductTypeDTO>>), "L'action doit être de type ActionResult<IEnumerable<ProductTypeDTO>>.");
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "Le result doit être de type OkObjectResult.");

            List<ProductTypeDTO> returnProductTypes = (action.Result as OkObjectResult)?.Value as List<ProductTypeDTO>;
            Assert.AreEqual(numberOfProductTypesInDb, returnProductTypes.Count(), $"La liste de type de produits dans la base de données doit être de {numberOfProductTypesInDb}.");

            List<ProductTypeDTO> productTypesInDb = mapper.Map<IEnumerable<ProductTypeDTO>>(((OkObjectResult)action.Result).Value).ToList();
            CollectionAssert.AreEqual(productTypesInDb, returnProductTypes, "La liste de type de produits dans la base de données et la liste de type de produits retournée ne sont pas les mêmes.");
        }

        [TestMethod()]
        public void ShouldCreateProductType()
        {
            // Given : On créer un type de produit à ajouter en base de données.
            ProductTypeDTO newProductTypeInDb = new ProductTypeDTO()
            {
                NameProductType = "Boisson"
            };

            // When : J'appelle la méthode get de mon API pour récupérer le type de produit.
            ActionResult<ProductType> action = controller.PostProductType(newProductTypeInDb).Result;

            // Then : On récupère le type de produit et le code de retour est 200.

            // On filtre le type de produit avec tous les champs car l'Id n'existe pas dans le DTO.
            ProductType expectedProductType = context.ProductTypes.FirstOrDefault(pt => pt.NameProductType == newProductTypeInDb.NameProductType);

            Assert.IsNotNull(expectedProductType, "Le type de produit n'a pas été ajouté en base de données.");
            Assert.IsInstanceOfType(action, typeof(ActionResult<ProductType>), "Result n'est pas un action result.");
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult.");

            ProductType returnProductType = (action.Result as CreatedAtActionResult)?.Value as ProductType;
            Assert.AreEqual(expectedProductType, returnProductType, "Les types de produits ne sont pas identiques.");
        }

        // On devrait aussi tester les cas où la création de produit échoue (ex : nom manquant, stock négatif, etc.)

        [TestMethod()]
        public void ShouldDeleteProductType()
        {
            // Given : Un type de produit en base de données.
            // Pas nécessaire, fait dans le TestInitialize.

            // When : J'appelle la méthode get de mon API pour récupérer le type de produit.
            IActionResult action = controller.DeleteProductType(productTypeInDb1.IdProductType).Result;

            // Then : On récupère le type de produit et le code de retour est 200.
            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result n'est pas un NoContentResult.");

            ProductType productTypeToGet = context.ProductTypes.Where(p => p.IdProductType == productTypeInDb1.IdProductType).FirstOrDefault();
            Assert.IsNull(productTypeToGet, "Le type de produit n'a pas été supprimé.");
        }

        [TestMethod()]
        public void ShouldNotDeleteProductTypeBecauseProductTypeDoesNotExist()
        {
            // When : J'appelle la méthode get de mon API pour récupérer le type de produit.
            IActionResult action = controller.DeleteProductType(0).Result;

            // Then : On récupère le type de produit et le code de retour est 404.
            Assert.IsInstanceOfType(action, typeof(NotFoundResult), "Result n'est pas un NotFoundResult.");
        }

        [TestMethod()]
        public void ShouldUpdateProductType()
        {
            // Given : Un type de produit à mettre à jour.
            ProductTypeDetailsDTO productTypeToUpdate = new ProductTypeDetailsDTO()
            {
                IdProductType = productTypeInDb1.IdProductType,
                NameProductType = "Magie"
            };

            // When : J'appelle la méthode get de mon API pour récupérer le type de produit.
            IActionResult action = controller.PutProductType(productTypeToUpdate.IdProductType, productTypeToUpdate).Result;

            // Then : On récupère le type de produit et le code de retour est 200.
            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result n'est pas un NoContentResult.");

            ProductType productTypeToGet = context.ProductTypes.Where(p => p.IdProductType == productTypeToUpdate.IdProductType).FirstOrDefault();
            ProductType productTypeInDb = mapper.Map<ProductType>(productTypeToUpdate);

            productTypeToGet.RelatedProductsProductType = null;
            productTypeInDb.RelatedProductsProductType = null;

            Assert.AreEqual(productTypeToGet, productTypeInDb, "Le type de produit a mal été modifié.");
        }

        [TestMethod]
        public void ShouldNotUpdateProductTypeBecauseIdInUrlIsDifferent()
        {
            // Given : Un type de produit à mettre à jour.
            ProductTypeDetailsDTO updatedProductTypeDTO = mapper.Map<ProductTypeDetailsDTO>(productTypeInDb1);

            // When : On appelle la méthode Put du controller pour mettre à jour le type de produit,
            // mais en précisant un ID différent de celui du type de produit enregistré
            IActionResult action = controller.PutProductType(0, updatedProductTypeDTO).Result;

            // Then : On ne renvoie rien et le code HTTP est BadRequest (400).
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(BadRequestResult));
        }

        [TestMethod]
        public void ShouldNotUpdateProductTypeBecauseProductTypeDoesNotExist()
        {
            // Given : Un type de produit à mettre à jour.
            ProductTypeDetailsDTO updatedProductTypeDTO = mapper.Map<ProductTypeDetailsDTO>(productTypeInDb2);
            updatedProductTypeDTO.IdProductType = 0;

            // When : On appelle la méthode Put de l'API pour mettre à jour un type de produit qui n'est pas enregistré.
            IActionResult action = controller.PutProductType(updatedProductTypeDTO.IdProductType, updatedProductTypeDTO).Result;

            // Then : On ne renvoie rien et le code HTTP est NotFound (404).
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NotFoundResult));
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.ProductTypes.RemoveRange(context.ProductTypes);
            context.SaveChanges();
        }
    }
}