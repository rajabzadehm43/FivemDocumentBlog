using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Services.Interfaces.Docs;
using ViewModels.Admin.Docs.NativeApiSet;

namespace FivemDocumentBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NativeApiController : Controller
    {

        private readonly INativeApiService _apiService;

        public NativeApiController(INativeApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var apiSets = await _apiService.GetAllApiSetsAsync();
            return View(apiSets);
        }

        #region Create Api Set

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminAddApiSetViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _apiService.AddApiSetAsync(model);
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit Api Set

        public async Task<IActionResult> Edit(int id)
        {
            var api = (await _apiService.GetApiSetByApiIdAsync(id));
            var model = new AdminEditApiSetViewModel
            {
                Name = api.Name,
                ClassName = api.ClassName,
                ApiId = id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AdminEditApiSetViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _apiService.UpdateApiSetAsync(model);
            return RedirectToAction("Edit", new {id});
        }

        #endregion

        #region Remove Api Set

        public async Task<IActionResult> Remove(int id)
        {
            await _apiService.RemoveApiSetAsync(id);
            return RedirectToAction("Index");
        }

        #endregion

    }
}
