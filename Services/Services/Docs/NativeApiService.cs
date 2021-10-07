using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Models.DocsModels;
using Services.Interfaces.Docs;
using ViewModels.Admin.Docs.NativeApiSet;

namespace Services.Services.Docs
{
    public class NativeApiService : INativeApiService
    {

        private readonly IDbConnection _db;

        public NativeApiService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<List<NativeApiSet>> GetAllApiSetsAsync()
        {
            var query = "Select * From NativeApiSets";
            return (await _db.QueryAsync<NativeApiSet>(query)).ToList();
        }

        public async Task<NativeApiSet> GetApiSetByApiIdAsync(int id)
        {
            var query = "Select * From NativeApiSets Where ApiSetId = @ApiId";

            return _db.QueryFirst<NativeApiSet>(query, new {ApiId = id});
        }

        public async Task AddApiSetAsync(NativeApiSet api)
        {
            var query = "Insert Into NativeApiSets (Name, ClassName) Values (@Name, @ClassName);"
                + "Select Cast(SCOPE_IDENTITY() As int)";

            api.ApiSetId = _db.QueryFirst<int>(query, api);
        }

        public async Task<NativeApiSet> AddApiSetAsync(AdminAddApiSetViewModel model)
        {
            var api = new NativeApiSet()
            {
                ClassName = model.ClassName,
                Name = model.Name
            };

            await AddApiSetAsync(api);
            return api;
        }

        public async Task UpdateApiSetAsync(NativeApiSet api)
        {
            var query = "Update NativeApiSets Set Name = @Name, ClassName = @ClassName Where ApiSetId = @ApiSetId";
            await _db.ExecuteAsync(query, api);
        }

        public async Task<NativeApiSet> UpdateApiSetAsync(AdminEditApiSetViewModel model)
        {
            var api = await GetApiSetByApiIdAsync(model.ApiId);
            
            api.Name = model.Name;
            api.ClassName = model.ClassName;
            await UpdateApiSetAsync(api);

            return api;
        }

        public async Task RemoveApiSetAsync(int id)
        {
            var query = "Delete From NativeApiSets Where ApiSetId = @id";
            await _db.ExecuteAsync(query, new {id});
        }
    }
}