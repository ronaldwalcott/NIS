using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Services
{
    public class AuthToken : IAuthToken
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthToken(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<String> GetToken()
        {
            var accessToken = await _httpContextAccessor
                .HttpContext
                .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);


            return accessToken;
        }
    }
}
