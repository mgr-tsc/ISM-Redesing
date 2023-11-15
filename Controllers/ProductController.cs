using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ISM_Redesign.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ISM_Redesing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly List<Product> _products;

        public ProductController()
        {
            _products = new List<Product>
                {
                    new Product { ProductID = 1, Name = "Product 1", Description = "Description 1" },
                    new Product { ProductID = 2, Name = "Product 2", Description = "Description 2" },
                    new Product { ProductID = 3, Name = "Product 3", Description = "Description 3" },
                };
        }

        // GET: api/Product
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get() => await Task.FromResult(_products.ToList());

        // GET: api/Product/{id}
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            return await Task.FromResult(product);
        }

        // POST: api/Product
        [Produces(MediaTypeNames.Application.Json)]
        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            _products.Add(product);

            return await Task.FromResult(CreatedAtAction(nameof(Get), new { id = product.ProductID }, product));
        }

        // PUT: api/Product/{id}
        [Produces(MediaTypeNames.Application.Json)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.ProductID == id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;

            return await Task.FromResult(NoContent());
        }

        // DELETE: api/Product/{id}
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            _products.Remove(product);

            return await Task.FromResult(NoContent());
        }
    }
}
