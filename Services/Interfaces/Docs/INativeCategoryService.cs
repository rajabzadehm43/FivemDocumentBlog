using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DocsModels;
using ViewModels.Admin.Docs.NativeCategory;

namespace Services.Interfaces.Docs
{
    public interface INativeCategoryService
    {
        Task<List<NativeCategory>> GetAllCategoriesAsync();
        Task<NativeCategory> GetCategoryByIdAsync(int id);

        Task AddCategoryAsync(NativeCategory category);
        Task<NativeCategory> AddCategoryAsync(AdminAddNativeCategoryViewModel model);

        Task UpdateCategoryAsync(NativeCategory category);
        Task<NativeCategory> UpdateCategoryAsync(AdminEditNativeCategoryViewModel model);

        Task RemoveCategoryByIdAsync(int id);
    }
}