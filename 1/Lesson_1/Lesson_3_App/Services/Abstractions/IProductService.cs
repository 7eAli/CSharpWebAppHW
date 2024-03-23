using Lesson_3_App.Models.Dto;

namespace Lesson_3_App.Services.Abstractions
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProducts();
        int AddProduct(ProductDto product);
    }
}
