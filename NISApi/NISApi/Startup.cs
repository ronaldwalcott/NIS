using NISApi.Infrastructure.Configs;
using NISApi.Infrastructure.Extensions;
using AspNetCoreRateLimit;
using AutoMapper;
using AutoWrapper;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Builder;
using NISApi.Data;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Net.Http.Headers;
using System.Linq;
using Microsoft.AspNet.OData.Formatter;
using NISApi.DTO.Response.SystemTables;

namespace NISApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NisDbContext>(options =>
                options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

            //Register services in Installers folder
            services.AddServicesInAssembly(Configuration);

            //Register MVC/Web API, NewtonsoftJson and add FluentValidation Support
            //services.AddControllers()
            //        .AddNewtonsoftJson(ops => { ops.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; })
            //        .AddFluentValidation(fv => { fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false; });

            services.AddControllers()
        .AddNewtonsoftJson()
        .AddFluentValidation(fv => { fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false; });


            //Odata
            services.AddOData();

            //Register Automapper
            services.AddAutoMapper(typeof(MappingProfileConfiguration));

            services.AddPolicyServerClient(Configuration.GetSection("Policy"))
                .AddAuthorizationPermissionPolicies();
            AddFormatters(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //Enable Swagger and SwaggerUI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NISApi ASP.NET Core API v1");
            });

            //Enable HealthChecks and UI
            //app.UseHealthChecks("/selfcheck", new HealthCheckOptions
            //{
            //    Predicate = _ => true,
            //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            //}).UseHealthChecksUI(setup =>
            //{
            //    setup.AddCustomStylesheet($"{env.ContentRootPath}/Infrastructure/HealthChecks/Ux/branding.css");
            //});

            //Enable AutoWrapper.Core
            //More info see: https://github.com/proudmonkey/AutoWrapper
            //            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { IsDebug = true, UseApiProblemDetailsException = true });
            //app.UseApiResponseAndExceptionWrapper();
            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
            {
                IgnoreWrapForOkRequests = true,
            });

            //Enable AspNetCoreRateLimit
            app.UseIpRateLimiting();

            app.UseRouting();

            //Enable CORS
            app.UseCors("AllowAll");

            //Adds authenticaton middleware to the pipeline so authentication will be performed automatically on each request to host
            app.UseAuthentication();

            //Add PolicyServer claims mapping middleware
            app.UsePolicyServerClaims();

            //Adds authorization middleware to the pipeline to make sure the Api endpoint cannot be accessed by anonymous clients
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.Select().Filter().OrderBy().Count().MaxTop(50);
                endpoints.MapODataRoute("odata", "odata", GetEdmModel());

            });
        }

        private IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            //odataBuilder.EntitySet<CountryQueryResponse>("TableCountries");
            odataBuilder.EntitySet<CollectionQueryResponse>("TableCollections");
            //odataBuilder.EntitySet<DistrictQueryResponse>("TableDistricts");
            //odataBuilder.EntitySet<DocumentTypeQueryResponse>("TableDocumentTypes");
            //odataBuilder.EntitySet<EmploymentTypeQueryResponse>("TableEmploymentTypes");
            //odataBuilder.EntitySet<IndustryQueryResponse>("TableIndustries");
            //odataBuilder.EntitySet<MaritalStatusQueryResponse>("TableMaritalStatuses");
            //odataBuilder.EntitySet<NationalityQueryResponse>("TableNationalities");
            //odataBuilder.EntitySet<OccupationQueryResponse>("TableOccupations");
            //odataBuilder.EntitySet<ParishQueryResponse>("TableParishes");
            //odataBuilder.EntitySet<PostalCodeQueryResponse>("TablePostalCodes");
            //odataBuilder.EntitySet<PostOfficeQueryResponse>("TablePostOffices");



            //  odataBuilder.EntityType<PersonResponse>().DerivesFromNothing();

            return odataBuilder.GetEdmModel();
        }
  
    private void AddFormatters(IServiceCollection services)
        {
            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }

                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }

            });
        }
    }
}
