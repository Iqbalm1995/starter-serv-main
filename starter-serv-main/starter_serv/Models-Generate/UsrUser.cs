using System;
using System.Collections.Generic;

namespace starter_serv.Models-Generate;

public partial class UsrUser
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Age { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public char StatusUser { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public char IsDeleted { get; set; }
}
