using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Models.DocsModels;
using Services.Interfaces.Docs;
using ViewModels.Admin.Docs.NativeCategory;

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

        public async Task<NativeCategory> GetCategoryByIdAsync(int id)
        {
            var query = "Select * From NativeCategories Where CategoryId = @id";
            return _db.QueryFirst<NativeCategory>(query, new {id});
        }

        public async Task AddCategoryAsync(NativeCategory category)
        {
            var query = "Insert Into NativeCategories (CategoryName, ClassName) Values (@CategoryName, @ClassName);"
                + "Select Cast(SCOPE_IDENTITY() As int)";

            category.CategoryId = _db.QueryFirst<int>(query, category);
        }

        public async Task<NativeCategory> AddCategoryAsync(AdminAddNativeCategoryViewModel model)
        {
            var category = new NativeCategory
            {
                ClassName = model.ClassName,
                CategoryName = model.Name
            };

            await AddCategoryAsync(category);
            return category;
        }

        public async Task UpdateCategoryAsync(NativeCategory category)
        {
            var query = "Update NativeCategories Set CategoryName = @CategoryName, ClassName = @ClassName Where CategoryId = @CategoryId";
            await _db.ExecuteAsync(query, category);
        }

        public async Task<NativeCategory> UpdateCategoryAsync(AdminEditNativeCategoryViewModel model)
        {
            var category = await GetCategoryByIdAsync(model.CategoryId);
            category.ClassName = model.ClassName;
            category.CategoryName = model.Name;

            await UpdateCategoryAsync(category);
            return category;
        }

        public async Task RemoveCategoryByIdAsync(int id)
        {
            var query = "Delete From NativeCategories Where CategoryId = @id";
            await _db.ExecuteAsync(query, new {id});
        }
    }
}