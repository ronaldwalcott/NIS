using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MVCClient.Services;
using Simple.OData.Client;

namespace MVCClient.Controllers
{
    public class KanbanTaskController : Controller
    {
        private readonly ILogger<KanbanTaskController> _logger;
        private readonly INisHttpClient _nisHttpClient;
        private readonly INisHttpOdataClient _nisHttpOdataClient;
        private readonly IConfiguration _configuration;

        public KanbanTaskController(INisHttpClient nisHttpClient, INisHttpOdataClient nisHttpOdataClient, IConfiguration configuration, ILogger<KanbanTaskController> logger)
        {
            _nisHttpClient = nisHttpClient;
            _nisHttpOdataClient = nisHttpOdataClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //var accessToken = HttpContext.GetTokenAsync("access_token").Result;
            //string url = _configuration["ApiResourceBaseUrls:Api"] + "/odata";

            //var client = new ODataClient(SetODataToken(url, accessToken));

            //var taskPriorities = await client.For<TableTaskPriorities>().FindEntriesAsync();

            var taskPriorities = _nisHttpOdataClient.GetClient().For<TableTaskPriorities>().FindEntriesAsync().Result;


            //var httpClient = await _nisHttpClient.GetClient();
            //var response = await httpClient.GetAsync(_configuration["ApiResourceBaseUrls:Api"] + "/odata/TableTaskPriorities").ConfigureAwait(false);

            //if (response.IsSuccessStatusCode)
            //{
            //    var apiResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            //}

            return View();
        }

        private ODataClientSettings SetODataToken(string url, string accessToken)
        {
            var oDataClientSettings = new ODataClientSettings(new Uri(url));
            oDataClientSettings.BeforeRequest += delegate (HttpRequestMessage message)
            {
                message.Headers.Add("Authorization", "Bearer " + accessToken);

            };
            oDataClientSettings.PayloadFormat = ODataPayloadFormat.Json;
          
            return oDataClientSettings;
        }

        public class TableTaskPriorities
        {
            public long ID { get; set; }
            public string Code { get; set; }
            public string ShortDescription { get; set; }
            public string LongDescription { get; set; }
        }
    }


}