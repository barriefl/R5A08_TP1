using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepo, IMapper mapper)
        {
            productRepository = productRepo;
            _mapper = mapper;
        }

        // GET: Products
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            ActionResult<IEnumerable<Product>> products = await productRepository.GetAllAsync();
            return _mapper.Map<Product>(products);
        }

        // GET: Products/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product.Value == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet]
        [Route("[action]/{name}")]
        [ActionName("GetByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByName(string name)
        {
            return await productRepository.GetProductsByNameAsync(name);
        }

        // PUT: Products/Put/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.IdProduct)
            {
                return BadRequest();
            }

            var productToUpdate = await productRepository.GetByIdAsync(id);

            if (productToUpdate.Value == null)
            {
                return NotFound();
            }

            await productRepository.UpdateAsync(productToUpdate.Value, product);
            return NoContent();
        }

        // POST: Products/Post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await productRepository.AddAsync(product);
            return CreatedAtAction("GetById", new { id = product.IdProduct }, product);
        }

        // DELETE: Products/Delete/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product.Value == null)
            {
                return NotFound();
            }

            await productRepository.DeleteAsync(product.Value);
            return NoContent();
        }
    }
}
