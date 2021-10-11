using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces.General;
using ViewModels.Admin.Menus;

namespace FivemDocumentBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class MenuController : Controller
    {

        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public IActionResult Index()
        {
            return View(_menuService.GetMenusAsync().Result);
        }


        #region Create Menu

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminAddMenuViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _menuService.AddMenuAsync(model);
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit Menu

        public async Task<IActionResult> Edit(int id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            var model = new AdminEditMenuViewModel
            {
                Title = menu.Title,
                Target = menu.Target,
                Rel = menu.Rel,
                TargetUrl = menu.TargetUrl,
                MenuId = menu.MenuId,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AdminEditMenuViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _menuService.UpdateMenuAsync(model);
            return RedirectToAction("Edit", new {id});
        }

        #endregion

        #region Remove Menu

        public async Task<IActionResult> Remove(int id)
        {
            await _menuService.RemoveMenuAsync(id);
            return RedirectToAction("Index");
        }

        #endregion

    }
}
