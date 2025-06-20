using Raftlab.Service.GlobleServices.CachingService;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Raftlab.GlobleService.CachingService
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _catchManager;
        public CacheService(IMemoryCache catchManager)
        {
            _catchManager = catchManager;
        }
        public void Set<T>(string key, T value, TimeSpan duration)
        {
            _catchManager.Set<T>(key, value, duration);
        }


        public bool TryGetValue<T>(string key, out T output)
        {
            return _catchManager.TryGetValue<T>(key, out output);
        }

        public void Remove(string key)
        {
            _catchManager.Remove(key);
        }
    }
}
