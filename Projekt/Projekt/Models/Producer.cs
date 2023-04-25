using Projekt.Data;
using Projekt.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class Producer
    {
        public Producer() { 
            MoviesProduced = new HashSet<Movie>();
        }
        
        public int ProducerId { get; set; }
        [Required(ErrorMessage ="Please enter the name of producer")]
        [MaxLength(25)]
        public String Name { get; set; }
        [Required(ErrorMessage ="Please enter the country")]
        [MaxLength(35)]
        public String Country { get; set; }
        [Required(ErrorMessage = "Please enter the year of foundations")]
        [DateValidator]
        public int YearOfFoundation { get; set; }

        public IEnumerable<Movie> MoviesProduced { get; set; }
    }
}
