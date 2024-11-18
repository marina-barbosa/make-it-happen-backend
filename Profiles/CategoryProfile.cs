using AutoMapper;
using make_it_happen.DTOs;
using make_it_happen.Models;

namespace make_it_happen.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
    }
}
