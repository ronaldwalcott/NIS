using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCClient.Models;
//using Microsoft.AspNetCore.DataProtection;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using MVCClient.Services;
using Microsoft.Extensions.Configuration;

namespace MVCClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INisHttpClient _nisHttpClient;
        private readonly IConfiguration _configuration;

        public HomeController(INisHttpClient nisHttpClient, IConfiguration configuration, ILogger<HomeController> logger)
        {
            _nisHttpClient = nisHttpClient;
            _configuration = configuration;
            _logger = logger;
        }

       
        public IActionResult Index()
        {
            List<object> listdata = new List<object>();
            listdata.Add(new
            {
                text = "Audi A4",
                id = "9bdb",
                category = "Audi"
            });
            listdata.Add(new
            {
                text = "Audi A4",
                id = "9bdb",
                category = "Audi"
            });
            listdata.Add(new
            {
                text = "Audi A5",
                id = "4589",
                category = "Audi"
            });

            listdata.Add(new
            {
                text = "Audi A6",
                id = "e807",
                category = "Audi"
            });
            listdata.Add(new
            {
                text = "Audi A7",
                id = "a0cc",
                category = "Audi"
            });
            listdata.Add(new
            {
                text = "Audi A8",
                id = "5e26",
                category = "Audi"
            });
            listdata.Add(new
            {
                text = "BMW 501",
                id = "f849",
                category = "BMW"
            });
            listdata.Add(new
            {
                text = "BMW 502",
                id = "7aff",
                category = "BMW"
            });
            listdata.Add(new
            {
                text = "BMW 503",
                id = "b1da",
                category = "BMW"
            });
            listdata.Add(new
            {
                text = "BMW 507",
                id = "de2f",
                category = "BMW"
            });
            listdata.Add(new
            {
                text = "BMW 3200",
                id = "b2b1",
                category = "BMW"
            });
            List<object> data = new List<object>();
            data.Add(new { text = "Hennessey Venom", id = "list-01" });
            data.Add(new { text = "Bugatti Chiron", id = "list-02" });
            data.Add(new { text = "Bugatti Veyron Super Sport", id = "list-03" });
            data.Add(new { text = "SSC Ultimate Aero", id = "list-04" });
            data.Add(new { text = "Koenigsegg CCR", id = "list-05" });
            data.Add(new { text = "McLaren F1", id = "list-06" });
            data.Add(new { text = "Aston Martin One- 77", id = "list-07" });
            data.Add(new { text = "Jaguar XJ220", id = "list-08" });
            data.Add(new { text = "McLaren P1", id = "list-09" });
            data.Add(new { text = "Ferrari LaFerrari", id = "list-10" });
            data.Add(new { text = "Mercedes-Benz Aston Martin", id = "list-11" });
            data.Add(new { text = "Zenvo ST1", id = "list-12" });
            data.Add(new { text = "Lamborghini Veneno", id = "list-13" });

            ViewBag.groupData = listdata;
            ViewBag.dataSource = data;




            return View();
        }




        //[Authorize(Roles = "administrator")]
        //[Authorize("ShowPrivacy")]
        public async Task<IActionResult> Privacy()
        {
            //var client = new HttpClient();

            //var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            //if (disco.IsError)
            //{
            //    Console.WriteLine("1 " + disco.Error);
            //    return View();
            //}

            //// request token

            //var tokenResponse = await client.RequestAuthorizationCodeTokenAsync (new AuthorizationCodeTokenRequest
            ////var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            //{
            //    Address = disco.TokenEndpoint,

            //    ClientId = "mvc",
            //    ClientSecret = "secret",

            //    //Scope = "api1"
            //});

            //if (tokenResponse.IsError)
            //{
            //    Console.WriteLine("2 " + tokenResponse.Error);
            //    return View();
            //}

            //Console.WriteLine(tokenResponse.Json);

            //// call api
            //var apiClient = new HttpClient();
            //apiClient.SetBearerToken(tokenResponse.AccessToken);




            //var response = await apiClient.GetAsync("https://localhost:6001/identity");
            //if (!response.IsSuccessStatusCode)
            //{
            //    Console.WriteLine("3 " + response.StatusCode);
            //}
            //else
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //}


            //using (var httpClient = new HttpClient())
            //{
            //    using (var response = await httpClient.GetAsync("http://localhost:6001/identity"))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();

            //    }
            //}

            var httpClient = await _nisHttpClient.GetClient();

            var response = await httpClient.GetAsync(_configuration["ApiResourceBaseUrls:Api"] + "/identity").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            }
            else
            {
                Console.WriteLine("Call to identity claims client failed");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
