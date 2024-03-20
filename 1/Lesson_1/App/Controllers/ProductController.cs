using App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet(template: "getProducts")]
        public IActionResult GetProducs()
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var products = context.Products.Select(x => new Product()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    });
                    return Ok(products);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost(template:"postProduct")]
        public IActionResult PostProducts([FromQuery] string name, string description, int price, int groupId)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (!context.Products.Any(x => x.Name.ToLower().Equals(name.ToLower())))
                    {
                        context.Add(new Product()
                        {
                            Name = name,
                            Description = description,
                            Cost = price,
                            CategoryId = groupId
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

        [HttpPost(template:"postCategory")]
        public IActionResult PostCategory([FromQuery] string name, string description)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (!context.Category.Any(x => x.Name.ToLower().Equals(name.ToLower())))
                    {
                        context.Add(new Category()
                        {
                            Name = name,
                            Description = description,                            
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

        [HttpPatch(template:"changePrice")]
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

        [HttpDelete(template:"deleteProduct")]
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