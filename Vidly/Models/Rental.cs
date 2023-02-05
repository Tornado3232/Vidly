using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public Movie Movie { get; set; }

        [Display(Name = "Date Rented")]
        [Column(TypeName = "datetime2")]
        public DateTime? DateRented { get; set; }
        
        
        [Display(Name = "Date Returned")]
        [Column(TypeName = "datetime2")]
        public DateTime? DateReturned { get; set; }
    }
}
