using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.DataManager;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Mapper;
using R5A08_TP1.Models.Repository;
using R5A08_TP1.Models.DTO;

namespace R5A08_TP1.Controllers.Tests
{
    [TestClass()]
    [TestCategory("Integration")]
    public class ProductsControllerTests
    {
        private AppDbContext context;
        private ProductsController controller;
        private IProductRepository productRepository;
        private IBrandRepository brandRepository;
        private IProductTypeRepository productTypeRepository;
        private IMapper mapper;

        public ProductsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql();
            context = new AppDbContext();

            productRepository = new ProductManager(context);
            brandRepository = new BrandManager(context);
            productTypeRepository = new ProductTypeManager(context);

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
            mapper = config.CreateMapper();

            controller = new ProductsController(productRepository, mapper, brandRepository, productTypeRepository);
        }

        private Product productInDb1 = new Product();
        private Product productInDb2 = new Product();
        private int numberOfProductsInDb = 0;
        private int numberOfBrandsInDb = 0;
        private int numberOfProductTypesInDb = 0;

        [TestInitialize]
        public void TestInitialize()
        {
            // Given : On insère des produits en base de données.
            Brand brandInDb1 = new Brand()
            {
                NameBrand = "Ikea"
            };

            Brand brandInDb2 = new Brand()
            {
                NameBrand = "Samsung"
            };

            ProductType productTypeInDb1 = new ProductType()
            {
                NameProductType = "Meuble"
            };

            ProductType productTypeInDb2 = new ProductType()
            {
                NameProductType = "Électronique"
            };

            context.Brands.Add(brandInDb1);
            context.Brands.Add(brandInDb2);

            context.ProductTypes.Add(productTypeInDb1);
            context.ProductTypes.Add(productTypeInDb2);

            context.SaveChanges();

            productInDb1 = new Product()
            {
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

            context.Products.Add(productInDb1);
            context.Products.Add(productInDb2);

            context.SaveChanges();

            numberOfProductsInDb = context.Products.Count();
            numberOfBrandsInDb = context.Brands.Count();
            numberOfProductTypesInDb = context.ProductTypes.Count();
        }

        [TestMethod()]
        public void ShouldGetProduct()
        {
            // Given : Un produit en base de données.
            // Pas nécessaire, fait dans le TestInitialize.

            // When : J'appelle la méthode get de mon API pour récupérer le produit.
            ActionResult<ProductDetailsDTO> action = controller.GetProductById(productInDb1.IdProduct).Result;

            // Then : On récupère le produit et le code de retour est 200.
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
            // When : J'appelle la méthode get de mon API pour récupérer le produit.
            ActionResult<ProductDetailsDTO> action = controller.GetProductById(0).Result;

            // Then : On récupère le produit et le code de retour est 404.
            Assert.IsNull(action.Value, "L'action doit être nulle.");
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "Le result doit être de type NotFoundResult.");
        }

