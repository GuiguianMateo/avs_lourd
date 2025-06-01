using AVS.DBLib.Class;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AVS.Wpf.ViewModels
{
    public class ViewModelMedicament
    {
        public DBLib.Class.Medicament? SelectedMedicament { get; set; }
        public DBLib.Class.Medicament? NewMedicament { get; set; }
        public ObservableCollection<DBLib.Class.Medicament> Medicaments { get; set; }

        private DBLib.Class.Medicament? _originalSelectedMedicament;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> NiveauxAvertissement { get; } = new List<string>
        {
            "Niveau 1",
            "Niveau 2",
            "Niveau 3"
        };

        public ViewModelMedicament()
        {
            ResetNewMedicament();
            using (AvsContext mg = new AvsContext())
            {
                Medicaments = new ObservableCollection<DBLib.Class.Medicament>(mg.Medicaments.ToList());
            }

        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ResetNewMedicament()
        {
            NewMedicament = new DBLib.Class.Medicament();
        }

        public void SaveOriginalSelectedMedicament()
        {
            if (this.SelectedMedicament != null)
            {
                _originalSelectedMedicament = new DBLib.Class.Medicament
                {
                    Nom = this.SelectedMedicament.Nom,
                    EffetIndesirable = this.SelectedMedicament.EffetIndesirable,
                    ModeAdministration = this.SelectedMedicament.ModeAdministration,
                    NiveauAvertissement = this.SelectedMedicament.NiveauAvertissement,
                };
            }
        }

        public void RestoreOriginalSelectedMedicament()
        {
            if (this.SelectedMedicament != null && _originalSelectedMedicament != null)
            {

                SelectedMedicament.Nom = _originalSelectedMedicament.Nom;
                SelectedMedicament.EffetIndesirable = _originalSelectedMedicament.EffetIndesirable;
                SelectedMedicament.ModeAdministration = _originalSelectedMedicament.ModeAdministration;
                SelectedMedicament.NiveauAvertissement = _originalSelectedMedicament.NiveauAvertissement;

                OnPropertyChanged(nameof(SelectedMedicament));

            }
        }

        internal void CreateMedicament()
        {
            // Validation des champs obligatoires
            if (string.IsNullOrWhiteSpace(this.NewMedicament?.Nom) ||
                string.IsNullOrWhiteSpace(this.NewMedicament?.ModeAdministration) ||
                string.IsNullOrWhiteSpace(this.NewMedicament?.EffetIndesirable) ||
                string.IsNullOrWhiteSpace(this.NewMedicament?.NiveauAvertissement))
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                using (AvsContext context = new())
                {
                    if (this.NewMedicament == null)
                    {
                        this.NewMedicament = new DBLib.Class.Medicament();
                    }

                    context.Add(this.NewMedicament);
                    context.SaveChanges();

                    this.Medicaments.Add(this.NewMedicament);
                    this.SelectedMedicament = this.NewMedicament;

                    ResetNewMedicament();
                }
            } 
        }

        internal void EditMedicament()
        {
            if (string.IsNullOrEmpty(this.SelectedMedicament?.Nom) ||
                string.IsNullOrEmpty(this.SelectedMedicament?.ModeAdministration) ||
                string.IsNullOrEmpty(this.SelectedMedicament?.EffetIndesirable) ||
                string.IsNullOrEmpty(this.SelectedMedicament?.NiveauAvertissement))
            {
                this.RestoreOriginalSelectedMedicament();
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                using (AvsContext context = new())
                {
                    context.Update(SelectedMedicament);
                    context.SaveChanges();

                    ResetNewMedicament();
                }
            }
        }

        internal void DeleteMedicament()
        {
            if (SelectedMedicament != null)
            {
                using (AvsContext context = new())
                {
                      context.Remove(SelectedMedicament);
                    context.SaveChanges();
                }
                this.Medicaments?.Remove(SelectedMedicament);
            }
        }
    }
}
