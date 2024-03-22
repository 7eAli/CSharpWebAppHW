using App.Models;
using App.Models.DTO;

namespace App.Abstractions
{
    public interface IProductRepository
    {
        public int AddGroup(CategoryDto group);

        public IEnumerable<CategoryDto> GetGroups();
        public int DeleteGroup(int id);

        public int AddProduct(ProductDto product);

        public IEnumerable<ProductDto> GetProducts();

        public int ChangePrice(int id, int price);
        public int DeleteProduct(int id);

        public int AddStorage(StorageDto storage);

        public IEnumerable<StorageDto> GetStorages();

        public int DeleteStorage(int id);

        public string GetProductCsv();
        public string GetStatsCsv();
    }
}
