using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.DocsModels;
using ViewModels.Docs;

namespace Services.Interfaces.Docs
{
    public interface INativeService
    {
        Task<List<Native>> GetNativesAsync();
        Task<List<Native>> GetTopNativesAsync(int take = 10);
        Task<Native> GetNativeByIdAsync(int id);
        Task<Tuple<List<Native>, int>> GetNativesByPagingAsync(string q = "", int take=10, int skip = 0);


        Task AddNativeAsync(Native native);
        Task<Native> AddNativeAsync(AdminAddNativeViewModel model);

        Task UpdateNativeAsync(Native native);
        Task<Native> UpdateNativeAsync(AdminEditNativeViewModel model);

        Task RemoveNative(int id);
    }
}