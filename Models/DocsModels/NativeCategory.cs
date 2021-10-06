using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.DocsModels
{
    public class NativeCategory
    {

        public NativeCategory()
        {
            ClassName = "5d-badge-warning";
        }

        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClassName { get; set; }

        #region Relations

        public List<Native> Natives { get; set; }

        #endregion

    }
}