using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Models.DocsModels;

namespace ViewModels.Docs
{
    public class AdminAddNativeViewModel
    {
        public IFormFile ImageFile { get; set; }

        [Required]
        public int? ApiSetId { get; set; }

        [Required]
        public int? CategoryId { get; set; }

        [Required]
        [MaxLength(80)]
        public string NativeName { get; set; }

        [Required]
        [MaxLength(250)]
        public string ShortDescription { get; set; }

        [Required]
        public string Description { get; set; }

        public string SampleCode { get; set; }
    }
}