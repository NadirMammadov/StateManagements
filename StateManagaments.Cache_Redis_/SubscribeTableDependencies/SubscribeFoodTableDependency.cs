using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using StateManagaments.Cache_Redis_.Extensions;
using StateManagaments.Cache_Redis_.Interfaces;
using StateManagaments.Cache_Redis_.Models;
using StateManagments.Models.Data;
using StateManagments.Models.Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace StateManagaments.Cache_Redis_.SubscribeTableDependencies
{
    public class SubscribeFoodTableDependency
    {
        private readonly IDistributedCache _cache;
        private readonly IRepository _repository;
        private const string CACHE_FOOD_KEY = "food";
        private readonly IMapper _mapper;

        public SubscribeFoodTableDependency(IDistributedCache cache, IMapper mapper, IRepository repository)
        {
            _cache = cache;
            _mapper = mapper;
            _repository = repository;
        }

        SqlTableDependency<Food> tableDependency;
        public void SubscribeTableDependency()
        {
            string connectionString = "Server=MACHINE;Database=StateManagementDb;Trusted_Connection=True;TrustServerCertificate=true;";
            tableDependency = new SqlTableDependency<Food>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Food)} SqlTableDependency error {e.Error.Message}");
        }

        private void TableDependency_OnChanged(object sender, RecordChangedEventArgs<Food> e)
        {
            if(e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                var foods =  _cache.GetRecordAsync<IEnumerable<FoodDto>>(CACHE_FOOD_KEY);
                if (foods == null)
                {
                    var category = _repository.GetAll();
                    foods = (Task<IEnumerable<FoodDto>>?)_mapper.Map<IEnumerable<FoodDto>>(category.Result.Foods);
                     _cache.SetRecordAsync(recordKey: CACHE_FOOD_KEY, data: foods, unusedExpirationTime: TimeSpan.FromSeconds(30));
                }
            }
        }
    }
}
