using AVS.DBLib.Class;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AVS.Wpf.ViewModels
{
    public class ViewModelType
    {
        public DBLib.Class.Type? SelectedType { get; set; }
        public DBLib.Class.Type? NewType { get; set; }
        public ObservableCollection<DBLib.Class.Type> Types { get; set; }

        private DBLib.Class.Type? _originalSelectedType;

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelType()
        {
            ResetNewType();
            using (AvsContext mg = new AvsContext())
            {
                Types = new ObservableCollection<DBLib.Class.Type>(mg.Types.ToList());
            }

        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ResetNewType()
        {
            NewType = new DBLib.Class.Type();
        }

        public void SaveOriginalSelectedType()
        {
            if (this.SelectedType != null)
            {
                _originalSelectedType = new DBLib.Class.Type
                {
                    Nom = this.SelectedType.Nom,
                    Duree = this.SelectedType.Duree
                };
            }
        }

        public void RestoreOriginalSelectedType()
        {
            if (this.SelectedType != null && _originalSelectedType != null)
            {

                SelectedType.Nom = _originalSelectedType.Nom;
                SelectedType.Duree = _originalSelectedType.Duree;

                OnPropertyChanged(nameof(SelectedType));

            }
        }

        internal void CreateType()
        {
            if (string.IsNullOrWhiteSpace(this.NewType?.Nom) ||
                this.NewType?.Duree == null || this.NewType.Duree < 0)
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (AvsContext context = new())
            {
                if (this.NewType == null)
                {
                    this.NewType = new DBLib.Class.Type();
                }
                else
                {   
                    context.Add(this.NewType);
                    context.SaveChanges();

                    this.Types.Add(this.NewType);
                    this.SelectedType = this.NewType;

                    ResetNewType();
                }

            }
        }

        internal void EditType()
        {
            if (string.IsNullOrEmpty(this.SelectedType?.Nom) ||
                this.SelectedType?.Duree == null || this.SelectedType?.Duree < 0)
            {
                this.RestoreOriginalSelectedType();
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                using (AvsContext context = new())
                {
                    context.Update(SelectedType);
                    context.SaveChanges();

                    ResetNewType();
                }
            }
        }

        internal void DeleteType()
        {
            if (SelectedType != null)
            {
                using (AvsContext context = new())
                {
                      context.Remove(SelectedType);
                    context.SaveChanges();
                }
                this.Types?.Remove(SelectedType);
            }
        }
    }
}
