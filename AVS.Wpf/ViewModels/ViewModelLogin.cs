using System.Windows;
using System.Windows.Data;
using AVS.Wpf;
using AVS.DBLib.Class;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace AVS.Wpf
{
    internal class ViewModelLogin
    {
        #region Fields

        public string? Email { get; set; }
        public string? Password { get; set; }

        #endregion

        public void Login()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (AvsContext context = new AvsContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(Email));

                if (user != null)
                {
                    //var isAdmin = context.AssignedRoles.Include(ar => ar.Role).Any(ar => ar.EntityId == user.Id && ar.EntityType == "User" && ar.Role.Name == "admin");
                    //var isPraticien = context.AssignedRoles.Include(ar => ar.Role).Any(ar => ar.EntityId == user.Id && ar.EntityType == "User" && ar.Role.Name == "praticien");

                    var isAdmin = context.AssignedRoles.Include(ar => ar.Role).Any(ar => ar.EntityId == user.Id && ar.Role.Name == "admin");
                    var isPraticien = context.AssignedRoles.Include(ar => ar.Role).Any(ar => ar.EntityId == user.Id && ar.Role.Name == "praticien");

                    if (isAdmin || isPraticien)
                    {
                        if (BCrypt.Net.BCrypt.Verify(Password, user.Password))
                        {
                            string roleInfo = isAdmin ? "admin" : "praticien";

                            ((App)System.Windows.Application.Current).Login(user);
                        }
                        else
                        {
                            MessageBox.Show("Mot de passe incorrect", "Erreur de connexion", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vous n'avez pas les autorisations nécessaires pour accéder à cette application", "Erreur de connexion", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Adresse mail introuvable", "Erreur de connexion", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}