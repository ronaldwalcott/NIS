using Microsoft.AspNetCore.Mvc;
using MVCClient.Constants;
using MVCClient.Services;
using System;
using System.Threading.Tasks;

namespace MVCClient.Areas.SystemTables.Controllers
{
    [Area(AreaNames.SystemTablesArea)]
    public class DistrictController : Controller
    {
        private readonly IAuthToken _authToken;

        public DistrictController(IAuthToken authToken)
        {
            _authToken = authToken;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.AuthToken = await _authToken.GetToken();
            return View();
        }
    }
}