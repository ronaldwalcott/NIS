using Microsoft.AspNetCore.Mvc;
using MVCClient.Constants;
using MVCClient.Services;
using System;
using System.Threading.Tasks;

namespace MVCClient.Areas.SystemTables.Controllers
{
    [Area(AreaNames.SystemTablesArea)]
    public class TaskStatusController : Controller
    {
        private readonly IAuthToken _authToken;

        public TaskStatusController(IAuthToken authToken)
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