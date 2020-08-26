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

namespace MVCClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INisHttpClient _nisHttpClient;

        public HomeController(INisHttpClient nisHttpClient, ILogger<HomeController> logger)
        {
            _nisHttpClient = nisHttpClient;
            _logger = logger;
        }

       
        public IActionResult Index()
        {
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

            var response = await httpClient.GetAsync("https://localhost:6001/identity").ConfigureAwait(false);

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
