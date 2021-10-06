using FivemDocumentBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using Models.DocsModels;
using Services.Interfaces.Docs;

namespace FivemDocumentBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INativeService _service;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, INativeService service, ApplicationDbContext context)
        {
            _logger = logger;
            _service = service;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            // var ss = _context.Natives.Skip(10).Take(60).ToList();
            var ss = await _service.GetNativesByPagingAsync();

            var dataEF = _context.Natives
                // .Include(c => c.ApiSet)
                .ToList();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
