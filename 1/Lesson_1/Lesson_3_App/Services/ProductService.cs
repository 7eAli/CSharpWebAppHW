using AutoMapper;
using Lesson_3_App.Db;
using Lesson_3_App.Models;
using Lesson_3_App.Models.Dto;
using Lesson_3_App.Services.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace Lesson_3_App.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        public ProductService(AppDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }
        public int AddProduct(ProductDto product)
        {
            using (_context)
            {
                var entity = _mapper.Map<Product>(product);
                _context.Products.Add(entity);
                _context.SaveChanges();

                _cache.Remove("products");

                return entity.Id;
            }

            
        }
        
        public IEnumerable<ProductDto> GetProducts()
        {
            using(_context)
            {
                if (_cache.TryGetValue("products", out List<ProductDto> products))
                    return products;
                
                products = _context.Products.Select(x => _mapper.Map<ProductDto>(x)).ToList();
                _cache.Set("products", products);

                return products;
            }
        }
    }
}
