﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AppleUsed.Web.Controllers
{
    public class ModerationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}