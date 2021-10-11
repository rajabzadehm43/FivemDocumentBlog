using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Admin.Menus
{
    public class AdminAddMenuViewModel
    {
        [Required]
        [MaxLength(50)]
        [DisplayName("تیتر منو")]
        public string Title { get; set; }

        [Required]
        [MaxLength(300)]
        [DisplayName("لینک منو")]
        public string TargetUrl { get; set; }

        [MaxLength(30)]
        public string Target { get; set; }

        [MaxLength(30)]
        public string Rel { get; set; }
    }
}