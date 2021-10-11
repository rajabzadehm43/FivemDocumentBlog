using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Models.GeneralModels;
using Services.Interfaces.General;
using ViewModels.Admin.Menus;

namespace Services.Services.General
{
    public class MenuService : IMenuService
    {

        private readonly IDbConnection _db;

        public MenuService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IList<Menu>> GetMenusAsync()
        {
            var query = "Select * From Menus";
            var result = await _db.QueryAsync<Menu>(query);

            return result.ToList();
        }

        public async Task<Menu> GetMenuByIdAsync(int id)
        {
            var query = "Select * From Menus Where MenuId = @id;";
            return await _db.QueryFirstAsync<Menu>(query, new {id});
        }

        public async Task AddMenuAsync(Menu menu)
        {
            var query = "Insert Into Menus (Title, TargetUrl, Target, Rel) Values (@Title, @TargetUrl, @Target, @Rel);" +
                        "Select Cast(Scope_Identity() As int)";
            menu.MenuId = await _db.QueryFirstAsync<int>(query, menu);
        }

        public async Task<Menu> AddMenuAsync(AdminAddMenuViewModel model)
        {
            var menu = new Menu
            {
                Title = model.Title,
                TargetUrl = model.TargetUrl,
                Target = model.Target,
                Rel = model.Rel
            };

            await AddMenuAsync(menu);
            return menu;
        }

        public async Task UpdateMenuAsync(Menu menu)
        {
            var query = "Update Menus Set Title = @Title, TargetUrl = @TargetUrl, " +
                        "Target = @Target, Rel = @Rel Where MenuId = @MenuId";
            await _db.ExecuteAsync(query, menu);
        }

        public async Task<Menu> UpdateMenuAsync(AdminEditMenuViewModel model)
        {
            var menu = new Menu
            {
                Title = model.Title,
                Target = model.Target,
                Rel = model.Rel,
                TargetUrl = model.TargetUrl,
                MenuId = model.MenuId
            };

            await UpdateMenuAsync(menu);
            return menu;
        }

        public async Task RemoveMenuAsync(int id)
        {
            var query = "Delete From Menus Where MenuId = @id";
            await _db.ExecuteAsync(query, new {id});
        }
    }
}