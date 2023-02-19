using System.Reflection;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using StateManagaments.Cache_Redis_.Extensions;
using StateManagaments.Cache_Redis_.Hubs;
using StateManagaments.Cache_Redis_.Models;

namespace StateManagaments.Cache_Redis_.BackgroundServices
{
    public class BackgroundWorkerService : BackgroundService
    {
        private FileSystemWatcher watcher;
        private IHubContext<FoodDepHub> hubContext;
        private const string CACHE_FOOD_KEY = "food";
        private readonly IDistributedCache _cache;
        

        public BackgroundWorkerService(IHubContext<FoodDepHub> hubContext, IDistributedCache cache)
        {
            this.hubContext = hubContext;
            _cache = cache;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            watcher = new();
            watcher.Path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            watcher.Filter = "*.xml";
            //watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            watcher.Changed += On_Changed;
            return Task.CompletedTask;
        }
        async void On_Changed(object sender, FileSystemEventArgs e)
        {
            var foods = await _cache.GetRecordAsync<IEnumerable<FoodDto>>(CACHE_FOOD_KEY);
            await hubContext.Clients.All.SendAsync("GetFoods", foods);
        }

       
    }
}