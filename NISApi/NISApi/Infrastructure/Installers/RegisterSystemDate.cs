using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NISApi.Contracts;
using NISApi.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Infrastructure.Installers
{
    internal class RegisterSystemDate : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IDateTimeUtc, SystemDateTimeUtc>();
        }
    }
}
