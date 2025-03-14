using System;
using System.Collections.Generic;

namespace AVS.DBLib.Class;

public partial class Type
{
    public ulong Id { get; set; }

    public string Nom { get; set; } = null!;

    public int Duree { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();

    public virtual ICollection<Praticien> Praticiens { get; set; } = new List<Praticien>();
}
