using Projekt.Validators;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class Actor
    {
        public int ActorId { get; set; }

        [Required(ErrorMessage = "Please enter firstname")]
        [NameValidator]
        [MaxLength(30)]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Please enter lastname")]
        [NameValidator]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter the year of birth")]
        [DateValidator]
        public int YearOfBirth { get; set; }

       // public int cos;
    }
}
