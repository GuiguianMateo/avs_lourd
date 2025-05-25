using AVS.DBLib.Class;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AVS.Wpf.ViewModels
{
    public class ViewModelMedicament
    {
        public DBLib.Class.Medicament? SelectedMedicament { get; set; }
        public DBLib.Class.Medicament? NewMedicament { get; set; }
        public ObservableCollection<DBLib.Class.Medicament> Medicaments { get; set; }

        private DBLib.Class.Medicament? _originalSelectedMedicament;

        public event PropertyChangedEventHandler PropertyChanged;

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
            NewMedicament = new DBLib.Class.Medicament(); // Crée un nouvel objet Medicament vide
        }

        public void SaveOriginalSelectedMedicament()
        {
            if (this.SelectedMedicament != null)
            {
                // Sauvegarder les données SelectedMedicament
                _originalSelectedMedicament = new DBLib.Class.Medicament
                {
                    Nom = this.SelectedMedicament.Nom,
                    Peremption = this.SelectedMedicament.Peremption
                };
            }
        }

        public void RestoreOriginalSelectedMedicament()
        {
            if (this.SelectedMedicament != null && _originalSelectedMedicament != null)
            {

                SelectedMedicament.Nom = _originalSelectedMedicament.Nom;
                SelectedMedicament.Peremption = _originalSelectedMedicament.Peremption;

                OnPropertyChanged(nameof(SelectedMedicament));

            }
        }

        internal void CreateMedicament()
        {
            using (AvsContext context = new())
            {
                if (this.NewMedicament == null)
                {
                    this.NewMedicament = new DBLib.Class.Medicament();
                }

                context.Add(this.NewMedicament); // J'ajoute le Medicament au contexte
                context.SaveChanges(); // Je sauvegarde les modifications du contexte en base de données

                this.Medicaments.Add(this.NewMedicament); // Ajouter à la collection pour mise à jour de la vue
                this.SelectedMedicament = this.NewMedicament;

                ResetNewMedicament(); // Réinitialiser après la sauvegarde
            }
        }

        internal void EditMedicament()
        {
            if (SelectedMedicament != null)
            {
                using (AvsContext context = new())
                {
                    context.Update(SelectedMedicament);
                    context.SaveChanges();
                }

                ResetNewMedicament();
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
