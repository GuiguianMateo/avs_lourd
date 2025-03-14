using System;
using System.Collections.Generic;

namespace AVS.DBLib.Class;

public partial class Medicament
{
    public ulong Id { get; set; }

    public string Nom { get; set; } = null!;

    public DateTime Peremption { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
