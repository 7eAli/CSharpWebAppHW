using App.Abstractions;
using App.Models;
using App.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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

        [HttpPost(template: "post_storage")]
        public IActionResult PostStorage([FromBody] StorageDto storageEntity)
        {
            var storage = _productRepository.AddStorage(storageEntity);
            return Ok(storage);            
        }

        [HttpPatch(template: "change_price")]
        public IActionResult ChangePrice([FromBody] int productId, int price)
        {
            var change = _productRepository.ChangePrice(price, productId);
            return Ok(change);            
        }

        [HttpDelete(template: "delete_product")]
        public IActionResult DeleteProduct([FromBody] int productId)
        {
            var deletion = _productRepository.DeleteProduct(productId);
            return Ok(deletion);
        }
        [HttpDelete(template: "delete_category")]
        public IActionResult DeleteCategory([FromBody] int categoryId)
        {
            var deletion = _productRepository.DeleteGroup(categoryId);
            return Ok(deletion);            
        }

        [HttpDelete(template: "delete_storage")]
        public IActionResult DeleteStorage([FromBody] int storageId)
        {
            var deletion = _productRepository.DeleteStorage(storageId);
            return Ok(deletion);
        }

        [HttpGet(template:"get_product_csv")]
        public FileContentResult GetProductCsv()
        {
            var content = _productRepository.GetProductCsv();
            return File(new System.Text.UTF8Encoding().GetBytes(content), "text/csv", "report.csv");
        }

        [HttpGet(template:"get_stats_csv")]
        public ActionResult<string> GetStatsCsv()
        {
            var content = _productRepository.GetStatsCsv();

            string fileName = string.Empty;

            fileName = "stats" + DateTime.Now.ToBinary().ToString() + ".csv";

            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName), content);

            return "https://" + Request.Host.ToString() + "/static/" + fileName;
        }
    }
}