using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DocsModels;

namespace Services.Interfaces.Docs
{
    public interface INativeTagService
    {
        Task<List<NativeTag>> GetAllTagsAsync(string tag);
        Task<NativeTag> GetTagByIdAsync(int id);
        Task<List<NativeTag>> GetAllTagsByNativeIdAsync(int id);

        Task AddTagsAsync(List<NativeTag> tags);
        Task AddTagAsync(NativeTag tag);

        Task UpdateTagAsync(NativeTag tag);

        Task RemoveTagAsync(int id);
        Task RemoveTagsByNativeId(int id);
    }
}