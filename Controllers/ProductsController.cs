using Microsoft.AspNetCore.Mvc;
using BasicApi.Models;

namespace BasicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // In-memory storage for demo purposes
        // In a real application, you would use a database
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Description = "Gaming laptop", Price = 999.99m, StockQuantity = 10 },
            new Product { Id = 2, Name = "Mouse", Description = "Wireless mouse", Price = 29.99m, StockQuantity = 50 },
            new Product { Id = 3, Name = "Keyboard", Description = "Mechanical keyboard", Price = 79.99m, StockQuantity = 25 }
        };
        private static int _nextId = 4;

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of all products</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return Ok(_products);
        }

        /// <summary>
        /// Get a specific product by ID
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product details</returns>
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(product);
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="product">Product to create</param>
        /// <returns>Created product</returns>
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is required.");
            }

            if (string.IsNullOrWhiteSpace(product.Name))
            {
                return BadRequest("Product name is required.");
            }

            if (product.Price < 0)
            {
                return BadRequest("Product price cannot be negative.");
            }

            product.Id = _nextId++;
            product.CreatedDate = DateTime.UtcNow;
            product.UpdatedDate = null;

            _products.Add(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        /// <summary>
        /// Update an existing product
        /// </summary>
        /// <param name="id">Product ID to update</param>
        /// <param name="updatedProduct">Updated product data</param>
        /// <returns>Updated product</returns>
        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            if (updatedProduct == null)
            {
                return BadRequest("Product data is required.");
            }

            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            if (string.IsNullOrWhiteSpace(updatedProduct.Name))
            {
                return BadRequest("Product name is required.");
            }

            if (updatedProduct.Price < 0)
            {
                return BadRequest("Product price cannot be negative.");
            }

            // Update properties
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.StockQuantity = updatedProduct.StockQuantity;
            existingProduct.UpdatedDate = DateTime.UtcNow;

            return Ok(existingProduct);
        }

        /// <summary>
        /// Partially update a product
        /// </summary>
        /// <param name="id">Product ID to update</param>
        /// <param name="updates">Partial product data</param>
        /// <returns>Updated product</returns>
        [HttpPatch("{id}")]
        public ActionResult<Product> PatchProduct(int id, [FromBody] dynamic updates)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            // This is a simple implementation. In a real application, 
            // you might want to use JSON Patch or a more sophisticated approach
            var updatesDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(updates.ToString());
            
            foreach (var update in updatesDict)
            {
                switch (update.Key.ToLower())
                {
                    case "name":
                        existingProduct.Name = update.Value?.ToString() ?? string.Empty;
                        break;
                    case "description":
                        existingProduct.Description = update.Value?.ToString() ?? string.Empty;
                        break;
                    case "price":
                        if (decimal.TryParse(update.Value?.ToString(), out decimal price))
                            existingProduct.Price = price;
                        break;
                    case "stockquantity":
                        if (int.TryParse(update.Value?.ToString(), out int stock))
                            existingProduct.StockQuantity = stock;
                        break;
                }
            }

            existingProduct.UpdatedDate = DateTime.UtcNow;
            return Ok(existingProduct);
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">Product ID to delete</param>
        /// <returns>Confirmation message</returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            _products.Remove(product);
            return Ok(new { message = $"Product with ID {id} has been deleted successfully." });
        }

        /// <summary>
        /// Search products by name
        /// </summary>
        /// <param name="name">Product name to search for</param>
        /// <returns>List of matching products</returns>
        [HttpGet("search")]
        public ActionResult<IEnumerable<Product>> SearchProducts([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Search term is required.");
            }

            var matchingProducts = _products
                .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(matchingProducts);
        }
    }
}
