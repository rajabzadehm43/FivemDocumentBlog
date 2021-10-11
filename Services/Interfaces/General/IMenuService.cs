using System.Collections.Generic;
using System.Threading.Tasks;
using Models.GeneralModels;
using ViewModels.Admin.Menus;

namespace Services.Interfaces.General
{
    public interface IMenuService
    {

        Task<IList<Menu>> GetMenusAsync();
        Task<Menu> GetMenuByIdAsync(int id);

        Task AddMenuAsync(Menu menu);
        Task<Menu> AddMenuAsync(AdminAddMenuViewModel model);

        Task UpdateMenuAsync(Menu menu);
        Task<Menu> UpdateMenuAsync(AdminEditMenuViewModel model);

        Task RemoveMenuAsync(int id);
    }
}