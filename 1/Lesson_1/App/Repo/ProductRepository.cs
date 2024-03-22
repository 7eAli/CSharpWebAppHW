using App.Abstractions;
using App.Models;
using App.Models.DTO;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using static System.Reflection.Metadata.BlobBuilder;
using System.Text;

namespace App.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly ProductContext _context;

        public ProductRepository(IMapper mapper, IMemoryCache cache, ProductContext context)
        {
            _mapper = mapper;
            _cache = cache;
            _context = context;
        }

        public int AddGroup(CategoryDto group)
        {
            using (_context)
            {
                var category = _context.Category.FirstOrDefault(x => x.Name.ToLower() == group.Name.ToLower());
                if (category == null)
                {
                    category = _mapper.Map<Category>(group);
                    _context.Category.Add(category);
                    _context.SaveChanges();
                    _cache.Remove("categories");
                }
                return category.Id;
            }
        }

        public int AddProduct(ProductDto product)
        {
            using (_context)
            {
                var productEntity = _context.Products.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (productEntity == null)
                {
                    productEntity = _mapper.Map<Product>(product);
                    _context.Products.Add(productEntity);
                    _context.SaveChanges();
                    _cache.Remove("products");
                }
                return productEntity.Id;
            }
        }

        public int AddStorage(StorageDto storage)
        {
            using (_context)
            {
                var storageEntity = _context.Storages.FirstOrDefault(x => x.Name.ToLower() == storage.Name.ToLower());
                if (storageEntity == null)
                {
                    storageEntity = _mapper.Map<Storage>(storage);
                    _context.Storages.Add(storageEntity);
                    _context.SaveChanges();
                    _cache.Remove("storages");
                }
                return storage.Id;
            }
        }


        public int ChangePrice(int id, int price)
        {
            using (_cache)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    product.Cost = price;
                    _context.SaveChanges();
                    return product.Id;                    
                }                
                return 0;
            }
        }

        public int DeleteGroup(int id)
        {
            using (_cache)
            {
                var category = _context.Category.FirstOrDefault(c => c.Id == id);
                if (category != null)
                {
                    _context.Category.Remove(category);
                    _context.SaveChanges();
                    return category.Id;                    
                }                
                return 0;
            }
        }

        public int DeleteProduct(int id)
        {
            using (_cache)
            {
                var product = _context.Products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                    return product.Id;                    
                }                
                return 0;
            }
        }

        public int DeleteStorage(int id)
        {
            using (_cache)
            {
                var storage = _context.Storages.FirstOrDefault(c => c.Id == id);
                if (storage != null)
                {
                    _context.Storages.Remove(storage);
                    _context.SaveChanges();
                    return storage.Id;
                }
                return 0;
            }
        }

        public IEnumerable<CategoryDto> GetGroups()
        {
            if (_cache.TryGetValue("categories", out List<CategoryDto> categoryCache))
            {
                return categoryCache;
            }

            using (_context)
            {
                var categories = _context.Category.Select(x => _mapper.Map<CategoryDto>(x)).ToList();
                _cache.Set("categories", categories, TimeSpan.FromMinutes(30));
                return categories;
            }
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<ProductDto> productCache))
            {
                return productCache;
            }

            using (_context)
            {
                var products = _context.Products.Select(x => _mapper.Map<ProductDto>(x)).ToList();
                _cache.Set("products", products, TimeSpan.FromMinutes(30));
                return products;
            }
        }

        public IEnumerable<StorageDto> GetStorages()
        {
            if (_cache.TryGetValue("storages", out List<StorageDto> storageCache))
            {
                return storageCache;
            }
            using (_context)
            {
                var storages = _context.Storages.Select(x => _mapper.Map<StorageDto>(x)).ToList();
                _cache.Set("storages", storages, TimeSpan.FromMinutes(30));
                return storages;
            }
        }

        private string GetProductCsvString(IEnumerable<ProductDto> products)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var p in products)
            {
                sb.AppendLine(p.Id + ";" + p.Name + ";" + p.Description + ";" + p.Cost + "\n");
            }
            return sb.ToString();
        }

        public string GetProductCsv()
        {
            var content = "";
            using (_context)
            {
                var products = GetProducts();
                content = GetProductCsvString(products);
                return content;
            }            
        }

        public string GetStatsCsv()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CurrentEntryCount" + ";" + _cache.GetCurrentStatistics().CurrentEntryCount.ToString() + "\n");
            sb.AppendLine("CurrentEstimatedSize" + ";" + _cache.GetCurrentStatistics().CurrentEstimatedSize.ToString() + "\n");
            sb.AppendLine("TotalMisses" + ";" + _cache.GetCurrentStatistics().TotalMisses.ToString() + "\n");
            sb.AppendLine("TotalHits" + ";" + _cache.GetCurrentStatistics().TotalHits.ToString() + "\n");
            return sb.ToString();
        }
    }
}
