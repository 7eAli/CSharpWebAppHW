using App.Abstractions;
using App.Models;
using App.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet(template: "get_products")]
        public IActionResult GetProducs()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpPost(template: "post_product")]
        public IActionResult PostProducts([FromBody] ProductDto productEntity)
        {
            var product = _productRepository.AddProduct(productEntity);
            return Ok(product);
        }

        [HttpGet(template: "get_categories")]
        public IActionResult GetCategories()
        {
            var categories = _productRepository.GetGroups();
            return Ok(categories);
        }


        [HttpPost(template: "post_category")]
        public IActionResult PostCategory([FromBody] CategoryDto categoryEntity)
        {
            var category = _productRepository.AddGroup(categoryEntity);
            return Ok(category);            
        }

        [HttpPost(template: "postStorage")]
        public IActionResult PostStorage([FromQuery] string name, string description, int productId, int amount)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (!context.Storages.Any(x => x.Name.ToLower().Equals(name.ToLower())))
                    {
                        context.Add(new Storage()
                        {
                            Name = name,
                            Description = description,
                            ProductId = productId,
                            Amount = amount
                        });
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(409);
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch(template: "changePrice")]
        public IActionResult ChangePrice([FromQuery] int productId, int price)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (context.Products.Any(x => x.Id == productId))
                    {
                        var product = context.Products.Find(productId);
                        if (product != null)
                        {
                            product.Cost = price;
                        }
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(409);
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(template: "deleteProduct")]
        public IActionResult DeleteProduct([FromQuery] int productId)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (context.Products.Any(x => x.Id == productId))
                    {
                        var product = context.Products.Find(productId);
                        context.Products.Remove(product);
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(409);
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpDelete(template: "deleteCategory")]
        public IActionResult DeleteCategory([FromQuery] int categoryId)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (context.Category.Any(x => x.Id == categoryId))
                    {
                        var category = context.Category.Find(categoryId);
                        context.Category.Remove(category);
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(409);
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}