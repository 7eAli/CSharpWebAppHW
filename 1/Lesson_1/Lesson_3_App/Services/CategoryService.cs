using AutoMapper;
using Lesson_3_App.Db;
using Lesson_3_App.Models;
using Lesson_3_App.Models.Dto;
using Lesson_3_App.Services.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace Lesson_3_App.Services
{
    public class CategoryService :ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        public CategoryService(AppDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddCategory(CategoryDto category)
        {
            using (_context)
            {
                var entity = _mapper.Map<Category>(category);
                _context.Category.Add(entity);
                _context.SaveChanges();

                _cache.Remove("categories");

                return entity.Id;
            }
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            using (_context)
            {
                if (_cache.TryGetValue("categories", out List<CategoryDto> categories))
                    return categories;

                categories = _context.Category.Select(x => _mapper.Map<CategoryDto>(x)).ToList();
                _cache.Set("categories", categories);

                return categories;
            }
        }
    }
}
