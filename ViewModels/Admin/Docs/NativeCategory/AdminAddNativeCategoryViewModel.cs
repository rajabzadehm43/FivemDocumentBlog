using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Admin.Docs.NativeCategory
{
    public class AdminAddNativeCategoryViewModel
    {
        [Required]
        [DisplayName("نام دسته")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [DisplayName("کلاس دسته")]
        [MaxLength(50)]
        public string ClassName { get; set; }
    }
}