﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Web.Controllers
{
    public class CheckoutController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}