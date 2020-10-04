using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCClient.Helpers;
using MVCClient.Services;

namespace MVCClient
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Register a Typed Instance of HttpClientFactory for AuthService 
            //services.AddHttpClient<IAuthServerConnect, AuthServerConnect>();            
            
            services.AddScoped<INisHttpClient, NisHttpClient>();
            services.AddScoped<IAuthToken, AuthToken>();

            //services.AddTransient<ProtectedApiBearerTokenHandler>();


            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = "Cookies";
                options.Authority = Configuration["ApiResourceBaseUrls:AuthServer"]; 

                options.ClientId = "mvc";
                options.ClientSecret = "secret";
                //options.ResponseType = "code";
                options.ResponseType = "code id_token";

                options.Scope.Add("api1");
                options.Scope.Add("NISapi");

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
            }
            
            
            
            );
            services.AddPolicyServerClient(Configuration.GetSection("Policy"))
                .AddAuthorizationPermissionPolicies();

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                // Register other policies here
            });


            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzA5MDEzQDMxMzgyZTMyMmUzMEpQakxiMnlBZTVRRjZENUlNcWtNY0Q1TTFJMHYyZzZVT1dWRHJtZjFCVWc9");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UsePolicyServerClaims();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "arearoute",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
                .RequireAuthorization();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
                .RequireAuthorization();
                //endpoints.MapRazorPages()
                //.RequireAuthorization();
            });

            //endpoints.MapAreaControllerRoute(
            //    name: "arearoute",
            //    areaName: "SystemTables",
            //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapDefaultControllerRoute()
            //        .RequireAuthorization();
            //});


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
