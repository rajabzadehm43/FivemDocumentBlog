using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models.DocsModels;
using ViewModels.Docs;

namespace Services.Interfaces.Docs
{
    public interface INativeService
    {
        Task<List<Native>> GetNativesWithoutRelAsync();
        Task<List<Native>> GetNativesAsync(string q = "");
        Task<List<Native>> GetTopNativesAsync(int take = 10);
        Task<Native> GetNativeByIdAsync(int id);
        Task<Native> GetNativeWithAllRelationsById(int id);
        Task<Tuple<List<Native>, int>> GetNativesByPagingAsync(string q = "", int take=10, int skip = 0);


        Task AddNativeAsync(Native native);
        Task<Native> AddNativeAsync(AdminAddNativeViewModel model, string authorId);
        Task<Tuple<string, string>> SaveNativeDescriptionFile(IFormFile file);

        Task UpdateNativeAsync(Native native);
        Task<Native> UpdateNativeAsync(AdminEditNativeViewModel model);

        Task RemoveNative(int id);
    }
}