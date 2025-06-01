using System;
using System.Collections.Generic;

namespace AVS.DBLib.Class;

public partial class Medicament
{
    public ulong Id { get; set; }

    public string Nom { get; set; } = null!;

    public string EffetIndesirable { get; set; } = null!;

    public string ModeAdministration { get; set; } = null!;

    public string NiveauAvertissement { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
