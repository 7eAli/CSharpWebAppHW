using App.Models;
using App.Models.DTO;

namespace App.Abstractions
{
    public interface IProductRepository
    {
        public int AddGroup(CategoryDto group);

        public IEnumerable<CategoryDto> GetGroups();

        public int AddProduct(ProductDto product);

        public IEnumerable<ProductDto> GetProducts();
    }
}
