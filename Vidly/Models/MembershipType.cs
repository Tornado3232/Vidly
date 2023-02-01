using System;
using System.Collections.Generic;

namespace Vidly.Models;

public partial class MembershipType
{
    public byte Id { get; set; }

    public short SignUpFee { get; set; }

    public byte DurationInMonths { get; set; }

    public byte DiscountRate { get; set; }

    public string Name { get; set; }

    //public virtual ICollection<Customer> Customers { get; } = new List<Customer>();


    public static readonly byte Unknown = 0;
    public static readonly byte PayAsYouGo = 1;
}
