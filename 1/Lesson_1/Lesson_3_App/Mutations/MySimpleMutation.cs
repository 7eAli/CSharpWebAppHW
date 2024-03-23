using Lesson_3_App.Models.Dto;
using Lesson_3_App.Services.Abstractions;

namespace Lesson_3_App.Mutations
{
    public class MySimpleMutation
    {
        public int AddProduct([Service] IProductService _service, ProductDto product)
        {
            _service.AddProduct(product);
            return product.Id;
        }
        public int AddCategory([Service] ICategoryService _service, CategoryDto category)
        {
            _service.AddCategory(category);
            return category.Id;
        }

        public int AddStorage([Service]  IStorageService _service, StorageDto storage)
        {
            _service.AddStorage(storage);
            return storage.Id;
        }
    }
}
