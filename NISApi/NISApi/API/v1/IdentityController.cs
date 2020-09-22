using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NISApi.API.v1
{
    [Route("identity")]
       //[Authorize]
    //[Authorize("ShowPrivacy")]
    //[AllowAnonymous]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var theUser = User.Claims.ToList();
            var userName = User.Claims.First(claim => claim.Type == JwtClaimTypes.PreferredUserName).Value;
            Console.WriteLine("PreferreduserName = " + JwtClaimTypes.PreferredUserName);
            Console.WriteLine("userName = " + userName);
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}