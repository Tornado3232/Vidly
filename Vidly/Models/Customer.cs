using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidly.Models;

public partial class Customer
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter customer's name")]
    [StringLength(255)]
    public string Name { get; set; }

    public bool IsSubscribedToNewsletter { get; set; }

    [Display(Name = "Membership Type")]
    public byte MembershipTypeId { get; set; }

    [Display (Name = "Date of Birth")]
    [Column(TypeName = "datetime2")]
    [Min18YearsIfAMember]
    public DateTime? Birthdate { get; set; }

    public virtual MembershipType? MembershipType { get; set; } = null!;
}
