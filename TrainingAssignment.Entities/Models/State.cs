using System;
using System.Collections.Generic;

namespace TrainingAssignment.Entities.Models;

public partial class State
{
    public byte StateId { get; set; }

    public string Statename { get; set; } = null!;

    public byte? CountryId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual Country? Country { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
