using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ISM_Redesign.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ISM_Redesing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class ProductController : ControllerBase
    {
        private readonly List<Product> _products;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        public ProductController()
        {
            _products = new List<Product>
                {
                    new Product { ProductID = 1, Name = "Product 1", Description = "Description 1", Area = "Area Fria", QuantityInStock = 10, UnitMeasure = ProductUnitMeasure.Caja, StockUniqueID = "001"},
                    new Product { ProductID = 2, Name = "Product 2", Description = "Description 2", Area = "Area Fria", QuantityInStock = 20, UnitMeasure = ProductUnitMeasure.Caja, StockUniqueID = "002"},
                    new Product { ProductID = 3, Name = "Product 3", Description = "Description 3", Area = "Area Fria", QuantityInStock = 30, UnitMeasure = ProductUnitMeasure.Caja, StockUniqueID = "003"}
                };
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get() => await Task.FromResult(_products.ToList());

        /// <summary>
        /// Gets a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID.</returns>
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

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The newly created product.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            _products.Add(product);

            return await Task.FromResult(CreatedAtAction(nameof(Get), new { id = product.ProductID }, product));
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product.</param>
        /// <returns>A response indicating success or failure.</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>A response indicating success or failure.</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
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
