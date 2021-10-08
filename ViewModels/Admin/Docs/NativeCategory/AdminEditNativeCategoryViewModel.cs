using System.ComponentModel.DataAnnotations;

namespace ViewModels.Admin.Docs.NativeCategory
{
    public class AdminEditNativeCategoryViewModel : AdminAddNativeCategoryViewModel
    {
        [Required]
        public int CategoryId { get; set; }
    }
}