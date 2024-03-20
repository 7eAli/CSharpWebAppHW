using App.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}