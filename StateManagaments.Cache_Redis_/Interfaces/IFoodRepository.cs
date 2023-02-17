using StateManagements.Models.Models;

namespace StateManagaments.Cache_Redis_.Interfaces
{
    public interface IFoodRepository
    {
        Task<Category> GetAll();
    }
}
