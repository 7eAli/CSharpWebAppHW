using Lesson_3_App.Models.Dto;
using Lesson_3_App.Services.Abstractions;

namespace Lesson_3_App.Query
{
    public class MySimpleQuery
    {
        public IEnumerable<ProductDto> GetProducts([Service] IProductService _service) => _service.GetProducts();

        public IEnumerable<StorageDto> GetStorages([Service] IStorageService _service) => _service.GetStorages();

        public IEnumerable<CategoryDto> GetCategories([Service] ICategoryService _service) => _service.GetCategories();        
    }
}
