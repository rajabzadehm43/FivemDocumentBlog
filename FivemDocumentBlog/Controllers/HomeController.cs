using FivemDocumentBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
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
        private readonly IDbConnection _db;
        private readonly INativeTagService _tagService;

        public HomeController(ILogger<HomeController> logger, INativeService service, ApplicationDbContext context, IDbConnection db, INativeTagService tagService)
        {
            _logger = logger;
            _service = service;
            _context = context;
            _db = db;
            _tagService = tagService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
