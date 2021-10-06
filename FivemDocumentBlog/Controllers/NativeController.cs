using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataLayer.Data;
using Services.Interfaces.Docs;

namespace FivemDocumentBlog.Controllers
{
    public class NativeController : Controller
    {
        private readonly INativeService _nativeService;

        private readonly ApplicationDbContext _context;

        public NativeController(INativeService nativeService, ApplicationDbContext context)
        {
            _nativeService = nativeService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _nativeService.GetTopNativesAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var native = await _nativeService.GetNativeByIdAsync(id);
            return Ok(native);
        }

    }
}
