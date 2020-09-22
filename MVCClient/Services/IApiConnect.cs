using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Services
{
    public interface IApiConnect
    {
        Task<TResponse> PostDataAsync<TResponse, TRequest>(string endPoint, TRequest request);
        Task<TResponse> GetDataAsync<TResponse>(string endPoint);
    }
}
