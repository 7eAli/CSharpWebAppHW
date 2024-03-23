using Lesson_3_App.Models.Dto;

namespace Lesson_3_App.Services.Abstractions
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetCategories();
        int AddCategory(CategoryDto category);
    }
}
