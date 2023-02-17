using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using StateManagaments.Cache_Redis_.Models;
using StateManagments.Models.Data;

namespace StateManagaments.Cache_Redis_.Hubs
{
    public class FoodDepHub : Hub
    {
        //private readonly IDistributedCache _cache;
        public async Task RunXmlRead()
        {
           
           //var foods =  await distributedCache.GetRecordAsync<IEnumerable<FoodDto>>(CACHE_FOOD_KEY);
        }
    }
}
