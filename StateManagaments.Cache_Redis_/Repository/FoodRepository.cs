using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using StateManagaments.Cache_Redis_.Extensions;
using StateManagaments.Cache_Redis_.Hubs;
using StateManagaments.Cache_Redis_.Interfaces;
using StateManagaments.Cache_Redis_.Models;
using StateManagements.Models.Models;
using StateManagments.Models.Data;
using StateManagments.Models.Models;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Serialization;

namespace StateManagaments.Cache_Redis_.Repository
{
    public class FoodRepository : IFoodRepository 
    {
        private readonly StateManagmentContext _context;
        private IHubContext<FoodDepHub> _hubContext;
        private readonly IDistributedCache _cache;
        private const string CACHE_FOOD_KEY = "food";
        private readonly IMapper _mapper;

        public FoodRepository(StateManagmentContext context, IDistributedCache cache, IMapper mapper, IHubContext<FoodDepHub> hubContext)
        {
            _context = context;
            _cache = cache;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public async Task<IEnumerable<FoodDto>> GetAllAsync()
        {
            var foods = await _cache.GetRecordAsync<IEnumerable<FoodDto>>(CACHE_FOOD_KEY);
            if (foods == null)
            {
                var category = await _context.Categories
                            .Include(x => x.Foods)
                            .FirstOrDefaultAsync(x => x.Id == 2);
                foods = _mapper.Map<IEnumerable<FoodDto>>(category.Foods);
                await _cache.SetRecordAsync(recordKey: CACHE_FOOD_KEY, data: foods, unusedExpirationTime: TimeSpan.FromMinutes(30));
                var serialezer = new XmlSerializer(typeof(List<FoodDto>));
                var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data.xml");
                FileStream file = System.IO.File.Create(path);
                serialezer.Serialize(file, foods);
                file.Close();
            }
            await _hubContext.Clients.All.SendAsync("GetFoods", foods);
            return foods;


        }

        public async Task Create(FoodDto entity)
        {
            var data = _mapper.Map<Food>(entity);
            await _context.Foods.AddAsync(data);
            await _context.SaveChangesAsync();
            await _cache.RemoveAsync(CACHE_FOOD_KEY);
            await GetAllAsync();
        }

        public async Task Remove(FoodDto entity)
        {
            var data = _mapper.Map<Food>(entity);
             _context.Foods.Remove(data);
            await _context.SaveChangesAsync();
            await GetAllAsync();
        }
    }
}
