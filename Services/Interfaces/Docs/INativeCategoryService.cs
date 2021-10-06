using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DocsModels;

namespace Services.Interfaces.Docs
{
    public interface INativeCategoryService
    {
        Task<List<NativeCategory>> GetAllCategoriesAsync();
    }
}