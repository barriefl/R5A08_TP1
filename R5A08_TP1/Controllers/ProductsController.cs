using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.DTO;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Mapper;
using R5A08_TP1.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace R5A08_TP1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper _mapper;
        private AppDbContext _context;

        /// <summary>
        /// Constructeur pour le contrôleur ProductsController.
        /// </summary>
        /// <param name="productRepo">Le DataRepository utilisé pour accéder aux produits.</param>
        public ProductsController(IProductRepository productRepo)
        {
            productRepository = productRepo;

            _context = new AppDbContext();
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile(_context));
            });
            _mapper = config.CreateMapper();
        }

        /// <summary>
        /// Récupère tous les produits.
        /// </summary>
        /// <returns>Une liste de produits sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des produits a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: Products
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            ActionResult<IEnumerable<Product>> productsResult = await productRepository.GetAllWithIncludesAsync();
            IEnumerable<Product> products = productsResult.Value;

            IEnumerable<ProductDTO> productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productsDto);
        }

        /// <summary>
        /// Récupère un produit avec son id.
        /// </summary>
        /// <param name="id">L'id du produit.</param>
        /// <returns>Un produit sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le produit a été récupéré avec succès.</response>
        /// <response code="404">Le produit demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: Products/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDetailsDTO>> GetProductById(int id)
        {
            ActionResult<Product> productResult = await productRepository.GetByIdWithIncludesAsync(id);
            Product product = productResult.Value;

            if (product == null)
            {
                return NotFound();
            }

            ProductDetailsDTO productDetailsDto = _mapper.Map<ProductDetailsDTO>(product);
            return Ok(productDetailsDto);
        }

        /// <summary>
        /// Récupère une liste de produits avec son nom.
        /// </summary>
        /// <param name="name">Le nom du produit.</param>
        /// <returns>Un produit sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Les produits ont été récupéré avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: Products/GetByName/5
        [HttpGet]
        [Route("[action]/{name}")]
        [ActionName("GetByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByName(string name)
        {
            ActionResult<IEnumerable<Product>> productsResult = await productRepository.GetProductsByNameAsync(name);
            IEnumerable<Product> products = productsResult.Value;

            IEnumerable<ProductDTO> productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productsDto);
        }

        /// <summary>
        /// Modifie un produit.
        /// </summary>
        /// <param name="id">L'id du produit.</param>
        /// <param name="productDto">L'objet produit.</param>
        /// <returns>Une réponse HTTP 204 NoContent.</returns>
        /// <response code="204">Le vintie a été modifié avec succès.</response>
        /// <response code="400">L'id donné ne correspond pas à l'id du produit.</response>
        /// <response code="404">Le produit n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: Products/Put/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutProduct(int id, UpdateProductDTO productDto)
        {
            if (id != productDto.IdProduct)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ActionResult<Product> productToUpdate = await productRepository.GetByIdAsync(id);
            if (productToUpdate.Value == null)
            {
                return NotFound();
            }

            Product product = _mapper.Map(productDto, productToUpdate.Value);

            await productRepository.UpdateAsync(productToUpdate.Value, product);
            return NoContent();
        }

        /// <summary>
        /// Créer un produit.
        /// </summary>
        /// <param name="productDto">L'objet produit.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">Le produit a été créé avec succès.</response>
        /// <response code="400">Le format du produit est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: Products/Post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Product>> PostProduct(CreateProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = _mapper.Map<Product>(productDto);

            await productRepository.AddAsync(product);
            return CreatedAtAction("GetById", new { id = product.IdProduct }, product);
        }

        /// <summary>
        /// Supprime un produit.
        /// </summary>
        /// <param name="id">L'id du produit.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">Le produit a été supprimé avec succès.</response>
        /// <response code="404">Le produit n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: Products/Delete/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            ActionResult<Product> product = await productRepository.GetByIdAsync(id);

            if (product.Value == null)
            {
                return NotFound();
            }

            await productRepository.DeleteAsync(product.Value);
            return NoContent();
        }
    }
}
