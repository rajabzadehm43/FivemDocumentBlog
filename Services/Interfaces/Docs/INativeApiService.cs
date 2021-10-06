using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DocsModels;

namespace Services.Interfaces.Docs
{
    public interface INativeApiService
    {
        Task<List<NativeApiSet>> GetAllApiSetsAsync();
    }
}