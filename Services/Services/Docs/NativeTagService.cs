using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http.Features;
using Models.DocsModels;
using Services.Interfaces.Docs;

namespace Services.Services.Docs
{
    public class NativeTagService : INativeTagService
    {

        private IDbConnection _db;

        public NativeTagService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<List<NativeTag>> GetAllTagsAsync(string tag)
        {
            var query = "Select * From NativeTags Where Tag Like @Tag";
            var result = _db.Query<NativeTag>(query, new
            {
                Tag = $"%{tag}%"
            });

            return result.ToList();
        }

        public async Task<NativeTag> GetTagByIdAsync(int id)
        {
            var query = "Select * From NativeTags Where TagId = @TagId";
            var result = _db.QueryFirst<NativeTag>(query, new
            {
                TagId = id
            });

            return result;
        }

        public async Task<List<NativeTag>> GetAllTagsByNativeIdAsync(int id)
        {
            var query = "Select * From NativeTags Where NativeId = @NativeId";
            var result = _db.Query<NativeTag>(query, new {NativeId = id});

            return result.ToList();
        }

        public async Task AddTagsAsync(List<NativeTag> tags)
        {
            var query = "";
            var inputParameter = new DynamicParameters();

            for (int i = 0; i < tags.Count; i++)
            {
                var tag = tags[i];
                query += $"Insert Into NativeTags (Tag, NativeId) Values (@Tag{i}, @NativeId{i});" +
                         "Select Cast(SCOPE_IDENTITY() As int)";
                inputParameter.Add($"Tag{i}", tag.Tag);
                inputParameter.Add($"NativeId{i}", tag.NativeId > 0 ? tag.NativeId : tag.Native.NativeId);
            }

            var result = await _db.QueryMultipleAsync(query, inputParameter);

            for (int i = 0; i < tags.Count; i++)
            {
                tags[i].TagId = result.ReadFirst<int>();
            }

        }

        public async Task AddTagAsync(NativeTag tag)
        {
            var query = "Insert Into NativeTags (Tag, NativeId) Values (@Tag, @NativeId);" +
                        "Select Cast(SCOPE_IDENTITY() As int)";
            tag.TagId = _db.QueryFirst<int>(query, tag);
        }

        public async Task UpdateTagAsync(NativeTag tag)
        {
            var query = "Update NativeTags Set Tag = @Tag, NativeId = @NativeId Where TagId = @TagId";
            await _db.ExecuteAsync(query, tag);
        }

        public async Task RemoveTagAsync(int id)
        {
            var query = "Delete From NativeTags Where TagId = @TagId";
            await _db.ExecuteAsync(query, new {TagId = id});
        }

        public async Task RemoveTagsByNativeId(int id)
        {
            var query = "Delete From NativeTags Where NativeId = @NativeId";
            await _db.ExecuteAsync(query, new {NativeId = id});
        }
    }
}