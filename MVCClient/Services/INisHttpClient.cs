using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCClient.Services
{
    public interface INisHttpClient
    {
        Task<HttpClient> GetClient();
    }
}
