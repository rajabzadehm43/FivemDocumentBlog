using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using DataLayer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;
using Models.DocsModels;
using Services.Interfaces.Docs;
using ViewModels.Docs;

namespace Services.Services.Docs
{
    public class NativeService : INativeService
    {
        private readonly IDbConnection _db;
        private readonly string _baseNativesImage;
        private readonly string _baseNativeDescImage;

        private readonly INativeTagService _tagService;

        private readonly ApplicationDbContext _context;

        public NativeService(IDbConnection db, INativeTagService tagService, ApplicationDbContext context) : this()
        {
            _db = db;
            _tagService = tagService;
            _context = context;
        }

        private NativeService()
        {
            _baseNativesImage = Path.Join(Directory.GetCurrentDirectory(), "wwwroot/Images/Natives/");
            _baseNativeDescImage = Path.Join(Directory.GetCurrentDirectory(), "wwwroot/Images/Natives/Description");
            
            if (!Directory.Exists(_baseNativesImage))
                Directory.CreateDirectory(_baseNativesImage);

            if (!Directory.Exists(_baseNativeDescImage))
                Directory.CreateDirectory(_baseNativeDescImage);

        }

        public async Task<List<Native>> GetNativesWithoutRelAsync()
        {
            var query = "Select * From Natives";

            return _db.Query<Native>(query).ToList();
        }

        public async Task<List<Native>> GetNativesAsync(string q = "")
        {
            var query = "Select * From Natives Where NativeName Like '%'+@q+'%' Order By NativeName;" +
                        "Select N.* From NativeTags As T Left Join Natives As " +
                        "N On N.NativeId = T.NativeId Where T.Tag = @q Order By N.NativeName";


            List<Native> final = new List<Native>();
            using (var result = _db.QueryMultiple(query, new { q }))
            {
                final.AddRange(result.Read<Native>());
                final.AddRange(result.Read<Native>());
            }

            return final.Distinct().ToList();
        }

        public async Task<List<Native>> GetTopNativesAsync(int take = 10)
        {
            var query = "SELECT Top(@take) * FROM Natives";
            return (await _db.QueryAsync<Native>(query, new { take = take })).ToList();
        }

        public async Task<Native> GetNativeByIdAsync(int id)
        {
            var query = "Select * From Natives Where NativeId = @Id";
            var result = await _db.QueryFirstAsync<Native>(query, new { Id = id });
            return result;
        }

        public async Task<Native> GetNativeWithAllRelationsById(int id)
        {
            var query =
                "Select N.*, T.*, A.*, C.*, U.* From Natives As N " +
                "Left Join NativeTags As T On N.NativeId = T.NativeId " +
                "Left Join NativeApiSets As A On N.ApiSetId = A.ApiSetId " +
                "Left Join NativeCategories As C On N.CategoryId = C.CategoryId " +
                "Left Join AspNetUsers As U On N.AuthorId = U.Id " +
                "Where N.NativeId = @id";

            var nativeDict = new Dictionary<int, Native>();

            var result = _db.Query<Native, NativeTag, NativeApiSet,
                NativeCategory, AppUser, Native>(query,param:new {id}, map: (native, tag, api, cat, author) =>
            {

                if (!nativeDict.TryGetValue(native.NativeId, out var cNative))
                {
                    cNative = native;
                    cNative.Tags = new List<NativeTag>();
                    nativeDict.Add(native.NativeId, cNative);
                }

                cNative.ApiSet = api;
                cNative.Category = cat;
                cNative.Author = author;
                cNative.Tags.Add(tag);

                return cNative;
            }, splitOn:"ApiSetId,CategoryId,Id");

            return result.FirstOrDefault();
            /*return _context.Natives
                .Include(n => n.ApiSet)
                .Include(n => n.Category)
                .SingleOrDefault(n => n.NativeId == id);*/
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
            var query = "Insert Into Natives (ImageName, NativeName, ShortDescription, Description, SampleCode, ApiSetId, CategoryId, AuthorId) " +
                        "VALUES (@ImageName, @NativeName, @ShortDescription, @Description, @SampleCode, @ApiSetId, @CategoryId, @AuthorId);"
                + "Select Cast(SCOPE_IDENTITY() As int);";

            var result = await _db.QueryFirstAsync<int>(query, native);

            native.NativeId = result;
        }

        public async Task<Native> AddNativeAsync(AdminAddNativeViewModel model, string authorId)
        {

            // Create New Native
            var newNative = new Native
            {
                Description = model.Description,
                ShortDescription = model.ShortDescription,
                NativeName = model.NativeName,
                SampleCode = model.SampleCode,
                ApiSetId = model.ApiSetId.Value,
                CategoryId = model.CategoryId.Value,
                AuthorId = authorId
            };

            // Image Process [Currently Not Have Image]
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

            // Process Tags
            var tags = model.Tags.Split("-")
                .Select(t => new NativeTag
                {
                    Tag = t.Trim(),
                    Native = newNative
                });

            // Saving Data
            await AddNativeAsync(newNative);
            await _tagService.AddTagsAsync(tags.ToList());

            return newNative;
        }

        public async Task<Tuple<string, string>> SaveNativeDescriptionFile(IFormFile file)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fullPath = Path.Join(_baseNativeDescImage, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new Tuple<string, string>($"/Images/Natives/Description/{fileName}", fileName);
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

            // Load Native
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

            // Update Native
            native.ShortDescription = model.ShortDescription;
            native.Description = model.Description;
            native.NativeName = model.NativeName;
            native.SampleCode = model.SampleCode;
            native.ApiSetId = model.ApiSetId.Value;
            native.CategoryId = model.CategoryId.Value;

            // Process Tags
            var tags = model.Tags.Split("-")
                .Select(c => new NativeTag
                {
                    Tag = c.Trim(),
                    NativeId = native.NativeId,
                });

            await UpdateNativeAsync(native);

            await _tagService.RemoveTagsByNativeId(native.NativeId);
            await _tagService.AddTagsAsync(tags.ToList());

            return native;
        }

        public async Task RemoveNative(int id)
        {
            var query = "Delete From NativeTags Where NativeId = @id;" + 
                "Delete From Natives Where NativeId = @id;";
            await _db.ExecuteAsync(query, new { id });
        }
    }
}