using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using wpfGasStations.Model;

namespace wpfGasStations.Viewmodel
{
    public class ApplicationViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<Card> deck;
        private ObservableCollection<Card> table;
        private ObservableCollection<Card> player1;
        private ObservableCollection<Card> player2;
        private ObservableCollection<Card> player3;
        private ObservableCollection<Card> player4;
        public Card? SelectedCard = null;
        public ApplicationViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
