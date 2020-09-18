using NISApi.Data.Entity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using IdentityModel;

namespace NISApi.Infrastructure.Helpers
{
    public class UserClaim
    {

        public UserData Claims(ClaimsPrincipal user)
        {
            UserData newUser = new UserData();
            newUser.UserName = user.Claims.First(claim => claim.Type == JwtClaimTypes.PreferredUserName).Value;
            newUser.UserId = user.Claims.First(claim => claim.Type == JwtClaimTypes.Subject).Value;
            return newUser;
        }
    }
}
