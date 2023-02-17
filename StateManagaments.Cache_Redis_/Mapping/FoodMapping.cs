using AutoMapper;
using StateManagaments.Cache_Redis_.Models;
using StateManagments.Models.Models;

namespace StateManagaments.Cache_Redis_.Mapping
{
    public class FoodMapping : Profile
    {
        public FoodMapping()
        {
            CreateMap<Food, FoodDto>().ReverseMap();
        }
    }
}
