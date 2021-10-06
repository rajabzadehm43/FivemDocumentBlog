using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Models.DocsModels;
using Services.Interfaces.Docs;

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
    }
}