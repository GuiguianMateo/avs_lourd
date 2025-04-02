using AVS.DBLib.Class;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AVS.Wpf.ViewModels
{
    public class ViewModelType
    {
        public DBLib.Class.Type? SelectedType { get; set; }
        public DBLib.Class.Type? NewType { get; set; }
        public ObservableCollection<DBLib.Class.Type> Types { get; set; }

        private DBLib.Class.Type? _originalSelectedType;

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ITypeProvider _TypeProvider;

        public ViewModelType(ITypeProvider TypeProvider)
        {
            //ResetNewType();
            //using (AvsContext mg = new AvsContext())
            //{
            //    Types = new ObservableCollection<DBLib.Class.Type>(mg.Types.ToList());
            //}

            _TypeProvider = TypeProvider;

        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ResetNewType()
        {
            NewType = new DBLib.Class.Type(); // Crée un nouvel objet Type vide
        }

        public void SaveOriginalSelectedType()
        {
            if (this.SelectedType != null)
            {
                // Sauvegarder les données SelectedType
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
            using (AvsContext context = new())
            {
                if (this.NewType == null)
                {
                    this.NewType = new DBLib.Class.Type();
                }

                context.Add(this.NewType); // J'ajoute le type au contexte
                context.SaveChanges(); // Je sauvegarde les modifications du contexte en base de données

                this.Types.Add(this.NewType); // Ajouter à la collection pour mise à jour de la vue
                this.SelectedType = this.NewType;

                ResetNewType(); // Réinitialiser après la sauvegarde
            }
        }

        internal void EditType()
        {
            if (SelectedType != null)
            {
                using (AvsContext context = new())
                {
                    context.Update(SelectedType);
                    context.SaveChanges();
                }

                ResetNewType();
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
