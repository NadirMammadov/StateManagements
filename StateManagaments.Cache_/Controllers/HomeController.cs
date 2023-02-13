using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using StateManagaments.Cache_.Models;
using StateManagments.Models.Data;
using StateManagments.Models.Models;
using System.Diagnostics;

namespace StateManagaments.Cache_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StateManagmentContext _context;
        private readonly IMemoryCache _memoryCache;
        private const string CACHE_DATE = "_date";
        private const string CACHE_FOODS = "_foods";
        public HomeController(ILogger<HomeController> logger, StateManagmentContext context, IMemoryCache memoryCache)
        {
            _logger = logger;
            var foods = new List<Food>
{
new Food{ Name = "Pizza Margherita" , Price ="AZN 11.50", Src="https://imageproxy.wolt.com/menu/menu-images/88983792-b6b4-11e9-b57c-0a5864661858_MARGARI_TA.jpeg" },
new Food{ Name = "Pizza Mamma Margherita" , Price ="AZN 12.50", Src="https://imageproxy.wolt.com/menu/menu-images/86e0b13a-b8d6-11e9-bea3-0a58646554a3_IMG_7733.jpeg" },
new Food{ Name = "Pizza Green Margherita" , Price ="AZN 13.50", Src="https://imageproxy.wolt.com/menu/menu-images/e0101a12-b6b4-11e9-b130-0a5864667e15_GREEN-MARGARI_TA.jpeg" },
new Food{ Name = "Pizza Spinach Ricotta" , Price ="AZN 12.00", Src="https://imageproxy.wolt.com/menu/menu-images/0e1f5cec-b6b5-11e9-8523-0a586465fe97_ISPANAKLI-Ricotta.jpeg" },
new Food{ Name = "Pizza Ricotta" , Price ="AZN 14.50", Src="https://imageproxy.wolt.com/menu/menu-images/1ed07b74-b6b6-11e9-9943-0a586465d88e_RI_COTTA.jpeg" },
new Food{ Name = "Five Cheese Pizza" , Price ="AZN 14.00", Src="https://imageproxy.wolt.com/menu/menu-images/efad5066-b8da-11e9-9b72-0a5864652f1f_DO_RT-PEYNI_RLI_.jpeg" },
new Food{ Name = "Zucchini Pizza" , Price ="AZN 13.50", Src="https://imageproxy.wolt.com/menu/menu-images/c126e430-b6b6-11e9-b0c8-0a5864661858_KABAKLI.jpeg" },
new Food{ Name = "Eggplant Pizza" , Price ="AZN 13.50", Src="https://imageproxy.wolt.com/menu/menu-images/3e067ce0-b6b7-11e9-abdb-0a586466198e_PATLICAN.jpeg" },
new Food{ Name = "Artichoke Pizza" , Price ="AZN 17.00", Src="https://imageproxy.wolt.com/menu/menu-images/b4599110-b6c7-11e9-aa9f-0a5864668320_ENGI_NARLI.jpeg" },
new Food{ Name = "Mushroom Pizza" , Price ="AZN 12.00", Src="https://imageproxy.wolt.com/menu/menu-images/e7beced6-b6b7-11e9-8e96-0a58646354d4_MANTARLI.jpeg" },
new Food{ Name = "Vegan Pizza 🥬" , Price ="AZN 13.50", Src="https://imageproxy.wolt.com/menu/menu-images/88d1cb24-b6b9-11e9-b022-0a586465545e_VEGAN.jpeg" },
new Food{ Name = "Cheese Filled Sausage Pizza", Price ="AZN 14.50", Src="https://imageproxy.wolt.com/menu/menu-images/caf737d8-b6c7-11e9-ad14-0a586465d88a_CHEDDAR-DOLGULU-SOSI_S.jpeg" },
new Food{ Name = "Soujouk Pizza" , Price ="AZN 21.00", Src="https://imageproxy.wolt.com/menu/menu-images/1dd3d404-b6bc-11e9-aaa7-0a5864667609_sucuklu.jpeg" },
new Food{ Name = "Chicken Caesar Pizza" , Price ="AZN 15.20", Src="https://imageproxy.wolt.com/menu/menu-images/637fe59c-b6bc-11e9-8ded-0a5864601048_Tavuk_Sezar.jpeg" },
new Food{ Name = "Mixed Pizza" , Price ="AZN 22.50", Src="https://imageproxy.wolt.com/menu/menu-images/a263684c-b6bc-11e9-91e1-0a58646559ca_Kar_s__k.jpeg" },
new Food{ Name = "Smoked Rib Steak Pizza" , Price ="AZN 19.50", Src="https://imageproxy.wolt.com/menu/menu-images/4675c6d4-b8d7-11e9-96c8-0a58646554a3_IMG_7735.jpeg" },
new Food{ Name = "Pepperoni Pizza" , Price ="AZN 18.00", Src="https://imageproxy.wolt.com/menu/menu-images/5b162d98-b6bd-11e9-b080-0a586465fe97_pepperoni.jpeg" },
new Food{ Name = "Roasted Beef Pizza" , Price ="AZN 22.50", Src="https://imageproxy.wolt.com/menu/menu-images/2aaad4ba-b6bd-11e9-9849-0a586465d88a_Kavurmal_.jpeg" },
new Food{ Name = "Salami Pizza" , Price ="AZN 16.00", Src="https://imageproxy.wolt.com/menu/menu-images/be5decfe-b8d6-11e9-a8f1-0a586466cc0e_pizza_kolbasa.jpeg" },
new Food{ Name = "Mini Salami Pizza" , Price ="AZN 14.00", Src="https://imageproxy.wolt.com/menu/menu-images/d5b07b42-b8d6-11e9-9830-0a586466cc0e_mini_kolbasa.jpeg" },
new Food{ Name = "Teriyaki Chicken Pizza" , Price ="AZN 15.20", Src="https://imageproxy.wolt.com/menu/menu-images/010ad73a-b6c3-11e9-8cc3-0a5864655ca7_Teriyaki_Tavuk.jpeg" },
new Food{ Name = "Smoked Turkey Pizza" , Price ="AZN 18.50", Src="https://imageproxy.wolt.com/menu/menu-images/0e8c7eb6-b8d7-11e9-a6a4-0a58646554a3_IMG_7731.jpeg" },
new Food{ Name = "Chicken BBQ Pizza" , Price ="AZN 15.20", Src="https://imageproxy.wolt.com/menu/menu-images/2c217e56-b6c3-11e9-a8bb-0a586465d88a_BBQ_Tavuk.jpeg" },
new Food{ Name = "Tuna Pizza" , Price ="AZN 19.00", Src="https://imageproxy.wolt.com/menu/menu-images/545d12b8-b6c3-11e9-906c-0a586465545e_Ton-Bal_kl_.jpeg" },
new Food{ Name = "Smoked Salmon Pizza" , Price ="AZN 25.00", Src="https://imageproxy.wolt.com/menu/menu-images/c72bc316-b6c3-11e9-a98a-0a586465545e_Somon_Fu_me.jpeg" },
new Food{ Name = "Shrimp Pizza" , Price ="AZN 19.50", Src="https://imageproxy.wolt.com/menu/menu-images/167a2750-b6c4-11e9-a2c9-0a5864667e11_Karidesli.jpeg" },
new Food{ Name = "Calzone Margherita" , Price ="AZN 12.50", Src="https://imageproxy.wolt.com/menu/menu-images/863cfada-b8d7-11e9-99b6-0a586466cc0e_Calzone.jpeg" },
new Food{ Name = "Calzone Salami" , Price ="AZN 14.50", Src="https://imageproxy.wolt.com/menu/menu-images/91fc08c0-b8d7-11e9-b63d-0a58646618df_Calzone.jpeg" },
new Food{ Name = "Sausage Calzone" , Price ="AZN 14.50", Src="https://imageproxy.wolt.com/menu/menu-images/9ddcaff0-b8d7-11e9-9e60-0a586466cc0e_Calzone.jpeg" },
new Food{ Name = "Sausage Calzone" , Price ="AZN 14.50", Src="https://imageproxy.wolt.com/menu/menu-images/9ddcaff0-b8d7-11e9-9e60-0a586466cc0e_Calzone.jpeg" }
};
            _context = context;

            if (!_context.Foods.Any())
            {
                _context.Categories.Add(new()
                {
                    Name = "Food",
                    Description = "Foods",
                    Foods = foods
                });
                _context.SaveChanges();
            }
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index()
        {
            if (!_memoryCache.TryGetValue(CACHE_FOODS, out List<Food> _foods))
            {
                _foods = await _context.Foods.AsQueryable().Include(x => x.Category).ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));
                _memoryCache.Set(CACHE_FOODS, _foods, cacheEntryOptions);
            }
            return View(model: _foods);
        }
        public IActionResult Index1()
        {
            if (!_memoryCache.TryGetValue(CACHE_FOODS, out List<Food> _foods))
            {

            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}