using System.ComponentModel.DataAnnotations;

namespace Peitho.Models
{
    public class TechniqueTypeModel
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string Name { get; set; }
    }
}