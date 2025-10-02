using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.DTO.Brands;
using R5A08_TP1.Models.DTO.ProductTypes;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Mapper;
using R5A08_TP1.Models.Repository;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;

namespace R5A08_TP1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypesController : ControllerBase
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructeur pour le contrôleur ProductTypesController.
        /// </summary>
        /// <param name="productTypeRepository">Le DataRepository utilisé pour accéder aux types de produit.</param>
        public ProductTypesController(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;

            _context = new AppDbContext();
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile(_context));
            });
            _mapper = config.CreateMapper();
        }

        /// <summary>
        /// Récupère tous les types de produit.
        /// </summary>
        /// <returns>Une liste de types de produit sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des types de produit a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: ProductTypes
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductTypeDTO>>> GetProductTypes()
        {
            ActionResult<IEnumerable<ProductType>> productTypesResult = await _productTypeRepository.GetAllAsync();
            IEnumerable<ProductType> productTypes = productTypesResult.Value;

            IEnumerable<ProductTypeDTO> productTypesDto = _mapper.Map<IEnumerable<ProductTypeDTO>>(productTypes);
            return Ok(productTypesDto);
        }

        /// <summary>
        /// Récupère un type de produit avec son id.
        /// </summary>
        /// <param name="id">L'id du type de produit.</param>
        /// <returns>Un type de produit sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le type de produit a été récupéré avec succès.</response>
        /// <response code="404">Le type de produit demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: ProductTypes/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductTypeDetailsDTO>> GetProductTypeById(int id)
        {
            ActionResult<ProductType> productTypesResult = await _productTypeRepository.GetByIdAsync(id);
            ProductType productType = productTypesResult.Value;

            if (productType == null)
            {
                return NotFound();
            }

            ProductTypeDetailsDTO productTypeDto = _mapper.Map<ProductTypeDetailsDTO>(productType);
            return Ok(productTypeDto);
        }

        /// <summary>
        /// Récupère un type de produit avec son nom.
        /// </summary>
        /// <param name="name">Le nom du type de produit.</param>
        /// <returns>Un type de produit sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le type de produit a été récupéré avec succès.</response>
        /// <response code="404">Le type de produit demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: ProductTypes/GetByName/5
        [HttpGet]
        [Route("[action]/{name}")]
        [ActionName("GetByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductTypeDetailsDTO>> GetProductTypeByName(string name)
        {
            ActionResult<ProductType> productTypesResult = await _productTypeRepository.GetProductTypeByNameAsync(name);
            ProductType productType = productTypesResult.Value;

            if (productType == null)
            {
                return NotFound();
            }

            ProductTypeDetailsDTO productTypeDto = _mapper.Map<ProductTypeDetailsDTO>(productType);
            return Ok(productTypeDto);
        }

        /// <summary>
        /// Modifie un type de produit.
        /// </summary>
        /// <param name="id">L'id du type de produit.</param>
        /// <param name="productTypeDto">L'objet type de produit.</param>
        /// <returns>Une réponse HTTP 204 NoContent.</returns>
        /// <response code="204">Le type de produit a été modifié avec succès.</response>
        /// <response code="400">L'id donné ne correspond pas à l'id du type de produit.</response>
        /// <response code="404">Le type de produit n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: ProductTypes/Put/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<IActionResult> PutProductType(int id, ProductTypeDetailsDTO productTypeDto)
        {
            if (id != productTypeDto.IdProductType)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ActionResult<ProductType> productTypeToUpdate = await _productTypeRepository.GetByIdAsync(id);
            if (productTypeToUpdate.Value == null)
            {
                return NotFound();
            }

            ProductType productType = _mapper.Map(productTypeDto, productTypeToUpdate.Value);

            await _productTypeRepository.UpdateAsync(productTypeToUpdate.Value, productType);
            return NoContent();
        }

        /// <summary>
        /// Créer un type de produit.
        /// </summary>
        /// <param name="productTypeDto">L'objet type de produit.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">Le type de produit a été créé avec succès.</response>
        /// <response code="400">Le format du type de produit est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: ProductTypes/Post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductType>> PostProductType(ProductTypeDTO productTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProductType productType = _mapper.Map<ProductType>(productTypeDto);

            await _productTypeRepository.AddAsync(productType);
            return CreatedAtAction("GetById", new { id = productType.IdProductType }, productType);
        }

        /// <summary>
        /// Supprime un type de produit.
        /// </summary>
        /// <param name="id">L'id du type de produit.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">Le type de produit a été supprimé avec succès.</response>
        /// <response code="404">Le type de produit n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: ProductTypes/Delete/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProductType(int id)
        {
            ActionResult<ProductType> productType = await _productTypeRepository.GetByIdAsync(id);

            if (productType.Value == null)
            {
                return NotFound();
            }

            await _productTypeRepository.DeleteAsync(productType.Value);
            return NoContent();
        }
    }
}
