using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using R5A08_TP1.Controllers;
using R5A08_TP1.Models.DataManager;
using R5A08_TP1.Models.DTO;
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
    [TestCategory("Mock")]
    public class ProduitsControllerMockTests
    {
        //private ProductsController controllerMock;
        //private Mock<IWriteDataRepository<Product>> dataRepositoryMock;

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    var builder = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql();
        //    controllerMock = new ProductsController(dataRepositoryMock.Object);
        //    dataRepositoryMock = new Mock<IWriteDataRepository<Product>>();
        //}

        //[TestMethod()]
        //public void ShouldGetProduct()
        //{
        //    // Given : Un produit en base de données.
        //    Produit produitInDb = new Produit()
        //    {
        //        NomProduit = "Chaise",
        //        DescriptionProduit = "Une superbe chaise",
        //        NomPhotoProduit = "Une superbe chaise bleu",
        //        UriPhotoProduit = "https://ikea.fr/chaise.jpg"
        //    };

        //    dataRepositoryMock.Setup(p => p.GetByIdAsync(produitInDb.IdProduit)).ReturnsAsync(produitInDb);
        //    context.Produits.Add(produitInDb);
        //    context.SaveChanges();

        //    // When : J'appelle la méthode get de mon API pour récupérer le produit.
        //    ActionResult<Produit> action = controller.GetProduitById(produitInDb.IdProduit).Result;

        //    // Then : On récupère le produit et le code de retour est 200.
        //    Assert.IsNotNull(action);
        //    Assert.IsInstanceOfType(action.Value, typeof(Produit));

        //    Produit returnProduct = action.Value;
        //    Assert.AreEqual(produitInDb.NomProduit, returnProduct.NomProduit);
        //    //Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult));
        //}

        //[TestMethod()]
        //public void ShouldGetAllProducts()
        //{
        //    // Given : Deux produits en base de données.
        //    Produit produitInDb1 = new Produit()
        //    {
        //        NomProduit = "Chaise",
        //        DescriptionProduit = "Une superbe chaise",
        //        NomPhotoProduit = "Une superbe chaise bleu",
        //        UriPhotoProduit = "https://ikea.fr/chaise.jpg"
        //    };
        //    Produit produitInDb2 = new Produit()
        //    {
        //        NomProduit = "Table",
        //        DescriptionProduit = "Une superbe table",
        //        NomPhotoProduit = "Une superbe table ronde",
        //        UriPhotoProduit = "https://ikea.fr/table.jpg"
        //    };

        //    context.Produits.Add(produitInDb1);
        //    context.Produits.Add(produitInDb2);
        //    context.SaveChanges();

        //    // When : J'appelle la méthode get de mon API pour récupérer le produit.
        //    ActionResult<IEnumerable<Produit>> action = controller.GetProduits().Result;

        //    // Then : On récupère le produit et le code de retour est 200.
        //    Assert.IsNotNull(action);
        //    Assert.IsInstanceOfType(action.Value, typeof(IEnumerable<Produit>));
        //    List<Produit> returnProducts = action.Value.ToList();
        //    Assert.AreEqual(2, returnProducts.Count);
        //    Assert.AreEqual(produitInDb1.NomProduit, returnProducts[0].NomProduit);
        //    Assert.AreEqual(produitInDb2.NomProduit, returnProducts[1].NomProduit);
        //}

        //[TestMethod()]
        //public void GetProductShouldReturnNotFound()
        //{
        //    // When : J'appelle la méthode get de mon API pour récupérer le produit.
        //    ActionResult<Produit> action = controller.GetProduitById(0).Result;

        //    // Then : On récupère le produit et le code de retour est 404.
        //    Assert.IsNull(action.Value);
        //    Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult));
        //}

        //[TestMethod()]
        //public void ShouldCreateProduct()
        //{
        //    // Given : Un produit en base de données.
        //    Produit produitInDb = new Produit()
        //    {
        //        NomProduit = "Chaise",
        //        DescriptionProduit = "Une superbe chaise",
        //        NomPhotoProduit = "Une superbe chaise bleu",
        //        UriPhotoProduit = "https://ikea.fr/chaise.jpg"
        //    };

        //    // When : J'appelle la méthode get de mon API pour récupérer le produit.
        //    ActionResult<Produit> action = controller.PostProduit(produitInDb).Result;

        //    // Then : On récupère le produit et le code de retour est 200.
        //    Produit productToGet = context.Produits.Where(p => p.IdProduit == produitInDb.IdProduit).FirstOrDefault();
        //    Assert.IsInstanceOfType(action, typeof(ActionResult<Produit>), "Result n'est pas un action result.");
        //    Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult.");
        //    Assert.AreEqual(produitInDb, productToGet, "Les produits ne sont pas identiques.");
        //}

        //[TestMethod()]
        //public void ShouldDeleteProduct() 
        //{ 
        //    // Given : Un produit en base de données.
        //    Produit produitInDb = new Produit()
        //    {
        //        NomProduit = "Chaise",
        //        DescriptionProduit = "Une superbe chaise",
        //        NomPhotoProduit = "Une superbe chaise bleu",
        //        UriPhotoProduit = "https://ikea.fr/chaise.jpg"
        //    };
        //    context.Produits.Add(produitInDb);
        //    context.SaveChanges();
        //    // When : J'appelle la méthode get de mon API pour récupérer le produit.
        //    IActionResult action = controller.DeleteProduit(produitInDb.IdProduit).Result;
        //    // Then : On récupère le produit et le code de retour est 200.
        //    Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result n'est pas un OkObjectResult.");
        //    Produit productToGet = context.Produits.Where(p => p.IdProduit == produitInDb.IdProduit).FirstOrDefault();
        //    Assert.IsNull(productToGet, "Le produit n'a pas été supprimé.");
        //}

        //[TestMethod()]
        //public void ShouldNotDeleteProductBecauseProductDoesNotExist()
        //{
        //    // When : J'appelle la méthode get de mon API pour récupérer le produit.
        //    IActionResult action = controller.DeleteProduit(0).Result;
        //    // Then : On récupère le produit et le code de retour est 404.
        //    Assert.IsInstanceOfType(action, typeof(NotFoundResult), "Result n'est pas un NotFoundResult.");
        //}   

        //[TestMethod()]
        //public void ShouldUpdateProduct()
        //{
        //    // Given : Un produit en base de données.
        //    Produit produitInDb = new Produit()
        //    {
        //        NomProduit = "Chaise",
        //        DescriptionProduit = "Une superbe chaise",
        //        NomPhotoProduit = "Une superbe chaise bleu",
        //        UriPhotoProduit = "https://ikea.fr/chaise.jpg"
        //    };
        //    context.Produits.Add(produitInDb);
        //    context.SaveChanges();
        //    Produit produitToUpdate = new Produit()
        //    {
        //        IdProduit = produitInDb.IdProduit,
        //        NomProduit = "Chaise Modifiée",
        //        DescriptionProduit = "Une superbe chaise modifiée",
        //        NomPhotoProduit = "Une superbe chaise bleu modifiée",
        //        UriPhotoProduit = "https://ikea.fr/chaise_modifiee.jpg"
        //    };
        //    // When : J'appelle la méthode get de mon API pour récupérer le produit.
        //    IActionResult action = controller.PutProduit(produitInDb.IdProduit, produitToUpdate).Result;
        //    // Then : On récupère le produit et le code de retour est 200.
        //    Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result n'est pas un NoContentResult.");
        //    Produit productToGet = context.Produits.Where(p => p.IdProduit == produitInDb.IdProduit).FirstOrDefault();
        //    Assert.AreEqual(produitToUpdate.NomProduit, productToGet.NomProduit, "Le nom du produit n'a pas été modifié.");
        //    Assert.AreEqual(produitToUpdate.DescriptionProduit, productToGet.DescriptionProduit, "La description du produit n'a pas été modifiée.");
        //    Assert.AreEqual(produitToUpdate.NomPhotoProduit, productToGet.NomPhotoProduit, "Le nom de la photo du produit n'a pas été modifié.");
        //    Assert.AreEqual(produitToUpdate.UriPhotoProduit, productToGet.UriPhotoProduit, "L'uri de la photo du produit n'a pas été modifiée.");
        //}

        //[TestMethod()]
        //public void ShouldNotUpdateProductBecauseProductDoesNotExist()
        //{
        //    // Given : Un produit en base de données.
        //    Produit produitToUpdate = new Produit()
        //    {
        //        IdProduit = 0,
        //        NomProduit = "Chaise Modifiée",
        //        DescriptionProduit = "Une superbe chaise modifiée",
        //        NomPhotoProduit = "Une superbe chaise bleu modifiée",
        //        UriPhotoProduit = "https://ikea.fr/chaise_modifiee.jpg"
        //    };
        //    // When : J'appelle la méthode get de mon API pour récupérer le produit.
        //    IActionResult action = controller.PutProduit(produitToUpdate.IdProduit, produitToUpdate).Result;
        //    // Then : On récupère le produit et le code de retour est 404.
        //    Assert.IsInstanceOfType(action, typeof(NotFoundResult), "Result n'est pas un NotFoundResult.");
        //}

        private Mock<IProductRepository> mockProductRepository;
        private Mock<IBrandRepository> mockBrandRepository;
        private Mock<IProductTypeRepository> mockProductTypeRepository;
        private ProductsController controller;

        private Product product1;
        private Product product2;
        private Brand brand1;
        private Brand brand2;
        private ProductType productType1;
        private ProductType productType2;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialisation des mocks
            mockProductRepository = new Mock<IProductRepository>();
            mockBrandRepository = new Mock<IBrandRepository>();
            mockProductTypeRepository = new Mock<IProductTypeRepository>();

            // Données de test
            brand1 = new Brand { IdBrand = 1, NameBrand = "Ikea" };
            brand2 = new Brand { IdBrand = 2, NameBrand = "Samsung" };

            productType1 = new ProductType { IdProductType = 1, NameProductType = "Meuble" };
            productType2 = new ProductType { IdProductType = 2, NameProductType = "Électronique" };

            product1 = new Product
            {
                IdProduct = 1,
                NameProduct = "Chaise",
                IdBrand = 1,
                IdProductType = 1,
                BrandNavigation = brand1,
                ProductTypeNavigation = productType1,
                DescriptionProduct = "Une superbe chaise bleu",
                PhotoNameProduct = "Chaise",
                UriPhotoProduct = "https://ikea.fr/chaise.jpg",
                RealStock = 10,
                MinStock = 1,
                MaxStock = 100
            };

            product2 = new Product
            {
                IdProduct = 2,
                NameProduct = "Téléphone",
                IdBrand = 2,
                IdProductType = 2,
                BrandNavigation = brand2,
                ProductTypeNavigation = productType2,
                DescriptionProduct = "Un très joli téléphone rouge",
                PhotoNameProduct = "Tel",
                UriPhotoProduct = "https://samsung.com/tel-rouge.jpg",
                RealStock = 5,
                MinStock = 3,
                MaxStock = 25
            };

            controller = new ProductsController(
                mockProductRepository.Object,
                mockBrandRepository.Object,
                mockProductTypeRepository.Object
            );
        }

        [TestMethod()]
        public void ShouldGetProduct()
        {
            // Given : Configuration du mock pour retourner un produit
            mockProductRepository
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(product1);

            // When : Appel de la méthode GetProductById
            ActionResult<ProductDetailsDTO> action = controller.GetProductById(1).Result;

            // Then : Vérifications
            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "Le result doit être de type OkObjectResult.");

            var okResult = action.Result as OkObjectResult;
            Assert.IsNotNull(okResult?.Value);

            mockProductRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }

        [TestMethod()]
        public void GetProductShouldReturnNotFound()
        {
            // Given : Configuration du mock pour retourner null
            mockProductRepository
                .Setup(repo => repo.GetByIdAsync(0))
                .ReturnsAsync((Product)null);

            // When : Appel de la méthode GetProductById avec un ID inexistant
            ActionResult<ProductDetailsDTO> action = controller.GetProductById(0).Result;

            // Then : Vérification du NotFound
            Assert.IsNull(action.Value, "L'action value doit être nulle.");
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "Le result doit être de type NotFoundResult.");

            mockProductRepository.Verify(repo => repo.GetByIdAsync(0), Times.Once);
        }

        [TestMethod()]
        public void ShouldGetAllProducts()
        {
            // Given : Configuration du mock pour retourner une liste de produits
            var products = new List<Product> { product1, product2 };
            mockProductRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(products);

            // When : Appel de la méthode GetProducts
            ActionResult<IEnumerable<ProductDTO>> action = controller.GetProducts().Result;

            // Then : Vérifications
            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "Le result doit être de type OkObjectResult.");

            var okResult = action.Result as OkObjectResult;
            var returnProducts = okResult?.Value as List<ProductDTO>;

            Assert.IsNotNull(returnProducts);
            Assert.AreEqual(2, returnProducts.Count, "La liste doit contenir 2 produits.");

            mockProductRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [TestMethod()]
        public void ShouldCreateProduct()
        {
            // Given : Configuration des mocks
            var newProductDTO = new CreateProductDTO
            {
                NameProduct = "Table",
                NameBrand = "Gifi",
                NameProductType = "Meuble extérieur",
                DescriptionProduct = "Une superbe table",
                PhotoNameProduct = "Table",
                UriPhotoProduct = "https://ikea.fr/table.jpg",
                RealStock = 15,
                MinStock = 5,
                MaxStock = 50
            };

            var newBrand = new Brand { IdBrand = 3, NameBrand = "Gifi" };
            var newProductType = new ProductType { IdProductType = 3, NameProductType = "Meuble extérieur" };

            mockBrandRepository
                .Setup(repo => repo.GetBrandByNameAsync("Gifi"))
                .ReturnsAsync((Brand)null);

            mockBrandRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Brand>()))
                .ReturnsAsync(newBrand);

            mockProductTypeRepository
                .Setup(repo => repo.GetProductTypeByNameAsync("Meuble extérieur"))
                .ReturnsAsync((ProductType)null);

            mockProductTypeRepository
                .Setup(repo => repo.AddAsync(It.IsAny<ProductType>()))
                .ReturnsAsync(newProductType);

            var createdProduct = new Product
            {
                IdProduct = 3,
                NameProduct = newProductDTO.NameProduct,
                IdBrand = newBrand.IdBrand,
                IdProductType = newProductType.IdProductType,
                DescriptionProduct = newProductDTO.DescriptionProduct,
                PhotoNameProduct = newProductDTO.PhotoNameProduct,
                UriPhotoProduct = newProductDTO.UriPhotoProduct,
                RealStock = newProductDTO.RealStock,
                MinStock = newProductDTO.MinStock,
                MaxStock = newProductDTO.MaxStock
            };

            mockProductRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync(createdProduct);

            // When : Appel de la méthode PostProduct
            ActionResult<Product> action = controller.PostProduct(newProductDTO).Result;

            // Then : Vérifications
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "Result doit être un CreatedAtActionResult.");

            var createdResult = action.Result as CreatedAtActionResult;
            var returnProduct = createdResult?.Value as Product;

            Assert.IsNotNull(returnProduct);
            Assert.AreEqual("Table", returnProduct.NameProduct);

            mockBrandRepository.Verify(repo => repo.GetBrandByNameAsync("Gifi"), Times.Once);
            mockBrandRepository.Verify(repo => repo.AddAsync(It.IsAny<Brand>()), Times.Once);
            mockProductTypeRepository.Verify(repo => repo.GetProductTypeByNameAsync("Meuble extérieur"), Times.Once);
            mockProductTypeRepository.Verify(repo => repo.AddAsync(It.IsAny<ProductType>()), Times.Once);
            mockProductRepository.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
        }

        [TestMethod()]
        public void ShouldDeleteProduct()
        {
            // Given : Configuration du mock
            mockProductRepository
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(product1);

            mockProductRepository
                .Setup(repo => repo.DeleteAsync(1))
                .ReturnsAsync(true);

            // When : Appel de la méthode DeleteProduct
            IActionResult action = controller.DeleteProduct(1).Result;

            // Then : Vérifications
            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result doit être un NoContentResult.");

            mockProductRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
            mockProductRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [TestMethod()]
        public void ShouldNotDeleteProductBecauseProductDoesNotExist()
        {
            // Given : Configuration du mock pour retourner null
            mockProductRepository
                .Setup(repo => repo.GetByIdAsync(0))
                .ReturnsAsync((Product)null);

            // When : Appel de la méthode DeleteProduct avec un ID inexistant
            IActionResult action = controller.DeleteProduct(0).Result;

            // Then : Vérification du NotFound
            Assert.IsInstanceOfType(action, typeof(NotFoundResult), "Result doit être un NotFoundResult.");

            mockProductRepository.Verify(repo => repo.GetByIdAsync(0), Times.Once);
            mockProductRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Never);
        }

        [TestMethod()]
        public void ShouldUpdateProduct()
        {
            // Given : Configuration des mocks
            var updateDTO = new UpdateProductDTO
            {
                IdProduct = 1,
                NameProduct = "Fourchette",
                NameBrand = "Carrefour",
                NameProductType = "Couvert",
                DescriptionProduct = "Une vraiment très jolie fourchette.",
                PhotoNameProduct = "Fourchette",
                UriPhotoProduct = "https://carrefour.fr/fourchette.jpg",
                RealStock = 100,
                MinStock = 50,
                MaxStock = 500
            };

            var newBrand = new Brand { IdBrand = 4, NameBrand = "Carrefour" };
            var newProductType = new ProductType { IdProductType = 4, NameProductType = "Couvert" };

            mockProductRepository
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(product1);

            mockBrandRepository
                .Setup(repo => repo.GetBrandByNameAsync("Carrefour"))
                .ReturnsAsync((Brand)null);

            mockBrandRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Brand>()))
                .ReturnsAsync(newBrand);

            mockProductTypeRepository
                .Setup(repo => repo.GetProductTypeByNameAsync("Couvert"))
                .ReturnsAsync((ProductType)null);

            mockProductTypeRepository
                .Setup(repo => repo.AddAsync(It.IsAny<ProductType>()))
                .ReturnsAsync(newProductType);

            mockProductRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                .ReturnsAsync(true);

            // When : Appel de la méthode PutProduct
            IActionResult action = controller.PutProduct(1, updateDTO).Result;

            // Then : Vérifications
            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result doit être un NoContentResult.");

            mockProductRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
            mockProductRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Once);
            mockBrandRepository.Verify(repo => repo.GetBrandByNameAsync("Carrefour"), Times.Once);
            mockProductTypeRepository.Verify(repo => repo.GetProductTypeByNameAsync("Couvert"), Times.Once);
        }

        [TestMethod]
        public void ShouldNotUpdateProductBecauseIdInUrlIsDifferent()
        {
            // Given : Un DTO avec un ID différent de celui dans l'URL
            var updateDTO = new UpdateProductDTO { IdProduct = 1 };

            // When : Appel avec un ID différent dans l'URL
            IActionResult action = controller.PutProduct(2, updateDTO).Result;

            // Then : Vérification du BadRequest
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(BadRequestResult));

            // Aucun appel au repository ne doit être effectué
            mockProductRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Never);
            mockProductRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void ShouldNotUpdateProductBecauseProductDoesNotExist()
        {
            // Given : Configuration du mock pour retourner null
            var updateDTO = new UpdateProductDTO { IdProduct = 999 };

            mockProductRepository
                .Setup(repo => repo.GetByIdAsync(999))
                .ReturnsAsync((Product)null);

            // When : Appel de la méthode PutProduct avec un produit inexistant
            IActionResult action = controller.PutProduct(999, updateDTO).Result;

            // Then : Vérification du NotFound
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NotFoundResult));

            mockProductRepository.Verify(repo => repo.GetByIdAsync(999), Times.Once);
            mockProductRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Never);
        }
    }
}