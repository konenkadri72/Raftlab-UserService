using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Raftlab.Service.ProjectRegister
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection service)
        {
            return service;
        }
    }
}
