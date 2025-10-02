using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using R5A08_TP1.Controllers;
using R5A08_TP1.Models.DataManager;
using R5A08_TP1.Models.DTO.Brands;
using R5A08_TP1.Models.DTO.Products;
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
    public class BrandsControllerMockTests
    {
        private AppDbContext context;
        private BrandsController controller;
        private Mock<IBrandRepository> brandRepositoryMock;
        private IMapper mapper;

        public BrandsControllerMockTests()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql();  

            brandRepositoryMock = new Mock<IBrandRepository>();

            controller = new BrandsController(brandRepositoryMock.Object);

            context = new AppDbContext();

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile(context));
            });
            mapper = config.CreateMapper();
        }

        private Brand brandInDb1;
        private Brand brandInDb2;

        [TestInitialize]
        public void TestInitialize()
        {
            // Given : On insère des marques en base de données.
            brandInDb1 = new Brand()
            {
                IdBrand = 1,
                NameBrand = "Aka"
            };

            brandInDb2 = new Brand()
            {
                IdBrand = 2,
                NameBrand = "Sungsam"
            };
        }

        [TestMethod()]
        public void ShouldGetBrandById()
        {
            // Given : Une marque en base de données.
            brandRepositoryMock.Setup(r => r.GetByIdAsync(brandInDb1.IdBrand)).ReturnsAsync(brandInDb1);

            // When : J'appelle la méthode get de mon API pour récupérer la marque.
            ActionResult<BrandDetailsDTO> action = controller.GetBrandById(brandInDb1.IdBrand).Result;

            // Then : On récupère la marque et le code de retour est 200.
            brandRepositoryMock.Verify(r => r.GetByIdAsync(brandInDb1.IdBrand), Times.Once, "La méthode n'a pas été utilisée.");

            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(ActionResult<BrandDetailsDTO>), "L'action doit être de type ActionResult<BrandDetailsDTO>");
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "Le result doit être de type OkObjectResult.");

            BrandDetailsDTO returnBrand = (action.Result as OkObjectResult)?.Value as BrandDetailsDTO;
            BrandDetailsDTO brandInDb = mapper.Map<BrandDetailsDTO>(brandInDb1);
            Assert.AreEqual(brandInDb, returnBrand);
        }

        [TestMethod()]
        public void GetBrandByIdShouldReturnNotFound()
        {
            // Given : Aucune marque en base de données.
            brandRepositoryMock.Setup(r => r.GetByIdAsync(0)).ReturnsAsync((Brand)null);

            // When : J'appelle la méthode get de mon API pour récupérer la marque.
            ActionResult<BrandDetailsDTO> action = controller.GetBrandById(0).Result;

            // Then : On récupère la marque et le code de retour est 404.
            brandRepositoryMock.Verify(r => r.GetByIdAsync(0), Times.Once, "La méthode n'a pas été utilisée.");

            Assert.IsNull(action.Value, "L'action doit être nulle.");
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "Le result doit être de type NotFoundResult.");
        }

        [TestMethod()]
        public void ShouldGetBrandByName()
        {
            // Given : Une marque en base de données.
            brandRepositoryMock.Setup(r => r.GetBrandByNameAsync(brandInDb1.NameBrand)).ReturnsAsync(brandInDb1);

            // When : J'appelle la méthode get de mon API pour récupérer la marque.
            ActionResult<BrandDetailsDTO> action = controller.GetBrandByName(brandInDb1.NameBrand).Result;

            // Then : On récupère la marque et le code de retour est 200.
            brandRepositoryMock.Verify(r => r.GetBrandByNameAsync(brandInDb1.NameBrand), Times.Once, "La méthode n'a pas été utilisée.");

            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(ActionResult<BrandDetailsDTO>), "L'action doit être de type ActionResult<BrandDetailsDTO>");
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "Le result doit être de type OkObjectResult.");

            BrandDetailsDTO returnBrand = (action.Result as OkObjectResult)?.Value as BrandDetailsDTO;
            BrandDetailsDTO brandInDb = mapper.Map<BrandDetailsDTO>(brandInDb1);
            Assert.AreEqual(brandInDb, returnBrand);
        }

        [TestMethod()]
        public void GetBrandByNameShouldReturnNotFound()
        {
            // Given : Aucune marque en base de données.
            brandRepositoryMock.Setup(r => r.GetBrandByNameAsync("a")).ReturnsAsync((Brand)null);

            // When : J'appelle la méthode get de mon API pour récupérer la marque.
            ActionResult<BrandDetailsDTO> action = controller.GetBrandByName("a").Result;

            // Then : On récupère la marque et le code de retour est 404.
            brandRepositoryMock.Verify(r => r.GetBrandByNameAsync("a"), Times.Once, "La méthode n'a pas été utilisée.");

            Assert.IsNull(action.Value, "L'action doit être nulle.");
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "Le result doit être de type NotFoundResult.");
        }

        [TestMethod()]
        public void ShouldGetAllBrands()
        {
            // Given : Deux marques en base de données.
            List<Brand> brands = new List<Brand>() { brandInDb1, brandInDb2 };
            brandRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(brands);

            // When : J'appelle la méthode get de mon API pour récupérer la marque.
            ActionResult<IEnumerable<BrandDTO>> action = controller.GetBrands().Result;

            // Then : On récupère la marque et le code de retour est 200.
            brandRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once, "La méthode n'a pas été utilisée.");

            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(ActionResult<IEnumerable<BrandDTO>>), "L'action doit être de type ActionResult<IEnumerable<BrandDTO>>.");
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "Le result doit être de type OkObjectResult.");

            List<BrandDTO> returnBrands = (action.Result as OkObjectResult)?.Value as List<BrandDTO>;
            Assert.AreEqual(2, returnBrands.Count(), $"La liste de marques dans la base de données doit être de {2}.");

            List<BrandDTO> brandsInDb = mapper.Map<IEnumerable<BrandDTO>>(brands).ToList();
            CollectionAssert.AreEqual(brandsInDb, returnBrands, "La liste de marques dans la base de données et la liste de marques retournée ne sont pas les mêmes.");
        }

        [TestMethod()]
        public void ShouldCreateBrand()
        {
            // Given : On créer une marque à ajouter en base de données.
            Brand newBrand = new Brand()
            {
                IdBrand = 3,
                NameBrand = "Youyou",
            };
            BrandDTO newBrandDto = new BrandDTO()
            {
                NameBrand = "Youyou",
            };

            brandRepositoryMock.Setup(r => r.AddAsync(newBrand));

            // When : J'appelle la méthode get de mon API pour récupérer la marque.
            ActionResult<Brand> action = controller.PostBrand(newBrandDto).Result;

            // Then : On récupère la marque et le code de retour est 201.

            // On filtre la marque avec tous les champs car l'Id n'existe pas dans le DTO.
            brandRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Brand>()), Times.Once, "La méthode n'a pas été utilisée.");

            Assert.IsNotNull(newBrand, "La marque n'a pas été ajoutée en base de données.");
            Assert.IsInstanceOfType(action, typeof(ActionResult<Brand>), "Result n'est pas un action result.");
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult.");
        }

        [TestMethod()]
        public void ShouldDeleteBrand()
        {
            // Given : Une marque en base de données.
            brandRepositoryMock.Setup(r => r.GetByIdAsync(brandInDb1.IdBrand)).ReturnsAsync(brandInDb1);
            brandRepositoryMock.Setup(r => r.DeleteAsync(brandInDb1));

            // When : J'appelle la méthode get de mon API pour récupérer le marque.
            IActionResult action = controller.DeleteBrand(brandInDb1.IdBrand).Result;

            // Then : On récupère la marque et le code de retour est 204.
            brandRepositoryMock.Verify(r => r.GetByIdAsync(brandInDb1.IdBrand), Times.Once, "La méthode GetByIdAsync n'a pas été utilisée.");
            brandRepositoryMock.Verify(r => r.DeleteAsync(brandInDb1), Times.Once, "La méthode DeleteAsync n'a pas été utilisée.");

            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result n'est pas un NoContentResult.");

            Brand brandToGet = context.Brands.Where(p => p.IdBrand == brandInDb1.IdBrand).FirstOrDefault();
            Assert.IsNull(brandToGet, "La marque n'a pas été supprimée.");
        }

        [TestMethod()]
        public void ShouldNotDeleteBrandBecauseBrandDoesNotExist()
        {
            // Given : Aucune marque en base de données.
            brandRepositoryMock.Setup(r => r.GetByIdAsync(0)).ReturnsAsync((Brand)null);
            brandRepositoryMock.Setup(r => r.DeleteAsync((Brand)null));

            // When : J'appelle la méthode get de mon API pour récupérer la marque.
            IActionResult action = controller.DeleteBrand(0).Result;

            // Then : On récupère la marque et le code de retour est 404.
            brandRepositoryMock.Verify(r => r.GetByIdAsync(0), Times.Once, "La méthode GetByIdAsync n'a pas été utilisée.");
            brandRepositoryMock.Verify(r => r.DeleteAsync((Brand)null), Times.Never, "La méthode DeleteAsync a été utilisée alors qu'elle n'aurait pas dû l'être.");

            Assert.IsInstanceOfType(action, typeof(NotFoundResult), "Result n'est pas un NotFoundResult.");
        }

        [TestMethod()]
        public void ShouldUpdateBrand()
        {
            // Given : Une marque à mettre à jour.
            BrandDetailsDTO brandToUpdateDto = new BrandDetailsDTO()
            {
                IdBrand = brandInDb1.IdBrand,
                NameBrand = "Fourchette"
            };
            Brand brandToUpdate = new Brand() 
            { 
                IdBrand = brandInDb1.IdBrand, 
                NameBrand = "Fourchette"
            };

            brandRepositoryMock.Setup(r => r.GetByIdAsync(brandToUpdate.IdBrand)).ReturnsAsync(brandInDb1);
            brandRepositoryMock.Setup(r => r.UpdateAsync(brandToUpdate, brandInDb1));

            // When : J'appelle la méthode get de mon API pour récupérer la marque.
            IActionResult action = controller.PutBrand(brandToUpdateDto.IdBrand, brandToUpdateDto).Result;

            // Then : On récupère la marque et le code de retour est 204.
            brandRepositoryMock.Verify(r => r.GetByIdAsync(brandToUpdate.IdBrand), Times.Once, "La méthode GetByIdAsync n'a pas été utilisée.");
            brandRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Brand>(), brandInDb1), Times.Once, "La méthode UpdateAsync n'a pas été utilisée.");

            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result n'est pas un NoContentResult.");
        }

        [TestMethod]
        public void ShouldNotUpdateBrandBecauseIdInUrlIsDifferent()
        {
            // Given : Une marque à mettre à jour.
            BrandDetailsDTO updatedBrandDTO = mapper.Map<BrandDetailsDTO>(brandInDb1);

            // When : On appelle la méthode Put du controller pour mettre à jour la marque,
            // mais en précisant un ID différent de celui de la marque enregistré
            IActionResult action = controller.PutBrand(0, updatedBrandDTO).Result;

            // Then : On ne renvoie rien et le code HTTP est BadRequest (400).
            brandRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never, "La méthode GetByIdAsync a été utilisée alors qu'elle n'aurait pas dû l'être.");
            brandRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Brand>(), It.IsAny<Brand>()), Times.Never, "La méthode UpdateAsync a été utilisée alors qu'elle n'aurait pas dû l'être.");

            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(BadRequestResult));
        }

        [TestMethod]
        public void ShouldNotUpdateBrandBecauseBrandDoesNotExist()
        {
            // Given : Une marque à mettre à jour.
            BrandDetailsDTO updatedBrandDTO = mapper.Map<BrandDetailsDTO>(brandInDb2);
            updatedBrandDTO.IdBrand = 0;
            brandRepositoryMock.Setup(r => r.GetByIdAsync(updatedBrandDTO.IdBrand)).ReturnsAsync((Brand)null);
            brandRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Brand>(), It.IsAny<Brand>()));

            // When : On appelle la méthode Put de l'API pour mettre à jour un produit qui n'est pas enregistré.
            IActionResult action = controller.PutBrand(updatedBrandDTO.IdBrand, updatedBrandDTO).Result;

            // Then : On ne renvoie rien et le code HTTP est NotFound (404).
            brandRepositoryMock.Verify(r => r.GetByIdAsync(updatedBrandDTO.IdBrand), Times.Once, "La méthode GetByIdAsync n'a pas été utilisée.");
            brandRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Brand>(), It.IsAny<Brand>()), Times.Never, "La méthode UpdateAsync a été utilisée alors qu'elle n'aurait pas dû l'être.");

            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NotFoundResult));
        }
    }
}