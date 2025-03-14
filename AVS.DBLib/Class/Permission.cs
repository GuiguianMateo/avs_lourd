using System;
using System.Collections.Generic;

namespace AVS.DBLib.Class;

public partial class Permission
{
    public ulong Id { get; set; }

    public ulong AbilityId { get; set; }

    public ulong? EntityId { get; set; }

    public string? EntityType { get; set; }

    public bool Forbidden { get; set; }

    public int? Scope { get; set; }

    public virtual Ability Ability { get; set; } = null!;
}
