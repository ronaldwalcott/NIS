using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Services;

namespace MVCClient.Areas.SystemTables.Controllers
{
    [Area("SystemTables")]
    public class CollectionController : Controller
    {
        private readonly IAuthToken _authToken;

        public CollectionController(IAuthToken authToken)
        {
            _authToken = authToken;
        }

        public async Task<IActionResult> Index()
        {
            String x = await  _authToken.GetToken();
            ViewBag.AuthToken = x;
            return View(); 
        }

        public IActionResult ViewTest()
        {
            return View();
        }


    }
}