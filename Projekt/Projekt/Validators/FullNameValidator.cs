using System.ComponentModel.DataAnnotations;

namespace Projekt.Validators
{
    public class FullNameValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var content = Convert.ToString(value);
            string[] names = content.Split(' ');
            var length= names.Length;
            if (length < 2 ) 
            {
                return new ValidationResult("This field must contain first name and last name");
            }
            else if (length == 2) 
            {
                if (Char.IsUpper(names[0], 0) && Char.IsUpper(names[1], 0))
                {
                    return null;
                }
            }
            return new ValidationResult("First name and last name have to start with capital letter");
            
        }
    }
}
