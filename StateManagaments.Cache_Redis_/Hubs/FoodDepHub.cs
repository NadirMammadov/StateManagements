using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using StateManagaments.Cache_Redis_.Interfaces;
using StateManagaments.Cache_Redis_.Models;
using StateManagments.Models.Data;

namespace StateManagaments.Cache_Redis_.Hubs
{
    public class FoodDepHub : Hub
    {
        private readonly IFoodRepository _foodRepository;

        public FoodDepHub(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }
        public async Task FirstPageData()
        {
           await _foodRepository.GetAllAsync();
        }
    }
}
