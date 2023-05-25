using System;
using System.Collections.Generic;

namespace TrainingAssignment.Entities.Models;

public partial class User
{
    public long Userid { get; set; }

    public string Username { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Phonenumber { get; set; } = null!;

    public string? Avatar { get; set; }

    public byte? Gender { get; set; }

    public byte? CountryId { get; set; }

    public byte? CityId { get; set; }

    public byte? StateId { get; set; }

    public string? Role { get; set; }

    public virtual City? City { get; set; }

    public virtual Country? Country { get; set; }

    public virtual Gender? GenderNavigation { get; set; }

    public virtual State? State { get; set; }
}
