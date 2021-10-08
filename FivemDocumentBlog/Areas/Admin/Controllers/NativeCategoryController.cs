using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Services.Interfaces.Docs;
using ViewModels.Admin.Docs.NativeCategory;

namespace FivemDocumentBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NativeCategoryController : Controller
    {

        private readonly INativeCategoryService _categoryService;

        public NativeCategoryController(INativeCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        #region Create Category

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminAddNativeCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _categoryService.AddCategoryAsync(model);
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit Category

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            var model = new AdminEditNativeCategoryViewModel
            {
                Name = category.CategoryName,
                ClassName = category.ClassName,
                CategoryId = category.CategoryId
            };

            return View(model);
        }

        #endregion

        #region Remove Category

        public async Task<IActionResult> Remove(int id)
        {
            await _categoryService.RemoveCategoryByIdAsync(id);
            return RedirectToAction("Index");
        }

        #endregion

    }
}
