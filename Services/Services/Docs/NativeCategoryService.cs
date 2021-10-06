using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Models.DocsModels;
using Services.Interfaces.Docs;

namespace Services.Services.Docs
{
    public class NativeCategoryService : INativeCategoryService
    {

        private readonly IDbConnection _db;

        public NativeCategoryService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<List<NativeCategory>> GetAllCategoriesAsync()
        {
            var query = "Select * From NativeCategories";
            return (await _db.QueryAsync<NativeCategory>(query)).ToList();
        }
    }
}