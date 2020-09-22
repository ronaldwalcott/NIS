using AutoWrapper.Server;
using Microsoft.Extensions.Logging;
using MVCClient.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVCClient.Services
{
    public class NisApiConnect : IApiConnect
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NisApiConnect> _logger;
        public NisApiConnect(HttpClient httpClient, ILogger<NisApiConnect> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<NisApiResponse> PostDataAsync<NisApiResponse, NisApiRequest>(string endPoint, NisApiRequest dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, HttpContentMediaTypes.JSON);
            var httpResponse = await _httpClient.PostAsync(endPoint, content);
            
            if (!httpResponse.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Warning, $"[{httpResponse.StatusCode}] An error occured while requesting external api.");
                return default;
            }

            var jsonString = await httpResponse.Content.ReadAsStringAsync();
            var data = Unwrapper.Unwrap<NisApiResponse>(jsonString);

            return data;
        }

        public async Task<NisApiResponse> GetDataAsync<NisApiResponse>(string endPoint)
        {
            var httpResponse = await _httpClient.GetAsync(endPoint);

            if (!httpResponse.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Warning, $"[{httpResponse.StatusCode}] An error occured while requesting external api.");
                return default;
            }

            var jsonString = await httpResponse.Content.ReadAsStringAsync();
            var data = Unwrapper.Unwrap<NisApiResponse>(jsonString);

            return data;
        }

    }
}
