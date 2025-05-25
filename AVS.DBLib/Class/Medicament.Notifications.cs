using System.ComponentModel;

namespace AVS.DBLib.Class
{
    public partial class Medicament : INotifyPropertyChanged
    {
        private int _peremption;

        //public int Peremption
        //{
        //    get => _peremption;
        //    set
        //    {
        //        if (_peremption != value)
        //        {
        //            _peremption = value;
        //            // Supprimez la propriété Peremption ici pour éviter l'ambiguïté
        //            // public int Peremption { get; set; } // À retirer si présente

        //            // Gardez uniquement la déclaration de Peremption dans un seul fichier partiel (par exemple Medicament.cs)
        //            OnPropertyChanged(nameof(Peremption));
        //            OnPropertyChanged(nameof(PeremptionUnit));
        //        }
        //    }
        //}

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
