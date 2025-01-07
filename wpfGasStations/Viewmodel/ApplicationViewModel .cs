﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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
        public BitmapImage CardsMapImage;
        public ImageSource ImageBackCard { get; set; }
        public ImageSource ImageBackGround { get; set; }
        public ApplicationViewModel(IViewModelBuilder viewModelBuilder)
        {
            path = Path.GetFullPath(".");
            Uri uri = new Uri(path+"\\resources\\card.jpg",UriKind.Absolute);
            ImageBackCard=new BitmapImage(uri);
            ImageBackGround=new BitmapImage(new Uri(path+"\\resources\\70.jpg"));
            List<ObservableCollection<Card>> observableCollections = new(viewModelBuilder.GetDecks());
            deck=observableCollections.Last();
            tableDeck1=observableCollections.First(x => x.Count==1);
            tableDeck2=observableCollections.First(x => x.Count==2);
            tableDeck3=observableCollections.First(x => x.Count==3);
            tableDeck4=observableCollections.First(x => x.Count==4);
            tableDeck5=observableCollections.First(x => x.Count==5);
            tableDeck6=observableCollections.First(x => x.Count==6);
            tableDeck7=observableCollections.First(x => x.Count==7);
            Uri uri2 = new Uri(path+"\\resources\\521.png", UriKind.Absolute);
            CardsMapImage = new(uri2);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
