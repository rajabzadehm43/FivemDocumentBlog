using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Admin.Docs.NativeApiSet
{
    public class AdminAddApiSetViewModel
    {
        [Required]
        [MaxLength(50)]
        [DisplayName("نام Api")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("کلاس Api")]
        public string ClassName { get; set; }
    }
}