using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
    [TestCategory("Mock")]
    public class ProductTypesControllerMockTests
    {
        private AppDbContext context;
        private ProductTypesController controller;
        private Mock<IProductTypeRepository> productTypeRepositoryMock;
        private IMapper mapper;

        public ProductTypesControllerMockTests()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql();      

            productTypeRepositoryMock = new Mock<IProductTypeRepository>();

            controller = new ProductTypesController(productTypeRepositoryMock.Object);

            context = new AppDbContext();

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile(context));
            });
            mapper = config.CreateMapper();
        }

        private ProductType productTypeInDb1;
        private ProductType productTypeInDb2;

        [TestInitialize]
        public void TestInitialize()
        {
            productTypeInDb1 = new ProductType()
            {
                IdProductType = 1,
                NameProductType = "Nourriture"
            };

            productTypeInDb2 = new ProductType()
            {
                IdProductType = 2,
                NameProductType = "Extérieur"
            };
        }

        [TestMethod()]
        public void ShouldGetProductTypeById()
        {
            // Given : Un type de produit en base de données.
            productTypeRepositoryMock.Setup(r => r.GetByIdAsync(productTypeInDb1.IdProductType)).ReturnsAsync(productTypeInDb1);

            // When : J'appelle la méthode get de mon API pour récupérer le type de produit.
            ActionResult<ProductTypeDetailsDTO> action = controller.GetProductTypeById(productTypeInDb1.IdProductType).Result;

            // Then : On récupère le type de produit et le code de retour est 200.
            productTypeRepositoryMock.Verify(r => r.GetByIdAsync(productTypeInDb1.IdProductType), Times.Once, "La méthode n'a pas été utilisée.");

            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(ActionResult<ProductTypeDetailsDTO>), "L'action doit être de type ActionResult<ProductTypeDetailsDTO>");
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "Le result doit être de type OkObjectResult.");

            ProductTypeDetailsDTO returnProductType = (action.Result as OkObjectResult)?.Value as ProductTypeDetailsDTO;
            ProductTypeDetailsDTO productTypeInDb = mapper.Map<ProductTypeDetailsDTO>(productTypeInDb1);
            Assert.AreEqual(productTypeInDb, returnProductType);
        }

        [TestMethod()]
        public void GetProductTypeByIdShouldReturnNotFound()
        {
            // Given : Aucun type de produit en base de données.
            productTypeRepositoryMock.Setup(r => r.GetByIdAsync(0)).ReturnsAsync((ProductType)null);

            // When : J'appelle la méthode get de mon API pour récupérer le type de produit.
            ActionResult<ProductTypeDetailsDTO> action = controller.GetProductTypeById(0).Result;

            // Then : On récupère le type de produit et le code de retour est 404.
            productTypeRepositoryMock.Verify(r => r.GetByIdAsync(0), Times.Once, "La méthode n'a pas été utilisée.");

            Assert.IsNull(action.Value, "L'action doit être nulle.");
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "Le result doit être de type NotFoundResult.");
        }

        [TestMethod()]
        public void ShouldGetProductTypeByName()
        {
            // Given : Un type de produit en base de données.
            productTypeRepositoryMock.Setup(r => r.GetProductTypeByNameAsync(productTypeInDb1.NameProductType)).ReturnsAsync(productTypeInDb1);

            // When : J'appelle la méthode get de mon API pour récupérer le type de produit.
            ActionResult<ProductTypeDetailsDTO> action = controller.GetProductTypeByName(productTypeInDb1.NameProductType).Result;

            // Then : On récupère le type de produit et le code de retour est 200.
            productTypeRepositoryMock.Verify(r => r.GetProductTypeByNameAsync(productTypeInDb1.NameProductType), Times.Once, "La méthode n'a pas été utilisée.");

            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(ActionResult<ProductTypeDetailsDTO>), "L'action doit être de type ActionResult<ProductTypeDetailsDTO>");
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "Le result doit être de type OkObjectResult.");

            ProductTypeDetailsDTO returnProductType = (action.Result as OkObjectResult)?.Value as ProductTypeDetailsDTO;
            ProductTypeDetailsDTO productTypeInDb = mapper.Map<ProductTypeDetailsDTO>(productTypeInDb1);
            Assert.AreEqual(productTypeInDb, returnProductType);
        }

        [TestMethod()]
        public void GetProductTypeByNameShouldReturnNotFound()
        {
            // Given : Aucun type de produit en base de données.
            productTypeRepositoryMock.Setup(r => r.GetProductTypeByNameAsync("a")).ReturnsAsync((ProductType)null);

            // When : J'appelle la méthode get de mon API pour récupérer le type de produit.
            ActionResult<ProductTypeDetailsDTO> action = controller.GetProductTypeByName("a").Result;

            // Then : On récupère le type de produit et le code de retour est 404.
            productTypeRepositoryMock.Verify(r => r.GetProductTypeByNameAsync("a"), Times.Once, "La méthode n'a pas été utilisée.");

            Assert.IsNull(action.Value, "L'action doit être nulle.");
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "Le result doit être de type NotFoundResult.");
        }

        [TestMethod()]
        public void ShouldGetAllProductTypes()
        {
            // Given : Deux type de produits en base de données.
            List<ProductType> productTypes = new List<ProductType>() { productTypeInDb1, productTypeInDb2 };
            productTypeRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(productTypes);

            // When : J'appelle la méthode get de mon API pour récupérer le type de produit.
            ActionResult<IEnumerable<ProductTypeDTO>> action = controller.GetProductTypes().Result;

            // Then : On récupère le type de produit et le code de retour est 200.
            productTypeRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once, "La méthode n'a pas été utilisée.");

            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(ActionResult<IEnumerable<ProductTypeDTO>>), "L'action doit être de type ActionResult<IEnumerable<ProductTypeDTO>>.");
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "Le result doit être de type OkObjectResult.");

            List<ProductTypeDTO> returnProductTypes = (action.Result as OkObjectResult)?.Value as List<ProductTypeDTO>;
            Assert.AreEqual(2, returnProductTypes.Count(), $"La liste de type de produits dans la base de données doit être de {2}.");

            List<ProductTypeDTO> productTypesInDb = mapper.Map<IEnumerable<ProductTypeDTO>>(productTypes).ToList();
            CollectionAssert.AreEqual(productTypesInDb, returnProductTypes, "La liste de type de produits dans la base de données et la liste de type de produits retournée ne sont pas les mêmes.");
        }

        [TestMethod()]
        public void ShouldCreateProductType()
        {
            // Given : On créer un type de produit à ajouter en base de données.
            ProductType newProductType = new ProductType()
            {
                IdProductType = 3,
                NameProductType = "Boisson"
            };
            ProductTypeDTO newProductTypeDto = new ProductTypeDTO()
            {
                NameProductType = "Boisson"
            };

            productTypeRepositoryMock.Setup(r => r.AddAsync(newProductType));

            // When : J'appelle la méthode get de mon API pour récupérer le type de produit.
            ActionResult<ProductType> action = controller.PostProductType(newProductTypeDto).Result;

            // Then : On récupère le type de produit et le code de retour est 201.

            // On filtre le type de produit avec tous les champs car l'Id n'existe pas dans le DTO.
            productTypeRepositoryMock.Verify(r => r.AddAsync(It.Is<ProductType>(p => p.NameProductType == newProductType.NameProductType)), Times.Once, "La méthode n'a pas été utilisée.");
            
            Assert.IsInstanceOfType(action, typeof(ActionResult<ProductType>), "Result n'est pas un action result.");
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult.");
        }

        [TestMethod()]
        public void ShouldDeleteProductType()
        {
            // Given : Un type de produit en base de données.
            productTypeRepositoryMock.Setup(r => r.GetByIdAsync(productTypeInDb1.IdProductType)).ReturnsAsync(productTypeInDb1);
            productTypeRepositoryMock.Setup(r => r.DeleteAsync(productTypeInDb1));

            // When : J'appelle la méthode get de mon API pour récupérer le type de produit.
            IActionResult action = controller.DeleteProductType(productTypeInDb1.IdProductType).Result;

            // Then : On récupère le type de produit et le code de retour est 204.
            productTypeRepositoryMock.Verify(r => r.GetByIdAsync(productTypeInDb1.IdProductType), Times.Once, "La méthode n'a pas été utilisée.");
            productTypeRepositoryMock.Verify(r => r.DeleteAsync(productTypeInDb1), Times.Once, "La méthode n'a pas été utilisée.");

            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result n'est pas un NoContentResult.");

            ProductType productTypeToGet = context.ProductTypes.Where(p => p.IdProductType == productTypeInDb1.IdProductType).FirstOrDefault();
            Assert.IsNull(productTypeToGet, "Le type de produit n'a pas été supprimé.");
        }

        [TestMethod()]
        public void ShouldNotDeleteProductTypeBecauseProductTypeDoesNotExist()
        {
            // Given : Aucun type de produit en base de données.
            productTypeRepositoryMock.Setup(r => r.GetByIdAsync(0)).ReturnsAsync((ProductType)null);
            productTypeRepositoryMock.Setup(r => r.DeleteAsync((ProductType)null));

            // When : J'appelle la méthode get de mon API pour récupérer le type de produit.
            IActionResult action = controller.DeleteProductType(0).Result;

            // Then : On récupère le type de produit et le code de retour est 404.
            productTypeRepositoryMock.Verify(r => r.GetByIdAsync(0), Times.Once, "La méthode n'a pas été utilisée.");
            productTypeRepositoryMock.Verify(r => r.DeleteAsync((ProductType)null), Times.Never, "La méthode ne doit pas être utilisée.");

            Assert.IsInstanceOfType(action, typeof(NotFoundResult), "Result n'est pas un NotFoundResult.");
        }

        [TestMethod()]
        public void ShouldUpdateProductType()
        {
            // Given : Un type de produit à mettre à jour.
            ProductTypeDetailsDTO productTypeToUpdateDto = new ProductTypeDetailsDTO()
            {
                IdProductType = productTypeInDb1.IdProductType,
                NameProductType = "Magie"
            };
            ProductType productTypeToUpdate = new ProductType()
            {
                IdProductType = productTypeInDb1.IdProductType,
                NameProductType = "Magie"
            };

            productTypeRepositoryMock.Setup(r => r.GetByIdAsync(productTypeToUpdateDto.IdProductType)).ReturnsAsync(productTypeInDb1);
            productTypeRepositoryMock.Setup(r => r.UpdateAsync(productTypeToUpdate, productTypeInDb1));

            // When : J'appelle la méthode get de mon API pour récupérer le type de produit.
            IActionResult action = controller.PutProductType(productTypeToUpdateDto.IdProductType, productTypeToUpdateDto).Result;

            // Then : On récupère le type de produit et le code de retour est 204.
            productTypeRepositoryMock.Verify(r => r.GetByIdAsync(productTypeToUpdateDto.IdProductType), Times.Once, "La méthode n'a pas été utilisée.");
            productTypeRepositoryMock.Verify(r => r.UpdateAsync(It.Is<ProductType>(p => p.IdProductType == productTypeToUpdate.IdProductType && p.NameProductType == productTypeToUpdate.NameProductType), productTypeInDb1), Times.Once, "La méthode n'a pas été utilisée.");

            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result n'est pas un NoContentResult.");
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
            productTypeRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never, "La méthode ne doit pas être utilisée.");
            productTypeRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<ProductType>(), It.IsAny<ProductType>()), Times.Never, "La méthode ne doit pas être utilisée.");

            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(BadRequestResult));
        }

        [TestMethod]
        public void ShouldNotUpdateProductTypeBecauseProductTypeDoesNotExist()
        {
            // Given : Un type de produit à mettre à jour.
            ProductTypeDetailsDTO updatedProductTypeDTO = mapper.Map<ProductTypeDetailsDTO>(productTypeInDb2);
            updatedProductTypeDTO.IdProductType = 0;
            productTypeRepositoryMock.Setup(r => r.GetByIdAsync(updatedProductTypeDTO.IdProductType)).ReturnsAsync((ProductType)null);
            productTypeRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<ProductType>(), It.IsAny<ProductType>()));

            // When : On appelle la méthode Put de l'API pour mettre à jour un type de produit qui n'est pas enregistré.
            IActionResult action = controller.PutProductType(updatedProductTypeDTO.IdProductType, updatedProductTypeDTO).Result;

            // Then : On ne renvoie rien et le code HTTP est NotFound (404).
            productTypeRepositoryMock.Verify(r => r.GetByIdAsync(updatedProductTypeDTO.IdProductType), Times.Once, "La méthode n'a pas été utilisée.");
            productTypeRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<ProductType>(), It.IsAny<ProductType>()), Times.Never, "La méthode ne doit pas être utilisée.");

            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NotFoundResult));
        }
    }
}