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
    public class ProduitsController : Controller
    {
        private readonly IDataRepository<Product> dataRepository;

        public ProduitsController(IDataRepository<Product> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: Produits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduits()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: Produits/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduitById(int id)
        {
            var product = await dataRepository.GetByIdAsync(id);
            if (product.Value == null)
            {
                return NotFound();
            }
            return product;
        }

        // PUT: Produits/Put/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutProduit(int id, Product product)
        {
            if (id != product.IdProduit)
            {
                return BadRequest();
            }
            var productToUpdate = await dataRepository.GetByIdAsync(id);
            if (productToUpdate.Value == null)
            {
                return NotFound();
            }
            await dataRepository.UpdateAsync(productToUpdate.Value, product);
            return NoContent();
        }

        // POST: Produits/Post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> PostProduit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(product);
            return CreatedAtAction("GetById", new { id = product.IdProduit }, product);
        }

        // DELETE: Produits/Delete/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduit(int id)
        {
            var product = await dataRepository.GetByIdAsync(id);
            if (product.Value == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(product.Value);
            return NoContent();
        }
    }
}
