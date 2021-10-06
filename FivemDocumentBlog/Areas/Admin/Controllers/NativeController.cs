using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.DocsModels;
using Services.Interfaces.Docs;
using ViewModels.Admin.Docs;
using ViewModels.Docs;

namespace FivemDocumentBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NativeController : Controller
    {

        private readonly INativeService _nativeService;
        private readonly INativeApiService _apiService;
        private readonly INativeCategoryService _categoryService;

        public NativeController(INativeService nativeService, INativeApiService apiService, INativeCategoryService categoryService)
        {
            _nativeService = nativeService;
            _apiService = apiService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, string q = "")
        {

            var take = 10;
            var skip = (page - 1) * take;

            var nData = await _nativeService.GetNativesByPagingAsync(q, take, skip);

            var allPages = (int) Math.Ceiling((decimal) nData.Item2 / take);

            var model = new AdminIndexNativeViewModel
            {
                Natives = nData.Item1,
                AllPages = allPages,
                Page = page,
                Q = q
            };
            return View(model);
        }

        #region Create Native

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminAddNativeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _nativeService.AddNativeAsync(model);
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit Native

        public async Task<IActionResult> Edit(int id)
        {
            var native = await _nativeService.GetNativeByIdAsync(id);
            var model = new AdminEditNativeViewModel
            {
                ApiSetId = native.ApiSetId,
                ShortDescription = native.ShortDescription,
                CategoryId = native.CategoryId,
                SampleCode = native.SampleCode,
                Description = native.SampleCode,
                NativeName = native.NativeName,
                NativeId = native.NativeId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AdminEditNativeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _nativeService.UpdateNativeAsync(model);
            return RedirectToAction("Edit", new {id});
        }

        #endregion

        #region Remove Native

        public async Task<IActionResult> Remove(int id)
        {
            await _nativeService.RemoveNative(id);
            return RedirectToAction("Index");
        }

        #endregion

    }
}