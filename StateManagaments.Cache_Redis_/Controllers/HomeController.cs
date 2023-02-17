using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using StateManagaments.Cache_Redis_.Extensions;
using StateManagaments.Cache_Redis_.Models;
using StateManagments.Models.Data;
using System.Reflection;
using System.Xml.Serialization;

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
                var serialezer = new XmlSerializer(typeof(List<FoodDto>));
                var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"data.xml");
                FileStream file = System.IO.File.Create(path);
                serialezer.Serialize(file, foods);
                file.Close();
            }
           
            return View(foods);
        }

      
    }
}