using System.ComponentModel.DataAnnotations;

namespace ViewModels.Docs
{
    public class AdminEditNativeViewModel : AdminAddNativeViewModel
    {
        [Required]
        public int? NativeId { get; set; }
    }
}