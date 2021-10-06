using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.DocsModels
{
    public class NativeApiSet
    {

        public NativeApiSet()
        {
            ClassName = "5d-badge-success";
        }

        [Key]
        public int ApiSetId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClassName { get; set; }

        #region Relations

        public List<Native> Natives { get; set; }

        #endregion

    }
}