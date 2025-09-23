using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.DTO;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace R5A08_TP1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IBrandRepository brandRepository;
        private readonly IProductTypeRepository productTypeRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepo, IMapper mapper, IBrandRepository brandRepo, IProductTypeRepository productTypeRepo)
        {
            productRepository = productRepo;
            _mapper = mapper;
            brandRepository = brandRepo;
            productTypeRepository = productTypeRepo;
        }

        // GET: Products
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            ActionResult<IEnumerable<Product>> products = await productRepository.GetAllAsync();

            IEnumerable<ProductDTO> productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productsDto);
        }

        // GET: Products/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDetailsDTO>> GetProductById(int id)
        {
            ActionResult<Product> product = await productRepository.GetByIdAsync(id);

            if (product.Value == null)
            {
                return NotFound();
            }

            ProductDetailsDTO productDetailsDto = _mapper.Map<ProductDetailsDTO>(product);
            return Ok(product);
        }

        [HttpGet]
        [Route("[action]/{name}")]
        [ActionName("GetByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByName(string name)
        {
            ActionResult<IEnumerable<Product>> products = await productRepository.GetProductsByNameAsync(name);

            IEnumerable<ProductDTO> productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productsDto);
        }

        // PUT: Products/Put/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutProduct(int id, UpdateProductDTO productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }

            ActionResult<Product> productToUpdate = await productRepository.GetByIdAsync(id);
            if (productToUpdate.Value == null)
            {
                return NotFound();
            }

            ActionResult<Brand> brandResult = await brandRepository.GetBrandByNameAsync(productDto.BrandName);
            Brand brand = brandResult.Value;
            if (brand == null)
            {
                brand = new Brand { NameBrand = productDto.BrandName };
                await brandRepository.AddAsync(brand);
            }

            ActionResult<ProductType> productTypeResult = await productTypeRepository.GetProductTypeByNameAsync(productDto.ProductTypeName);
            ProductType productType = productTypeResult.Value;
            if (productType == null)
            {
                productType = new ProductType { NameProductType = productDto.ProductTypeName };
                await productTypeRepository.AddAsync(productType);
            }

            Product product = _mapper.Map(productDto, productToUpdate.Value);

            product.IdBrand = brand.IdBrand;
            product.IdProductType = productType.IdProductType;

            await productRepository.UpdateAsync(productToUpdate.Value, product);
            return NoContent();
        }

        // POST: Products/Post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> PostProduct(CreateProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ActionResult<Brand> brandResult = await brandRepository.GetBrandByNameAsync(productDto.BrandName);
            Brand brand = brandResult.Value;
            if (brand == null)
            {
                brand = new Brand { NameBrand = productDto.BrandName };
                await brandRepository.AddAsync(brand);
            }

            ActionResult<ProductType> productTypeResult = await productTypeRepository.GetProductTypeByNameAsync(productDto.ProductTypeName);
            ProductType productType = productTypeResult.Value;
            if (productType == null)
            {
                productType = new ProductType { NameProductType = productDto.ProductTypeName };
                await productTypeRepository.AddAsync(productType);
            }

            Product product = _mapper.Map<Product>(productDto);

            product.IdBrand = brand.IdBrand;
            product.IdProductType = productType.IdProductType;

            await productRepository.AddAsync(product);
            return CreatedAtAction("GetById", new { id = product.IdProduct }, product);
        }

        // DELETE: Products/Delete/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
