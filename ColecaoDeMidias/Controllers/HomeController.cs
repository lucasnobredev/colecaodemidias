using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ColecaoDeMidias.Models;
using ColecaoDeMidias.Services;
using AutoMapper;
using ColecaoDeMidias.Models.Home;
using ColecaoDeMidias.Domain;

namespace ColecaoDeMidias.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMidiaService midiaService;

        public HomeController(
                IMidiaService midiaService)
        {
            this.midiaService = midiaService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
