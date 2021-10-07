using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DocsModels;
using ViewModels.Admin.Docs.NativeApiSet;

namespace Services.Interfaces.Docs
{
    public interface INativeApiService
    {
        Task<List<NativeApiSet>> GetAllApiSetsAsync();
        Task<NativeApiSet> GetApiSetByApiIdAsync(int id);

        Task AddApiSetAsync(NativeApiSet api);
        Task<NativeApiSet> AddApiSetAsync(AdminAddApiSetViewModel model);

        Task UpdateApiSetAsync(NativeApiSet api);
        Task<NativeApiSet> UpdateApiSetAsync(AdminEditApiSetViewModel model);

        Task RemoveApiSetAsync(int id);
    }
}