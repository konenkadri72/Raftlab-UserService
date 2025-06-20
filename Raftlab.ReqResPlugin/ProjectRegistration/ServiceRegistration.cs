using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Raftlab.ReqResImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raftlab.ReqResPlugin.ProjectRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterReqResServices(this IServiceCollection service)
        {
            service.AddHttpClient<IReqResClient, ReqResClientService>();

            return service;
        }
    }
}
