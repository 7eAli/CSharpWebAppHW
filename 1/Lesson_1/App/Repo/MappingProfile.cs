using App.Models;
using App.Models.DTO;
using AutoMapper;

namespace App.Repo
{
    public class MappingProfile : Profile
    {


        public MappingProfile() 
        {
            CreateMap<Product, ProductDto>(MemberList.Destination).ReverseMap();
            CreateMap<Category, CategoryDto>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, StorageDto>(MemberList.Destination).ReverseMap();
        }
    }
}
