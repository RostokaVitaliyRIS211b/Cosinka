using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace wpfGasStations
{
    public class ApplicationViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Card> deck;
        public ObservableCollection<Card> ace1;
        public ObservableCollection<Card> ace2;
        public ObservableCollection<Card> ace3;
        public ObservableCollection<Card> ace4;
        public ObservableCollection<Card> tableDeck1;
        public ObservableCollection<Card> tableDeck2;
        public ObservableCollection<Card> tableDeck3;
        public ObservableCollection<Card> tableDeck4;
        public ObservableCollection<Card> tableDeck5;
        public ObservableCollection<Card> tableDeck6;
        public ObservableCollection<Card> tableDeck7;
        public Card? SelectedCard = null;
        public string path;
        public ImageSource imageBackCard { get; set; }
        public ImageSource imageBackGround { get; set; }
        public ApplicationViewModel()
        {
            path = Path.GetFullPath(".");
            Uri uri = new Uri(path+"\\resources\\card.jpg",UriKind.Absolute);
            imageBackCard=new BitmapImage(uri);
            imageBackGround=new BitmapImage(new Uri(path+"\\resources\\70.jpg"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
