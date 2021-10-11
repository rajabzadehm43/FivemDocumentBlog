using System.ComponentModel.DataAnnotations;

namespace Models.GeneralModels
{
    public class Menu
    {

        [Key]
        public int MenuId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(300)]
        public string TargetUrl { get; set; }

        [MaxLength(30)]
        public string Target { get; set; }

        [MaxLength(30)]
        public string Rel { get; set; }

    }
}