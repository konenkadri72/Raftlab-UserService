using System;
using System.Collections.Generic;
using System.Text;

namespace Raftlab.Service.GlobleServices.CachingService
{
    public interface ICacheService
    {
        void Set<T>(string key, T value, TimeSpan duration);
        bool TryGetValue<T>(string key, out T value);
        void Remove(string key);

    }
}