        [TestMethod()]
        public void ShouldGetAllProducts()
        {
            // Given : Deux produits en base de données.
            // Pas nécessaire, fait dans le TestInitialize.

            // When : J'appelle la méthode get de mon API pour récupérer le produit.
            ActionResult<IEnumerable<ProductDTO>> action = controller.GetProducts().Result;

            // Then : On récupère le produit et le code de retour est 200.
            Assert.IsNotNull(action, "L'action ne doit pas être nulle.");
            Assert.IsInstanceOfType(action, typeof(ActionResult<IEnumerable<ProductDTO>>), "L'action doit être de type ActionResult<IEnumerable<ProductDTO>>.");
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult), "Le result doit être de type OkObjectResult.");

            List<ProductDTO> returnProducts = (action.Result as OkObjectResult)?.Value as List<ProductDTO>;
            Assert.AreEqual(numberOfProductsInDb, returnProducts.Count(), $"La liste de produits dans la base de données doit être de {numberOfProductsInDb}.");

            List<ProductDTO> productsInDb = mapper.Map<IEnumerable<ProductDTO>>(((OkObjectResult)action.Result).Value).ToList();
            CollectionAssert.AreEqual(productsInDb, returnProducts, "La liste de produits dans la base de données et la liste de produits retournée ne sont pas les mêmes.");
        }

        [TestMethod()]
        public void ShouldCreateProduct()
        {
            // Given : On créer un produit à ajouter en base de données.
            CreateProductDTO newProductInDb = new CreateProductDTO()
            {
                NameProduct = "Table",
                NameBrand = "Ikea",
                NameProductType = "Meuble",
                DescriptionProduct = "Une superbe table",
                PhotoNameProduct = "Table",
                UriPhotoProduct = "https://ikea.fr/table.jpg",
                RealStock = 15,
                MinStock = 5,
                MaxStock = 50
            };

            // When : J'appelle la méthode get de mon API pour récupérer le produit.
            ActionResult<Product> action = controller.PostProduct(newProductInDb).Result;

            // Then : On récupère le produit et le code de retour est 200.
            // On filtre le produit avec tous les champs car l'Id n'existe pas dans le DTO.
            Product productToGet = context.Products.Include(p => p.BrandNavigation)
                .Include(p => p.ProductTypeNavigation)
                .FirstOrDefault(p =>
                    p.NameProduct == newProductInDb.NameProduct &&
                    p.DescriptionProduct == newProductInDb.DescriptionProduct &&
                    p.RealStock == newProductInDb.RealStock &&
                    p.MinStock == newProductInDb.MinStock &&
                    p.MaxStock == newProductInDb.MaxStock &&
                    p.UriPhotoProduct == newProductInDb.UriPhotoProduct);
            
            Assert.IsNotNull(productToGet, "Le produit n'a pas été ajouté en base de données.");
            Assert.IsInstanceOfType(action, typeof(ActionResult<Product>), "Result n'est pas un action result.");
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult.");

            Product produitInDb = mapper.Map<Product>(newProductInDb);
            // Erreur ici.
            Assert.AreEqual(produitInDb, productToGet, "Les produits ne sont pas identiques.");
        }

        //[TestMethod()]
        //public void ShouldDeleteProduct() 
        //{ 
        //    // Given : Un produit en base de données.
        //    Product produitInDb = new Product()
        //    {
        //        NomProduit = "Chaise",
        //        DescriptionProduit = "Une superbe chaise",
        //        NomPhotoProduit = "Une superbe chaise bleu",
        //        UriPhotoProduit = "https://ikea.fr/chaise.jpg"
        //    };
        //    context.Products.Add(produitInDb);
        //    context.SaveChanges();
        //    // When : J'appelle la méthode get de mon API pour récupérer le produit.
        //    IActionResult action = controller.DeleteProduit(produitInDb.IdProduit).Result;
        //    // Then : On récupère le produit et le code de retour est 200.
        //    Assert.IsInstanceOfType(action, typeof(NoContentResult), "Result n'est pas un OkObjectResult.");
        //    Product productToGet = context.Products.Where(p => p.IdProduit == produitInDb.IdProduit).FirstOrDefault();
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
        //    // Given : Un produit en base de données.
        //    Product produitInDb = new Product()
        //    {
        //        NomProduit = "Chaise",
        //        DescriptionProduit = "Une superbe chaise",
        //        NomPhotoProduit = "Une superbe chaise bleu",
        //        UriPhotoProduit = "https://ikea.fr/chaise.jpg"
        //    };
        //    context.Products.Add(produitInDb);
        //    context.SaveChanges();
        //    Product produitToUpdate = new Product()
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
        //    Product productToGet = context.Products.Where(p => p.IdProduit == produitInDb.IdProduit).FirstOrDefault();
        //    Assert.AreEqual(produitToUpdate.NomProduit, productToGet.NomProduit, "Le nom du produit n'a pas été modifié.");
        //    Assert.AreEqual(produitToUpdate.DescriptionProduit, productToGet.DescriptionProduit, "La description du produit n'a pas été modifiée.");
        //    Assert.AreEqual(produitToUpdate.NomPhotoProduit, productToGet.NomPhotoProduit, "Le nom de la photo du produit n'a pas été modifié.");
        //    Assert.AreEqual(produitToUpdate.UriPhotoProduit, productToGet.UriPhotoProduit, "L'uri de la photo du produit n'a pas été modifiée.");
        //}

        //[TestMethod()]
        //public void ShouldNotUpdateProductBecauseProductDoesNotExist()
        //{
        //    // Given : Un produit en base de données.
        //    Product produitToUpdate = new Product()
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

        [TestCleanup]
        public void Cleanup()
        {
            context.Products.RemoveRange(context.Products);
            context.Brands.RemoveRange(context.Brands);
            context.ProductTypes.RemoveRange(context.ProductTypes);
            context.SaveChanges();
        }
    }
}