using System;
using System.Collections.Generic;

namespace TrainingAssignment.Entities.Models;

public partial class Country
{
    public byte CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<State> States { get; set; } = new List<State>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
