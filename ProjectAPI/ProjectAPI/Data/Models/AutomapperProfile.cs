using AutoMapper;
using ProjectAPI.Data.Models.View;

namespace ProjectAPI.Data.Models;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Customer, CustomerViewModel>().ReverseMap();
        CreateMap<Product, ProductViewModel>().ReverseMap();
    }
}