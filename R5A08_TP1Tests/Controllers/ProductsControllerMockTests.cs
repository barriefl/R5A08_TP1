using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using R5A08_TP1.Controllers;
using R5A08_TP1.Models.DataManager;
using R5A08_TP1.Models.DTO;
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
    public class ProductsControllerMockTests
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

        private AppDbContext context;

        private ProductsController controller;

        private IProductRepository productRepository;
        private IBrandRepository brandRepository;
        private IProductTypeRepository productTypeRepository;

        private Mock<IProductRepository> productRepositoryMock;
        private Mock<IBrandRepository> brandRepositoryMock;
        private Mock<IProductTypeRepository> productTypeRepositoryMock;

        private IMapper mapper;

        private Product productInDb1;
        private Product productInDb2;

        private Brand brandInDb1;
        private Brand brandInDb2;

        private ProductType productTypeInDb1;
        private ProductType productTypeInDb2;

        public ProductsControllerMockTests()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql();
            context = new AppDbContext();

            productRepository = new ProductManager(context);

            productRepositoryMock = new Mock<IProductRepository>();

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile(context));
            });
            mapper = config.CreateMapper();

            controller = new ProductsController(productRepository);

            brandInDb1 = new Brand()
            {
                IdBrand = 1,
                NameBrand = "Ikea"
            };
            brandInDb2 = new Brand()
            {
                IdBrand = 2,
                NameBrand = "Samsung"
            };

            productTypeInDb1 = new ProductType()
            {
                IdProductType = 1,
                NameProductType = "Meuble"
            };
            productTypeInDb2 = new ProductType()
            {
                IdProductType = 2,
                NameProductType = "Électronique"
            };

            productInDb1 = new Product()
            {
                IdProduct = 1,
                NameProduct = "Chaise",
                IdBrand = brandInDb1.IdBrand,
                IdProductType = productTypeInDb1.IdProductType,
                DescriptionProduct = "Une superbe chaise bleu",
                PhotoNameProduct = "Chaise",
                UriPhotoProduct = "https://ikea.fr/chaise.jpg",
                RealStock = 10,
                MinStock = 1,
                MaxStock = 100
            };
            productInDb2 = new Product()
            {
                IdProduct = 2,
                NameProduct = "Téléphone",
                IdBrand = brandInDb2.IdBrand,
                IdProductType = productTypeInDb2.IdProductType,
                DescriptionProduct = "Un très joli téléphone rouge",
                PhotoNameProduct = "Tel",
                UriPhotoProduct = "https://samsung.com/tel-rouge.jpg",
                RealStock = 5,
                MinStock = 3,
                MaxStock = 25
            };
        }
        
        [TestMethod()]
        public void ShouldGetProduct()
        {
            // Given : Un produit en base de données.
            // Pas nécessaire, fait dans le TestInitialize.
            int testId = productInDb1.IdProduct;
            productRepositoryMock.Setup(p => p.GetByIdAsync(productInDb1.IdProduct)).ReturnsAsync(productInDb1);

            // When : J'appelle la méthode get de mon API pour récupérer le produit.
            ActionResult<ProductDetailsDTO> action = controller.GetProductById(productInDb1.IdProduct).Result;

            // Then : On récupère le produit et le code de retour est 200.
            productRepositoryMock.Verify(p => p.GetByIdWithIncludesAsync(productInDb1.IdProduct), Times.Once);

            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(ActionResult<ProductDetailsDTO>), "L'action doit être de type ActionResult<ProductDetailsDTO>");
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "Le result doit être de type OkObjectResult.");

            ProductDetailsDTO returnProduct = (action.Result as OkObjectResult)?.Value as ProductDetailsDTO;
            ProductDetailsDTO productInDb = mapper.Map<ProductDetailsDTO>(((OkObjectResult)action.Result).Value);
            Assert.AreEqual(productInDb, returnProduct);
        }

        [TestMethod()]
        public void GetProductShouldReturnNotFound()
        {
            // Given : Aucun produit en base de données.
            productRepositoryMock.Setup(p => p.GetByIdAsync(0)).ReturnsAsync((Product)null);

            // When : J'appelle la méthode get de mon API pour récupérer le produit.
            ActionResult<ProductDetailsDTO> action = controller.GetProductById(0).Result;

            // Then : On récupère le produit et le code de retour est 404.
            productRepositoryMock.Verify(p => p.GetByIdAsync(0), Times.Never);

            Assert.IsNull(action.Value, "L'action doit être nulle.");
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "Le result doit être de type NotFoundResult.");
        }

        //[TestMethod()]
        //public void ShouldGetAllProducts()
        //{
        //    // Given : Deux produits en base de données.
        //    // Pas nécessaire, fait dans le TestInitialize.

        //    // When : J'appelle la méthode get de mon API pour récupérer le produit.
        //    ActionResult<IEnumerable<ProductDTO>> action = controller.GetProducts().Result;

        //    // Then : On récupère le produit et le code de retour est 200.
        //    Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
        //    Assert.IsInstanceOfType(action, typeof(ActionResult<IEnumerable<ProductDTO>>), "L'action doit être de type ActionResult<IEnumerable<ProductDTO>>.");
        //    Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "Le result doit être de type OkObjectResult.");

        //    List<ProductDTO> returnProducts = (action.Result as OkObjectResult)?.Value as List<ProductDTO>;
        //    Assert.AreEqual(numberOfProductsInDb, returnProducts.Count(), $"La liste de produits dans la base de données doit être de {numberOfProductsInDb}.");

        //    List<ProductDTO> productsInDb = mapper.Map<IEnumerable<ProductDTO>>(((OkObjectResult)action.Result).Value).ToList();
        //    CollectionAssert.AreEqual(productsInDb, returnProducts, "La liste de produits dans la base de données et la liste de produits retournée ne sont pas les mêmes.");
        //}

        //[TestMethod()]
        //public void ShouldCreateProduct()
        //{
        //    // Given : On créer un produit à ajouter en base de données.
        //    CreateProductDTO newProductInDb = new CreateProductDTO()
        //    {
        //        NameProduct = "Table",
        //        NameBrand = "Gifi",
        //        NameProductType = "Meuble extérieur",
        //        DescriptionProduct = "Une superbe table",
        //        PhotoNameProduct = "Table",
        //        UriPhotoProduct = "https://ikea.fr/table.jpg",
        //        RealStock = 15,
        //        MinStock = 5,
        //        MaxStock = 50
        //    };

        //    // When : J'appelle la méthode get de mon API pour récupérer le produit.
        //    ActionResult<Product> action = controller.PostProduct(newProductInDb).Result;

        //    // Then : On récupère le produit et le code de retour est 200.

        //    // On vérifie que la marque et le type de produit ont été créés.
        //    Brand createdBrand = context.Brands.FirstOrDefault(b => b.NameBrand == newProductInDb.NameBrand);
        //    Assert.IsNotNull(createdBrand, "La marque n'a pas été créée en base de données.");
        //    Assert.AreEqual(newProductInDb.NameBrand, createdBrand.NameBrand, "Le nom de la marque ne correspond pas.");

        //    ProductType createdProductType = context.ProductTypes.FirstOrDefault(pt => pt.NameProductType == newProductInDb.NameProductType);
        //    Assert.IsNotNull(createdProductType, "Le type de produit n'a pas été créé en base de données.");
        //    Assert.AreEqual(newProductInDb.NameProductType, createdProductType.NameProductType, "Le nom du type de produit ne correspond pas.");

        //    // On filtre le produit avec tous les champs car l'Id n'existe pas dans le DTO.
        //    Product expectedProduct = context.Products
        //        .Include(p => p.BrandNavigation)
        //        .Include(p => p.ProductTypeNavigation)
        //        .FirstOrDefault(p =>
        //            p.NameProduct == newProductInDb.NameProduct &&
        //            p.DescriptionProduct == newProductInDb.DescriptionProduct &&
        //            p.RealStock == newProductInDb.RealStock &&
        //            p.MinStock == newProductInDb.MinStock &&
        //            p.MaxStock == newProductInDb.MaxStock &&
        //            p.UriPhotoProduct == newProductInDb.UriPhotoProduct);

        //    Assert.IsNotNull(expectedProduct, "Le produit n'a pas été ajouté en base de données.");
        //    Assert.IsInstanceOfType(action, typeof(ActionResult<Product>), "Result n'est pas un action result.");
        //    Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult.");

        //    //Product returnProduct = mapper.Map<Product>(newProductInDb);
        //    Product returnProduct = (action.Result as CreatedAtActionResult)?.Value as Product;
        //    Assert.AreEqual(expectedProduct, returnProduct, "Les produits ne sont pas identiques.");
        //}

        //// On devrait aussi tester les cas où la création de produit échoue (ex : nom manquant, stock négatif, etc.)

        //[TestMethod()]
        //public void ShouldDeleteProduct()
        //{
        //    // Given : Un produit en base de données.
        //    // Pas nécessaire, fait dans le TestInitialize.

        //    // When : J'appelle la méthode get de mon API pour récupérer le produit.
        //    IActionResult action = controller.DeleteProduct(productInDb1.IdProduct).Result;

        //    // Then : On récupère le produit et le code de retour est 200.
        //    Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
        //    Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result n'est pas un NoContentResult.");

        //    Product productToGet = context.Products.Where(p => p.IdProduct == productInDb1.IdProduct).FirstOrDefault();
        //    Assert.IsNull(productToGet, "Le produit n'a pas été supprimé.");
        //}

        //[TestMethod()]
        //public void ShouldNotDeleteProductBecauseProductDoesNotExist()
        //{
        //    // When : J'appelle la méthode get de mon API pour récupérer le produit.
        //    IActionResult action = controller.DeleteProduct(0).Result;

        //    // Then : On récupère le produit et le code de retour est 404.
        //    Assert.IsInstanceOfType(action, typeof(NotFoundResult), "Result n'est pas un NotFoundResult.");
        //}

        //[TestMethod()]
        //public void ShouldUpdateProduct()
        //{
        //    // Given : Un produit à mettre à jour.
        //    UpdateProductDTO productToUpdate = new UpdateProductDTO()
        //    {
        //        IdProduct = productInDb1.IdProduct,
        //        NameProduct = "Fourchette",
        //        NameBrand = "Carrefour",
        //        NameProductType = "Couvert",
        //        DescriptionProduct = "Une vraiment très jolie fourchette.",
        //        PhotoNameProduct = "Fourchette",
        //        UriPhotoProduct = "https://carrefour.fr/fourchette.jpg",
        //        RealStock = 100,
        //        MinStock = 50,
        //        MaxStock = 500
        //    };

        //    // When : J'appelle la méthode get de mon API pour récupérer le produit.
        //    IActionResult action = controller.PutProduct(productToUpdate.IdProduct, productToUpdate).Result;

        //    // Then : On récupère le produit et le code de retour est 200.

        //    // On vérifie que la marque et le type de produit ont été créés.
        //    Brand createdBrand = context.Brands.FirstOrDefault(b => b.NameBrand == productToUpdate.NameBrand);
        //    Assert.IsNotNull(createdBrand, "La marque n'a pas été créée en base de données.");
        //    Assert.AreEqual(productToUpdate.NameBrand, createdBrand.NameBrand, "Le nom de la marque ne correspond pas.");

        //    ProductType createdProductType = context.ProductTypes.FirstOrDefault(pt => pt.NameProductType == productToUpdate.NameProductType);
        //    Assert.IsNotNull(createdProductType, "Le type de produit n'a pas été créé en base de données.");
        //    Assert.AreEqual(productToUpdate.NameProductType, createdProductType.NameProductType, "Le nom du type de produit ne correspond pas.");

        //    Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
        //    Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result n'est pas un NoContentResult.");

        //    Product productToGet = context.Products.Where(p => p.IdProduct == productToUpdate.IdProduct).FirstOrDefault();
        //    Product productInDb = mapper.Map<Product>(productToUpdate);

        //    Assert.AreEqual(productToGet, productInDb, "Le produit a mal été modifié.");
        //}

        //[TestMethod]
        //public void ShouldNotUpdateProductBecauseIdInUrlIsDifferent()
        //{
        //    // Given : Un produit à mettre à jour.
        //    UpdateProductDTO updatedProductDTO = mapper.Map<UpdateProductDTO>(productInDb1);

        //    // When : On appelle la méthode Put du controller pour mettre à jour le produit,
        //    // mais en précisant un ID différent de celui du produit enregistré
        //    IActionResult action = controller.PutProduct(0, updatedProductDTO).Result;

        //    // Then : On ne renvoie rien et le code HTTP est BadRequest (400).
        //    Assert.IsNotNull(action);
        //    Assert.IsInstanceOfType(action, typeof(BadRequestResult));
        //}

        //[TestMethod]
        //public void ShouldNotUpdateProductBecauseProductDoesNotExist()
        //{
        //    // Given : Un produit à mettre à jour.
        //    UpdateProductDTO updatedProductDTO = mapper.Map<UpdateProductDTO>(productInDb2);
        //    updatedProductDTO.IdProduct = 0;

        //    // When : On appelle la méthode Put de l'API pour mettre à jour un produit qui n'est pas enregistré.
        //    IActionResult action = controller.PutProduct(updatedProductDTO.IdProduct, updatedProductDTO).Result;

        //    // Then : On ne renvoie rien et le code HTTP est NotFound (404).
        //    Assert.IsNotNull(action);
        //    Assert.IsInstanceOfType(action, typeof(NotFoundResult));
        //}
    }
}