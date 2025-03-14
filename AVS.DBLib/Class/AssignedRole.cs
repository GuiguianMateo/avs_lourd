using System;
using System.Collections.Generic;

namespace AVS.DBLib.Class;

public partial class AssignedRole
{
    public ulong Id { get; set; }

    public ulong RoleId { get; set; }

    public ulong EntityId { get; set; }

    public string EntityType { get; set; } = null!;

    public ulong? RestrictedToId { get; set; }

    public string? RestrictedToType { get; set; }

    public int? Scope { get; set; }

    public virtual Role Role { get; set; } = null!;
}
