using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace AVS.DBLib.Class;

public partial class Type : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public ulong Id { get; set; }

    public string Nom { get; set; } = null!;

    private int _duree;
    private string? _dureeUnitCache;

    public int Duree
    {
        get => _duree;
        set
        {
            if (_duree != value)
            {
                _duree = value;
                _dureeUnitCache = null; // Invalide le cache
                OnPropertyChanged(nameof(Duree));
                OnPropertyChanged(nameof(DureeUnit));
            }
        }
    }
    public string DureeUnit
    {
        get
        {
            if (_dureeUnitCache == null)
            {
                int years = Duree / 365;
                int remainingDays = Duree % 365;
                int months = remainingDays / 30;
                int days = remainingDays % 30;

                var parts = new List<string>();
                if (years > 0) parts.Add($"{years} an{(years > 1 ? "s" : "")}");
                if (months > 0) parts.Add($"{months} mois");
                if (days > 0) parts.Add($"{days} jour{(days > 1 ? "s" : "")}");

                _dureeUnitCache = parts.Count > 0 ? string.Join(" et ", parts) : "0 jour";
            }
            return _dureeUnitCache;
        }
    }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();

    public virtual ICollection<Praticien> Praticiens { get; set; } = new List<Praticien>();
}
