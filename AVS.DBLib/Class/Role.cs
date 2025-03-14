using System;
using System.Collections.Generic;

namespace AVS.DBLib.Class;

public partial class Role
{
    public ulong Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Title { get; set; }

    public int? Scope { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AssignedRole> AssignedRoles { get; set; } = new List<AssignedRole>();
}
