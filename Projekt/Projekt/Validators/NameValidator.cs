using Azure.Core;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Validators
{
    public class NameValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var content = Convert.ToString(value);
            var upper = content.ToUpper();
            if (content[0] != upper[0])
            {
                return new ValidationResult("First name nad Last name must start with capital letter");
            }
            else
            {
                return null;
            }
        }
    }
}
