using System;
using System.Collections.Generic;

namespace TrainingAssignment.Entities.Models;

public partial class Gender
{
    public byte GenderId { get; set; }

    public string Gender1 { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
