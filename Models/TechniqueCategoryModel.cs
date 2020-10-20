using System.ComponentModel.DataAnnotations;

namespace Peitho.Models
{
    public class TechniqueCategoryModel
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression(ModelValidationRegexes.ValidAlphabetic, ErrorMessage = "Must only contain alphabetical characters and spaces.")]
        public string Name { get; set; }

        [MaxLength(50)]
        [RegularExpression(ModelValidationRegexes.ValidHangeul, ErrorMessage = "Must only contain hangeul vowels and space.")]
        public string NameHangeul { get; set; }

        [MaxLength(50)]
        [RegularExpression(ModelValidationRegexes.ValidHanja, ErrorMessage = "Must only contain hanja characters and spaces.")]
        public string NameHanja { get; set; }
    }
}