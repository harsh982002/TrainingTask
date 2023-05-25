using System;
using System.Collections.Generic;

namespace TrainingAssignment.Entities.Models;

public partial class City
{
    public byte CityId { get; set; }

    public byte? CountryId { get; set; }

    public string CityName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public byte? StateId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual State? State { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
