using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Models.DocsModels;

namespace ViewModels.Docs
{
    public class AdminAddNativeViewModel
    {
        public IFormFile ImageFile { get; set; }

        [Required]
        [DisplayName("استفاده")]
        public int? ApiSetId { get; set; }

        [Required]
        [DisplayName("دسته بندی")]
        public int? CategoryId { get; set; }

        [Required]
        [MaxLength(80)]
        [DisplayName("نام تابع (بدون پرانتز و پارامتر ها)")]
        public string NativeName { get; set; }

        [Required]
        [MaxLength(250)]
        [DisplayName("توضیحات کوتاه")]
        public string ShortDescription { get; set; }

        [Required]
        [DisplayName("توضیحات")]
        public string Description { get; set; }

        [DisplayName("کد مثال")]
        public string SampleCode { get; set; }
    }
}