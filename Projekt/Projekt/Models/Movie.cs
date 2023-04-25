using Projekt.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Movie : IValidatableObject
    {

        private int _classicYear = 1980;


        public int MovieId { get; set; }
        [Required(ErrorMessage = "Please enter the title")]
        [MaxLength(40)]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Please enter the director")]
        [FullNameValidator]
        [MaxLength(40)]
        public string Director { get; set; }

        [Required(ErrorMessage = "Please enter the genre")]
        [MaxLength(30)]
        public string Genre { get; set; } 

        [Required(ErrorMessage = "Please enter the year")]
        [DateValidator]
        public int YearOfProduction { get; set; }

        [ForeignKey("Producer")]
        [Display(Name = "Producer")]
        public int ProducerId { get; set; }
        public Producer? Producer { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
        
            if (Genre.Equals("classic") && YearOfProduction > _classicYear)
            {
                yield return new ValidationResult(
                    $"Classic movies must have a release year no later than {_classicYear}",
                    new List<String> { nameof(YearOfProduction)}
                    );
            }
        }

    }
}
