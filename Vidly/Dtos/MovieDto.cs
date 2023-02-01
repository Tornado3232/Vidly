using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public byte GenreId { get; set; }

        public GenreDto Genre { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateAdded { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ReleaseDate { get; set; }

        public byte NumberInStock { get; set; }


    }
}
