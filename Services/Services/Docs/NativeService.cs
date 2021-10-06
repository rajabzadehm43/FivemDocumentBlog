using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Dapper;
using Models.DocsModels;
using Services.Interfaces.Docs;
using ViewModels.Docs;

namespace Services.Services.Docs
{
    public class NativeService : INativeService
    {
        private readonly IDbConnection _db;
        private readonly string _baseNativesImage;

        public NativeService(IDbConnection db)
        {
            _db = db;
        }

        private NativeService()
        {
            _baseNativesImage = Path.Join(Directory.GetCurrentDirectory(), "Images/Natives/");
            if (!Directory.Exists(_baseNativesImage))
                Directory.CreateDirectory(_baseNativesImage);
        }

        public async Task<List<Native>> GetNativesAsync()
        {
            var query = "Select * From Natives";
            return _db.Query<Native>(query).ToList();
        }

        public async Task<List<Native>> GetTopNativesAsync(int take = 10)
        {
            var query = "SELECT Top(@take) * FROM Natives";
            return (await _db.QueryAsync<Native>(query, new {take = take})).ToList();
        }

        public async Task<Native> GetNativeByIdAsync(int id)
        {
            var query = "Select * From Natives Where NativeId = @Id";
            var result = await _db.QueryFirstAsync<Native>(query, new {Id = id});
            return result;
        }

        public async Task<Tuple<List<Native>, int>> GetNativesByPagingAsync(string q = "", int take = 10, int skip = 0)
        {
            var query1 =
                "Select N.*, A.* From Natives AS N Left Join NativeApiSets As A On N.ApiSetId = A.ApiSetId Where N.NativeName LIKE @q Or N.ShortDescription LIKE @q Or N.Description LIKE @q ";

            var finalQuery = query1 + " Order By (Select 1) Offset @skip Rows Fetch Next  @take Rows Only;"
                                    + $"Select Count(*) From (Select N.* From Natives As N Where N.NativeName LIKE @q Or N.ShortDescription LIKE @q Or N.Description LIKE @q) As int";

            var param = new
            {
                q = $"%{q}%",
                skip,
                take
            };

            var finalResult = new Tuple<List<Native>, int>(new List<Native>(), 0);

            var list = _db.QueryMultiple(finalQuery, param);
            var result = list.Read<Native, NativeApiSet, Native>((n, a) =>
            {
                n.ApiSet = a;
                return n;
            }, "ApiSetId").ToList();
            var count = list.ReadFirst<int>();
            finalResult = new Tuple<List<Native>, int>(result, count);

            return finalResult;
        }

        public async Task AddNativeAsync(Native native)
        {
            var query = "Insert Into Natives (ImageName, NativeName, ShortDescription, Description, SampleCode, ApiSetId, CategoryId) " +
                        "VALUES (@ImageName, @NativeName, @ShortDescription, @Description, @SampleCode, @ApiSetId, @CategoryId);"
                + "Select Cast(SCOPE_IDENTITY() As int);";

            var result = await _db.QueryFirstAsync<int>(query, native);

            native.NativeId = result;
        }

        public async Task<Native> AddNativeAsync(AdminAddNativeViewModel model)
        {

            var newNative = new Native
            {
                Description = model.Description,
                ShortDescription = model.ShortDescription,
                NativeName = model.NativeName,
                SampleCode = model.SampleCode,
                ApiSetId = model.ApiSetId.Value,
                CategoryId = model.CategoryId.Value
            };

            if (model.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var fullPath = Path.Join(_baseNativesImage, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                newNative.ImageName = fileName;
            }

            await AddNativeAsync(newNative);
            return newNative;
        }

        public async Task UpdateNativeAsync(Native native)
        {
            var query =
                "Update Natives Set ImageName = @ImageName, NativeName = @NativeName, ShortDescription = @ShortDescription," +
                "Description = @Description, SampleCode = @SampleCode, ApiSetId = @ApiSetId, CategoryId = @CategoryId Where NativeId = @NativeId";

            await _db.ExecuteAsync(query, native);
        }

        public async Task<Native> UpdateNativeAsync(AdminEditNativeViewModel model)
        {

            var native = await GetNativeByIdAsync(model.NativeId.Value);

            // Image Process
            if (model.ImageFile != null)
            {
                // Delete Old File Name ;
                {
                    var oldFullPath = Path.Join(_baseNativesImage, native.ImageName);
                    File.Delete(oldFullPath);
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var fullPath = Path.Join(_baseNativesImage, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                native.ImageName = fileName;
            }

            native.ShortDescription = model.ShortDescription;
            native.Description = model.Description;
            native.NativeName = model.NativeName;
            native.SampleCode = model.SampleCode;
            native.ApiSetId = model.ApiSetId.Value;
            native.CategoryId = model.CategoryId.Value;

            await UpdateNativeAsync(native);
            return native;
        }

        public async Task RemoveNative(int id)
        {
            var query = "Delete From Natives Where NativeId = @NativeId";
            await _db.ExecuteAsync(query, new {NativeId = id});
        }
    }
}