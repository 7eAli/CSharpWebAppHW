using App.Abstractions;
using App.Models;
using App.Models.DTO;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace App.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductRepository(IMapper mapper, IMemoryCache cache) 
        { 
            _mapper = mapper; 
            _cache = cache;
        }

        public int AddGroup(CategoryDto group)
        {
            using (var context = new ProductContext())
            {
                var category = context.Category.FirstOrDefault(x => x.Name.ToLower() == group.Name.ToLower());
                if (category == null)
                {
                    category = _mapper.Map<Category>(group);
                    context.Category.Add(category);
                    context.SaveChanges();
                    _cache.Remove("categories");
                }                
                return category.Id;
            }            
        }

        public int AddProduct(ProductDto product)
        {
            using (var context = new ProductContext())
            {
                var productEntity = context.Products.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (productEntity == null)
                {
                    productEntity = _mapper.Map<Product>(product);
                    context.Products.Add(productEntity);
                    context.SaveChanges();
                    _cache.Remove("products");
                }                
                return productEntity.Id;
            }
        }

        public IEnumerable<CategoryDto> GetGroups()
        {
            if (_cache.TryGetValue("categories", out List<CategoryDto> categoryCache))
            {
                return categoryCache;
            }     
            
            using (var context = new ProductContext())
            {
                var categories = context.Category.Select(x => _mapper.Map<CategoryDto>(x)).ToList();
                _cache.Set("categories", categories, TimeSpan.FromMinutes(30));
                return categories;
            }
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<>))
            using (var context = new ProductContext())
            {
                var products = context.Products.Select(x => _mapper.Map<ProductDto>(x)).ToList();
                return products;
            }
        }
    }
}
