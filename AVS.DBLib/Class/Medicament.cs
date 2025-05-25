using System;
using System.Collections.Generic;

namespace AVS.DBLib.Class;

public partial class Medicament
{
    public ulong Id { get; set; }

    public string Nom { get; set; } = null!;

    public int Peremption { get; set; }

    public string PeremptionUnit
    {
        get
        {
            int years = Peremption / 365;
            int remainingDays = Peremption % 365;
            int months = remainingDays / 30;
            int days = remainingDays % 30;

            // Crée une chaîne descriptive
            var parts = new List<string>();
            if (years > 0) parts.Add($"{years} an{(years > 1 ? "s" : "")}");
            if (months > 0) parts.Add($"{months} mois");
            if (days > 0) parts.Add($"{days} jour{(days > 1 ? "s" : "")}");

            return string.Join(" et ", parts);
        }
    }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
