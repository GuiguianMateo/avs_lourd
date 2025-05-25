using AVS.DBLib.Class;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AVS.Wpf.ViewModels
{
    public class ViewModelUser
    {
        public DBLib.Class.User? SelectedUser { get; set; }
        public DBLib.Class.User? NewUser { get; set; }
        public ObservableCollection<DBLib.Class.User> Users { get; set; }

        private DBLib.Class.User? _originalSelectedUser;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> Genres { get; set; } = new List<string> { "Masculin", "Feminin", "Autre" };


        public ViewModelUser()
        {
            ResetNewUser();
            using (AvsContext mg = new AvsContext())
            {
                Users = new ObservableCollection<DBLib.Class.User>(mg.Users.ToList()); 
            }

        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ResetNewUser()
        {
            NewUser = new DBLib.Class.User(); // Crée un nouvel objet User vide
        }

        public void SaveOriginalSelectedUser()
        {
            if (this.SelectedUser != null)
            {
                // Sauvegarder les données SelectedUser
                _originalSelectedUser = new DBLib.Class.User
                {
                    Nom = this.SelectedUser.Nom,
                    Prenom = this.SelectedUser.Prenom
                };
            }
        }

        public void RestoreOriginalSelectedUser()
        {
            if (this.SelectedUser != null && _originalSelectedUser != null)
            {

                SelectedUser.Nom = _originalSelectedUser.Nom;
                SelectedUser.Prenom = _originalSelectedUser.Prenom;

                OnPropertyChanged(nameof(SelectedUser));

            }
        }

        internal void EditUser()
        {
            if (SelectedUser != null)
            {
                using (AvsContext context = new())
                {
                    context.Update(SelectedUser);
                    context.SaveChanges();
                }

                ResetNewUser();
            }
        }

        internal void DeleteUser()
        {
            if (SelectedUser != null)
            {
                using (AvsContext context = new())
                {
                      context.Remove(SelectedUser);
                    context.SaveChanges();
                }
                this.Users?.Remove(SelectedUser);
            }
        }
    }
}
