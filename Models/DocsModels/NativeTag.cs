using System.ComponentModel.DataAnnotations;

namespace Models.DocsModels
{
    public class NativeTag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Tag { get; set; }

        [Required]
        public int NativeId { get; set; }

        #region Relations

        public Native Native { get; set; }

        #endregion

    }
}