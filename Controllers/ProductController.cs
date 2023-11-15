
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ISM_Redesign.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Mime;

namespace ISM_Redesing.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
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
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get() => _products.ToList();

        // GET: api/Product/5
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/Product
        [HttpPost]
        public ActionResult<Product> Post(Product product)
        {
            _products.Add(product);

            return CreatedAtAction(nameof(Get), new { id = product.ProductID}, product);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.ProductID== id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;

            return NoContent();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            _products.Remove(product);

            return NoContent();
        }
    }
}
