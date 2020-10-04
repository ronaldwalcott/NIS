using NISApi.Contracts;
using NISApi.Data.DataManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NISApi.Infrastructure.Installers
{
    internal class RegisterContractMappings : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            //Register Interface Mappings for Repositories
            services.AddTransient<IPersonManager, PersonManager>();
            services.AddTransient<ITableCollectionManager, TableCollectionManager>();
            services.AddTransient<ITableCountryManager, TableCountryManager>();
            services.AddTransient<ITableDistrictManager, TableDistrictManager>();
            services.AddTransient<ITableDocumentTypeManager, TableDocumentTypeManager>();
            services.AddTransient<ITableEmploymentTypeManager, TableEmploymentTypeManager>();
            services.AddTransient<ITableIndustryManager, TableIndustryManager>();
            services.AddTransient<ITableMaritalStatusManager, TableMaritalStatusManager>();
            services.AddTransient<ITableNationalityManager, TableNationalityManager>();
            services.AddTransient<ITableOccupationManager, TableOccupationManager>();
            services.AddTransient<ITableParishManager, TableParishManager>();
            services.AddTransient<ITablePostalCodeManager, TablePostalCodeManager>();
            services.AddTransient<ITablePostOfficeManager, TablePostOfficeManager>();
            services.AddTransient<ITableStreetManager, TableStreetManager>();
        }
    }
}
