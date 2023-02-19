using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using StateManagaments.Cache_Redis_.Extensions;
using StateManagaments.Cache_Redis_.Hubs;
using StateManagaments.Cache_Redis_.Interfaces;
using StateManagaments.Cache_Redis_.Models;
using StateManagments.Models.Data;
using System.Reflection;
using System.Xml.Serialization;

namespace StateManagaments.Cache_Redis_.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        public HomeController(IFoodRepository foodRepository)
        {
           
            _foodRepository = foodRepository;
        }

        public  async Task<IActionResult> Index()
        {
            var foods = await _foodRepository.GetAllAsync();
            return View();
        }

       
    }
}