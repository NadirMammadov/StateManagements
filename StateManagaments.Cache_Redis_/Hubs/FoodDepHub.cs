using Microsoft.AspNetCore.SignalR;
using StateManagments.Models.Data;

namespace StateManagaments.Cache_Redis_.Hubs
{
    public class FoodDepHub : Hub
    {
        private readonly StateManagmentContext _context;

        public FoodDepHub(StateManagmentContext context)
        {
            _context = context;
        }
        public async Task SendProducts()
        {
            var foods = _context.Foods.ToList();
            await Clients.All.SendAsync("ReceivedFoods", foods);
        }
    }
}
