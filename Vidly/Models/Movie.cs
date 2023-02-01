using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidly.Models;

public partial class Movie
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please Enter Movie's Name")]
    [StringLength(255)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please Select Genre")]
    public byte GenreId { get; set; }

    public virtual Genre? Genre { get; set; } = null!;

    [Column(TypeName = "datetime2")]
    public DateTime DateAdded { get; set; }

    [Required(ErrorMessage = "Please Enter Movie's Release Date")]
    [Column(TypeName = "datetime2")]
    public DateTime ReleaseDate { get; set; }

    [Display(Name = "Number In Stock")]
    [Range(1, 20, ErrorMessage = "Value for {0} must be between {1} and {2}")]
    [Required(ErrorMessage = "The Field Number in Stock Must Be Between 1 and 20")]
    public byte NumberInStock { get; set; }

}
