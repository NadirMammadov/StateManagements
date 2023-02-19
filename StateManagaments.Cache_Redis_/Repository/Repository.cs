using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using StateManagaments.Cache_Redis_.Extensions;
using StateManagaments.Cache_Redis_.Models;
using StateManagements.Session_.Interfaces;
using StateManagments.Models.Data;
using System.Reflection;
using System.Xml.Serialization;

namespace StateManagaments.Cache_Redis_.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDistributedCache _cache;
        private readonly StateManagmentContext _context;
        private const string CACHE_FOOD_KEY = "food";
        private readonly IMapper _mapper;
        public Repository(IDistributedCache cache, StateManagmentContext context, IMapper mapper)
        {
            _cache = cache;
            _context = context;
            _mapper = mapper;
        }

        public Task CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
