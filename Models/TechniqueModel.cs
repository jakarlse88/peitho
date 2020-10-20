using System.ComponentModel.DataAnnotations;

namespace Peitho.Models
{
    public class TechniqueModel
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string Name { get; set; }
        
        [MaxLength(50)]
        [RegularExpression(@"^[\p{IsHangulSyllables} ]*$")]
        public string NameHanja { get; set; }
        
        [MaxLength(50)]
        [RegularExpression(@"^[\p{IsCJKUnifiedIdeographs} ]*$")]
        public string NameHangeul { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string TechniqueType { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string TechniqueCategory { get; set; }
    }
}
