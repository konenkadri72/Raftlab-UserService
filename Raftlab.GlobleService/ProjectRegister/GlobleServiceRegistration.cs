using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Raftlab.GlobleService.CachingService;
using Raftlab.GlobleService.UserService;
using Raftlab.ReqResImplementation;
using Raftlab.Service.GlobleServices.CachingService;
using Raftlab.Service.UserService;

namespace Raftlab.GlobleService.ProjectRegister
{
    public static class GlobleServiceRegistration
    {
        public static IServiceCollection RegisterGlobleService(this IServiceCollection service)
        {
            service.AddMemoryCache();
            service.TryAddSingleton(typeof(ICacheService), typeof(CacheService));
            service.AddScoped(typeof(IExternalUserService), typeof(ExternalUserService));
            return service;
        }
    }
}
