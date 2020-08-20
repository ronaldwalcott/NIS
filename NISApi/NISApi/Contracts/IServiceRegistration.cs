using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NISApi.Contracts
{
    public interface IServiceRegistration
    {
        void RegisterAppServices(IServiceCollection services, IConfiguration configuration);
    }
}
