using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using StateManagaments.Cache_Redis_.Extensions;
using StateManagaments.Cache_Redis_.Models;
using StateManagments.Models.Data;


namespace StateManagaments.Cache_Redis_.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<HomeController> _logger;
        private readonly StateManagmentContext _context;
        private const string CACHE_FOOD_KEY = "food";
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, StateManagmentContext context, IDistributedCache cache, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var foods = await _cache.GetRecordAsync<IEnumerable<FoodDto>>(CACHE_FOOD_KEY);
            if(foods ==null)
            {
                var category = await _context.Categories
                            .Include(x => x.Foods)
                            .FirstOrDefaultAsync(x => x.Id == 2);
                foods = _mapper.Map<IEnumerable<FoodDto>>(category.Foods);
                await _cache.SetRecordAsync(recordKey: CACHE_FOOD_KEY, data: foods, unusedExpirationTime: TimeSpan.FromSeconds(30));
            }
            return View(foods);
        }

      
    }
}