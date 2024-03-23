using Lesson_3_App.Models.Dto;

namespace Lesson_3_App.Services.Abstractions
{
    public interface IStorageService
    {
        IEnumerable<StorageDto> GetStorages();
        int AddStorage(StorageDto storage);
    }
}
