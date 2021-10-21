using FivemDocumentBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using DataLayer.Data;
using Flurl.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;
using Models.DocsModels;
using Services.Interfaces.Docs;
using ViewModels;

namespace FivemDocumentBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INativeService _nativeService;

        private readonly ApplicationDbContext _context;
        private readonly IDbConnection _db;
        private readonly INativeTagService _tagService;

        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, INativeService nativeService, ApplicationDbContext context, IDbConnection db, INativeTagService tagService, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _nativeService = nativeService;
            _context = context;
            _db = db;
            _tagService = tagService;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index(string q = "")
        {
            if (q == null) return RedirectToAction("Index");
            var natives = await _nativeService.GetNativesAsync(q);
            ViewData["q"] = q;
            return View(natives);
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        [Route("sitemap.xml")]
        public async Task<IActionResult> SiteMap()
        {
            var natives = await _nativeService.GetNativesWithoutRelAsync();
            return View(natives);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
