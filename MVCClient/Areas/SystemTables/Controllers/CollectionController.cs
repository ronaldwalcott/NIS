﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCClient.Areas.SystemTables.Controllers
{
    [Area("SystemTables")]
    public class CollectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}