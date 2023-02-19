using StateManagaments.Cache_Redis_.Models;
using StateManagements.Models.Models;

namespace StateManagaments.Cache_Redis_.Interfaces
{

    public interface IFoodRepository
    {
        Task<IEnumerable<FoodDto>> GetAllAsync();
        Task Create(FoodDto entity);
        Task Remove(FoodDto entity);
    }
}
