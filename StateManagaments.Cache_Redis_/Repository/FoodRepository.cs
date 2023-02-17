using Microsoft.EntityFrameworkCore;
using StateManagaments.Cache_Redis_.Interfaces;
using StateManagements.Models.Models;
using StateManagments.Models.Data;
using System.Linq.Expressions;

namespace StateManagaments.Cache_Redis_.Repository
{
    public class FoodRepository : IFoodRepository 
    {
        private readonly StateManagmentContext _context;

        public FoodRepository(StateManagmentContext context)
        {
            _context = context;
        }

        public async Task<Category> GetAll()
        {
            return await _context.Categories.Include(x => x.Foods).FirstOrDefaultAsync(x => x.Id == 2);
                
            
        }
    }
}
