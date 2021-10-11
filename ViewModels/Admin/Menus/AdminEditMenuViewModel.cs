using System.ComponentModel.DataAnnotations;

namespace ViewModels.Admin.Menus
{
    public class AdminEditMenuViewModel : AdminAddMenuViewModel
    {
        [Required]
        public int MenuId { get; set; }
    }
}