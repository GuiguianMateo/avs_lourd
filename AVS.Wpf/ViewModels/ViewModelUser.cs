using AVS.DBLib.Class;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AVS.Wpf.ViewModels
{
    public class ViewModelUser
    {
        private DBLib.Class.User? _selectedUser;

        public DBLib.Class.User? SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged();
                }
            }
        }

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
            NewUser = new DBLib.Class.User();
        }

        public void SaveOriginalSelectedUser()
        {
            if (this.SelectedUser != null)
            {
                _originalSelectedUser = new DBLib.Class.User
                {
                    Nom = this.SelectedUser.Nom,
                    Prenom = this.SelectedUser.Prenom,
                    Email = this.SelectedUser.Email,
                    Genre = this.SelectedUser.Genre,
                    Naissance = this.SelectedUser.Naissance
                };
            }
        }

        public void RestoreOriginalSelectedUser()
        {
            if (this.SelectedUser != null && _originalSelectedUser != null)
            {
                SelectedUser.Nom = _originalSelectedUser.Nom;
                SelectedUser.Prenom = _originalSelectedUser.Prenom;
                SelectedUser.Email = _originalSelectedUser.Email;
                SelectedUser.Genre = _originalSelectedUser.Genre;
                SelectedUser.Naissance = _originalSelectedUser.Naissance;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        internal void EditUser()
        {
            if (string.IsNullOrWhiteSpace(this.SelectedUser?.Nom) ||
                string.IsNullOrWhiteSpace(this.SelectedUser?.Prenom) ||
                string.IsNullOrWhiteSpace(this.SelectedUser?.Email) ||
                this.SelectedUser?.Genre == null ||
                this.SelectedUser?.Naissance == null ||
                this.SelectedUser.Naissance.Equals(default(DateOnly)))
            {
                this.RestoreOriginalSelectedUser();
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
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
