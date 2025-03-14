using System;
using System.Collections.Generic;

namespace AVS.DBLib.Class;

public partial class Ability
{
    public ulong Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Title { get; set; }

    public ulong? EntityId { get; set; }

    public string? EntityType { get; set; }

    public bool OnlyOwned { get; set; }

    public string? Options { get; set; }

    public int? Scope { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
