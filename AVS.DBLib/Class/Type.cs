using System;
using System.Collections.Generic;

namespace AVS.DBLib.Class;

public partial class Type
{
    public ulong Id { get; set; }

    public string Nom { get; set; } = null!;

    public int Duree { get; set; }

    public string DureeUnit
    {
        get
        {
            int years = Duree / 365;
            int remainingDays = Duree % 365;
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

    public virtual ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();

    public virtual ICollection<Praticien> Praticiens { get; set; } = new List<Praticien>();
}
