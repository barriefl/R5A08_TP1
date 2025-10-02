using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    [TestCategory("Integration")]
    public class BrandsControllerTests
    {
        private AppDbContext context;
        private BrandsController controller;
        private IBrandRepository brandRepository;
        private IMapper mapper;

        public BrandsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql();
            context = new AppDbContext();

            brandRepository = new BrandManager(context);

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile(context));
            });
            mapper = config.CreateMapper();

            controller = new BrandsController(brandRepository);
        }

        private Brand brandInDb1 = new Brand();
        private Brand brandInDb2 = new Brand();
        private int numberOfBrandsInDb = 0;

        [TestInitialize]
        public void TestInitialize()
        {
            // Given : On insère des marques en base de données.
            brandInDb1 = new Brand()
            {
                NameBrand = "Aka"
            };

            brandInDb2 = new Brand()
            {
                NameBrand = "Sungsam"
            };

            context.Brands.Add(brandInDb1);
            context.Brands.Add(brandInDb2);

            context.SaveChanges();

            numberOfBrandsInDb = context.Brands.Count();
        }

        [TestMethod()]
        public void ShouldGetBrand()
        {
            // Given : Une marque en base de données.
            // Pas nécessaire, fait dans le TestInitialize.

            // When : J'appelle la méthode get de mon API pour récupérer la marque.
            ActionResult<BrandDetailsDTO> action = controller.GetBrandById(brandInDb1.IdBrand).Result;

            // Then : On récupère la marque et le code de retour est 200.
            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(ActionResult<BrandDetailsDTO>), "L'action doit être de type ActionResult<BrandDetailsDTO>");
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "Le result doit être de type OkObjectResult.");

            BrandDetailsDTO returnBrand = (action.Result as OkObjectResult)?.Value as BrandDetailsDTO;
            BrandDetailsDTO brandInDb = mapper.Map<BrandDetailsDTO>(((OkObjectResult)action.Result).Value);
            Assert.AreEqual(brandInDb, returnBrand);
        }

        [TestMethod()]
        public void GetBrandShouldReturnNotFound()
        {
            // When : J'appelle la méthode get de mon API pour récupérer la marque.
            ActionResult<BrandDetailsDTO> action = controller.GetBrandById(0).Result;

            // Then : On récupère la marque et le code de retour est 404.
            Assert.IsNull(action.Value, "L'action doit être nulle.");
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "Le result doit être de type NotFoundResult.");
        }

        [TestMethod()]
        public void ShouldGetAllBrands()
        {
            // Given : Deux marques en base de données.
            // Pas nécessaire, fait dans le TestInitialize.

            // When : J'appelle la méthode get de mon API pour récupérer la marque.
            ActionResult<IEnumerable<BrandDTO>> action = controller.GetBrands().Result;

            // Then : On récupère la marque et le code de retour est 200.
            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(ActionResult<IEnumerable<BrandDTO>>), "L'action doit être de type ActionResult<IEnumerable<BrandDTO>>.");
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "Le result doit être de type OkObjectResult.");

            List<BrandDTO> returnBrands = (action.Result as OkObjectResult)?.Value as List<BrandDTO>;
            Assert.AreEqual(numberOfBrandsInDb, returnBrands.Count(), $"La liste de marques dans la base de données doit être de {numberOfBrandsInDb}.");

            List<BrandDTO> brandsInDb = mapper.Map<IEnumerable<BrandDTO>>(((OkObjectResult)action.Result).Value).ToList();
            CollectionAssert.AreEqual(brandsInDb, returnBrands, "La liste de marques dans la base de données et la liste de marques retournée ne sont pas les mêmes.");
        }

        [TestMethod()]
        public void ShouldCreateBrand()
        {
            // Given : On créer une marque à ajouter en base de données.
            BrandDTO newBrandInDb = new BrandDTO()
            {
                NameBrand = "Youyou",
            };

            // When : J'appelle la méthode get de mon API pour récupérer la marque.
            ActionResult<Brand> action = controller.PostBrand(newBrandInDb).Result;

            // Then : On récupère la marque et le code de retour est 200.

            // On filtre la marque avec tous les champs car l'Id n'existe pas dans le DTO.
            Brand expectedBrand = context.Brands.FirstOrDefault(m => m.NameBrand == newBrandInDb.NameBrand);

            Assert.IsNotNull(expectedBrand, "La marque n'a pas été ajoutée en base de données.");
            Assert.IsInstanceOfType(action, typeof(ActionResult<Brand>), "Result n'est pas un action result.");
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult.");

            Brand returnBrand = (action.Result as CreatedAtActionResult)?.Value as Brand;
            Assert.AreEqual(expectedBrand, returnBrand, "Les marques ne sont pas identiques.");
        }

        // On devrait aussi tester le cas où la création de marque échoue (ex : nom manquant.)

        [TestMethod()]
        public void ShouldDeleteBrand()
        {
            // Given : Une marque en base de données.
            // Pas nécessaire, fait dans le TestInitialize.

            // When : J'appelle la méthode get de mon API pour récupérer le marque.
            IActionResult action = controller.DeleteBrand(brandInDb1.IdBrand).Result;

            // Then : On récupère la marque et le code de retour est 200.
            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result n'est pas un NoContentResult.");

            Brand brandToGet = context.Brands.Where(p => p.IdBrand == brandInDb1.IdBrand).FirstOrDefault();
            Assert.IsNull(brandToGet, "La marque n'a pas été supprimée.");
        }

        [TestMethod()]
        public void ShouldNotDeleteBrandBecauseBrandDoesNotExist()
        {
            // When : J'appelle la méthode get de mon API pour récupérer la marque.
            IActionResult action = controller.DeleteBrand(0).Result;

            // Then : On récupère la marque et le code de retour est 404.
            Assert.IsInstanceOfType(action, typeof(NotFoundResult), "Result n'est pas un NotFoundResult.");
        }

        [TestMethod()]
        public void ShouldUpdateBrand()
        {
            // Given : Une marque à mettre à jour.
            BrandDetailsDTO brandToUpdate = new BrandDetailsDTO()
            {
                IdBrand = brandInDb1.IdBrand,
                NameBrand = "Fourchette",
            };

            // When : J'appelle la méthode get de mon API pour récupérer la marque.
            IActionResult action = controller.PutBrand(brandToUpdate.IdBrand, brandToUpdate).Result;

            // Then : On récupère la marque et le code de retour est 200.
            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result n'est pas un NoContentResult.");

            Brand brandToGet = context.Brands.Where(p => p.IdBrand == brandToUpdate.IdBrand).FirstOrDefault();
            Brand brandInDb = mapper.Map<Brand>(brandToUpdate);

            brandToGet.RelatedProductsBrand = null;
            brandInDb.RelatedProductsBrand = null;

            Assert.AreEqual(brandToGet, brandInDb, "La marque a mal été modifiée.");
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
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(BadRequestResult));
        }

        [TestMethod]
        public void ShouldNotUpdateBrandBecauseBrandDoesNotExist()
        {
            // Given : Une marque à mettre à jour.
            BrandDetailsDTO updatedBrandDTO = mapper.Map<BrandDetailsDTO>(brandInDb2);
            updatedBrandDTO.IdBrand = 0;

            // When : On appelle la méthode Put de l'API pour mettre à jour un produit qui n'est pas enregistré.
            IActionResult action = controller.PutBrand(updatedBrandDTO.IdBrand, updatedBrandDTO).Result;

            // Then : On ne renvoie rien et le code HTTP est NotFound (404).
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NotFoundResult));
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Brands.RemoveRange(context.Brands);
            context.SaveChanges();
        }
    }
}