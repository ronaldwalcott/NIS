using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Simple.OData.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCClient.Services
{
    public class NisHttpOdataClient : INisHttpOdataClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ODataClient _oDataClient;
        private readonly IConfiguration _configuration;

        public NisHttpOdataClient(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
        //    _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;

        }

        public ODataClient GetClient()
        {
            var accessToken = _httpContextAccessor
                .HttpContext
                .GetTokenAsync(OpenIdConnectParameterNames.AccessToken).Result;
            string url = _configuration["ApiResourceBaseUrls:Api"] + "/odata";

            var client = new ODataClient(SetODataToken(url, accessToken));

            return client;
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

    }
}
