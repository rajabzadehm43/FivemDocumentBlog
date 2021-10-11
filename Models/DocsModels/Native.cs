using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.DataModels;

namespace Models.DocsModels
{
    public class Native
    {
        [Key]
        public int NativeId { get; set; }

        [Required]
        public int ApiSetId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(450)]
        public string AuthorId { get; set; }

        [MaxLength(50)]
        public string ImageName { get; set; }

        [Required]
        [MaxLength(80)]
        public string NativeName { get; set; }

        [Required]
        [MaxLength(250)]
        public string ShortDescription { get; set; }

        [Required]
        public string Description { get; set; }

        public string SampleCode { get; set; }

        #region Relations

        public NativeApiSet ApiSet { get; set; }
        
        public NativeCategory Category { get; set; }

        public List<NativeTag> Tags { get; set; }

        public AppUser Author { get; set; }

        #endregion

    }
}