using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.DTO;
using R5A08_TP1.Models.DTO.Brands;
using R5A08_TP1.Models.DTO.Products;
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
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructeur pour le contrôleur BrandsController.
        /// </summary>
        /// <param name="brandRepository">Le DataRepository utilisé pour accéder aux marques.</param>
        public BrandsController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;

            _context = new AppDbContext();
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile(_context));
            });
            _mapper = config.CreateMapper();
        }

        /// <summary>
        /// Récupère toutes les marques.
        /// </summary>
        /// <returns>Une liste de marques sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste de marques a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: Brands
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetBrands()
        {
            ActionResult<IEnumerable<Brand>> brandsResult = await _brandRepository.GetAllAsync();
            IEnumerable<Brand> brands = brandsResult.Value;

            IEnumerable<BrandDTO> brandsDto = _mapper.Map<IEnumerable<BrandDTO>>(brands);
            return Ok(brandsDto);
        }

        /// <summary>
        /// Récupère une marque avec son id.
        /// </summary>
        /// <param name="id">L'id de la marque.</param>
        /// <returns>Une marque sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La marque a été récupérée avec succès.</response>
        /// <response code="404">La marque demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: Brands/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BrandDetailsDTO>> GetBrandById(int id)
        {
            ActionResult<Brand> brandResult = await _brandRepository.GetByIdAsync(id);
            Brand brand = brandResult.Value;

            if (brand == null)
            {
                return NotFound();
            }

            BrandDetailsDTO brandDto = _mapper.Map<BrandDetailsDTO>(brand);
            return Ok(brandDto);
        }

        /// <summary>
        /// Récupère une marque avec son nom.
        /// </summary>
        /// <param name="name">Le nom de la marque.</param>
        /// <returns>Une marque sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La marque a été récupéré avec succès.</response>
        /// <response code="404">La marque demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: Brands/GetByName/5
        [HttpGet]
        [Route("[action]/{name}")]
        [ActionName("GetByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BrandDetailsDTO>> GetBrandByName(string name)
        {
            ActionResult<Brand> brandResult = await _brandRepository.GetBrandByNameAsync(name);
            Brand brand = brandResult.Value;

            if (brand == null)
            {
                return NotFound();
            }

            BrandDetailsDTO brandDto = _mapper.Map<BrandDetailsDTO>(brand);
            return Ok(brandDto);
        }

        /// <summary>
        /// Modifie une marque.
        /// </summary>
        /// <param name="id">L'id de la marque.</param>
        /// <param name="brandDto">L'objet marque.</param>
        /// <returns>Une réponse HTTP 204 NoContent.</returns>
        /// <response code="204">La marque a été modifiée avec succès.</response>
        /// <response code="400">L'id donné ne correspond pas à l'id de la marque.</response>
        /// <response code="404">La marque n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: Brands/Put/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<IActionResult> PutBrand(int id, BrandDetailsDTO brandDto)
        {
            if (id != brandDto.IdBrand)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ActionResult<Brand> brandToUpdate = await _brandRepository.GetByIdAsync(id);
            if (brandToUpdate.Value == null)
            {
                return NotFound();
            }

            Brand brand = _mapper.Map(brandDto, brandToUpdate.Value);

            await _brandRepository.UpdateAsync(brandToUpdate.Value, brand);
            return NoContent();
        }

        /// <summary>
        /// Créer une marque.
        /// </summary>
        /// <param name="brandDto">L'objet marque.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">La marque a été créée avec succès.</response>
        /// <response code="400">Le format de la marque est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: Brands/Post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<ActionResult<Brand>> PostBrand(BrandDTO brandDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Brand brand = _mapper.Map<Brand>(brandDto);

            await _brandRepository.AddAsync(brand);
            return CreatedAtAction("GetById", new { id = brand.IdBrand }, brand);
        }

        /// <summary>
        /// Supprime une marque.
        /// </summary>
        /// <param name="id">L'id de la marque.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">La marque a été supprimée avec succès.</response>
        /// <response code="404">La marque n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: Brands/Delete/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            ActionResult<Brand> brand = await _brandRepository.GetByIdAsync(id);

            if (brand.Value == null)
            {
                return NotFound();
            }

            await _brandRepository.DeleteAsync(brand.Value);
            return NoContent();
        }
    }
}
