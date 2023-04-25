using Projekt.Data;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Validators
{
    public class DateValidator : ValidationAttribute
    {
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var givenYear = Convert.ToInt32(value);
            var currentYear = DateTime.Today;

            if(givenYear > currentYear.Year)
            {
                return new ValidationResult("Given year didnt even started");
            }
            else
            {
                return null;
            }
        }
    }
}
