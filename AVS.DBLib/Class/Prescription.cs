using System;
using System.Collections.Generic;

namespace AVS.DBLib.Class;

public partial class Prescription
{
    public ulong Id { get; set; }

    public int Posologie { get; set; }

    public int Quantite { get; set; }

    public int Duree { get; set; }

    public string Detail { get; set; } = null!;

    public ulong ConsultationId { get; set; }

    public ulong MedicamentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Consultation Consultation { get; set; } = null!;

    public virtual Medicament Medicament { get; set; } = null!;
}
