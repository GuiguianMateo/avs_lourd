using System;
using System.Collections.Generic;

namespace AVS.DBLib.Class;

public partial class Consultation
{
    public ulong Id { get; set; }

    public DateTime DateConsultation { get; set; }

    public bool Retard { get; set; }

    public string Statu { get; set; } = null!;

    public ulong TypeId { get; set; }

    public ulong UserId { get; set; }

    public ulong PraticienId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Praticien Praticien { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual Type Type { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
