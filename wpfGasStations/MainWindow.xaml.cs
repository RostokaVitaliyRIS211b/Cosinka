﻿using Cosinka.Model;
using Cosinka.Viewmodel;
using Cosinka.Viewmodel.Interfaces;
using Cosinka.Viewmodel.Realizations;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpfCosinka
{

    public partial class MainWindow : Window
    {
        public ApplicationViewModel Myapp { get; set; }
        public IGetImageOfCard GetImageCard { get; set; }
        public IGetNextCard nextCard { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<ApplicationViewModel>();
            serviceCollection.AddSingleton<IViewModelBuilder, ViewModelBuilder>();
            serviceCollection.AddSingleton<IGetNextCard, GetNextCard>();
            serviceCollection.AddSingleton<IGetImageOfCard, GetImageOfCard>();
            serviceCollection.AddSingleton<IGenerateDecksInterface, GenerateDecksInteface>();
            serviceCollection.AddSingleton<MainWindow>(this);
            serviceCollection.AddScoped<ISetHandlerClick, SetHandler>();
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            Myapp=serviceProvider.GetRequiredService<ApplicationViewModel>();
            DataContext=Myapp;
            nextCard=serviceProvider.GetRequiredService<IGetNextCard>();
            GetImageCard=serviceProvider.GetRequiredService<IGetImageOfCard>();
            IGenerateDecksInterface generateDecksInterface = serviceProvider.GetRequiredService<IGenerateDecksInterface>();
            generateDecksInterface.GenerateInterface(this);
            ISetHandlerClick setHandler = serviceProvider.GetRequiredService<ISetHandlerClick>();
            setHandler.SetHandler(currenttable1Deck.Children.OfType<Button>());
            setHandler.SetHandler(currenttable2Deck.Children.OfType<Button>());
            setHandler.SetHandler(currenttable3Deck.Children.OfType<Button>());
            setHandler.SetHandler(currenttable4Deck.Children.OfType<Button>());
            setHandler.SetHandler(currenttable5Deck.Children.OfType<Button>());
            setHandler.SetHandler(currenttable6Deck.Children.OfType<Button>());
            setHandler.SetHandler(currenttable7Deck.Children.OfType<Button>());
        }

        private void cardDeck_Click(object sender, RoutedEventArgs e)
        {
            Card card = nextCard.GetNextCard();
            MyCardButton button = new(card, true);
            button.Height=100;
            button.Width=67;
            button.Click+=card_Click;
            button.Content=GetImageCard.GetImage(card);
            if (currentCardDeck.Children.Count>0)
                currentCardDeck.Children.Clear();
            currentCardDeck.Children.Add(button);
        }
        private void card_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MyCardButton button)
            {
                (ObservableCollection<Card>, Canvas) pos = Position(button);
                if (button.IsOpen && pos.Item2!=currentAce1Deck && pos.Item2!=currentAce2Deck && pos.Item2!=currentAce3Deck && pos.Item2!=currentAce1Deck)
                {
                    #region Aces
                    if (button.Card.Rank==CardRank.Ace)
                    {
                        pos.Item2.Children.Remove(button);
                        Canvas.SetTop(button, 0);
                        if (button.Card.Suit==CardSuit.Heart)
                        {
                            currentAce1Deck.Children.Add(button);
                            Myapp.ace1.Add(button.Card);
                        }
                        else if (button.Card.Suit==CardSuit.Diamond)
                        {
                            currentAce2Deck.Children.Add(button);
                            Myapp.ace2.Add(button.Card);
                        }
                        else if (button.Card.Suit==CardSuit.Club)
                        {
                            currentAce3Deck.Children.Add(button);
                            Myapp.ace3.Add(button.Card);
                        }
                        else
                        {
                            currentAce4Deck.Children.Add(button);
                            Myapp.ace4.Add(button.Card);
                        }
                        pos.Item1.Remove(button.Card);
                        OpenCard(pos.Item2.Children);
                    }
                    #endregion
                    #region Kings
                    else if (button.Card.Rank==CardRank.King)
                    {
                        bool IsIAce = TryToAce(button, pos);
                        if(!IsIAce)
                        {
                            for(int i=0;i<Myapp.tableDecks.Count;++i)
                            {
                                if (Myapp.tableDecks[i].Count==0)
                                {
                                    pos.Item2.Children.Remove(button);
                                    pos.Item1.Remove(button.Card);
                                    Myapp.tableDecks[i].Add(button.Card);
                                    Canvas canvas = allTableDecks.Children.OfType<Canvas>().ToList()[i];
                                    Canvas.SetTop(button, 0);
                                    canvas.Children.Add(button);
                                }
                            }
                        }
                    }
                    #endregion
                    #region EveryElseCard
                    else
                    {
                        bool IsAcing = TryToAce(button, pos);
                        if(!IsAcing)
                        {
                            for (int i = 0; i<Myapp.tableDecks.Count; ++i)
                            {
                                Card? card = Myapp.tableDecks[i].LastOrDefault();
                                if (card is not null && CardHelp.IsCompatable(button.Card, card))
                                {
                                    pos.Item2.Children.Remove(button);
                                    pos.Item1.Remove(button.Card);
                                    Myapp.tableDecks[i].Add(button.Card);
                                    Canvas canvas = allTableDecks.Children.OfType<Canvas>().ToList()[i];
                                    Canvas.SetTop(button, 30*(canvas.Children.Count-1));
                                    canvas.Children.Add(button);
                                    OpenCard(pos.Item2.Children);
                                    break;
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
        }
        private (ObservableCollection<Card>, Canvas) Position(MyCardButton button)
        {
            (ObservableCollection<Card>, Canvas) pos = new(null, null);
            for(int i=0;i<Myapp.aces.Count;++i)
            {
                if (Myapp.aces[i].Contains(button.Card))
                {
                    pos.Item1 = Myapp.aces[i];
                    pos.Item2 = allAces.Children.OfType<Canvas>().ToList()[i+1];
                    break;
                }
            }
            if (currentCardDeck.Children.Contains(button))
            {
                pos.Item1=Myapp.deck;
                pos.Item2=currentCardDeck;
            }
            for (int i = 0; i<Myapp.tableDecks.Count; ++i)
            {
                if (Myapp.tableDecks[i].Contains(button.Card))
                {
                    pos.Item1 = Myapp.aces[i];
                    pos.Item2 = allTableDecks.Children.OfType<Canvas>().ToList()[i];
                    break;
                }
            }
            return pos;
        }
        private void OpenCard(UIElementCollection collection)
        {
            if(collection.Count>0)
            {
                IEnumerable<MyCardButton> myCardButtons = collection.OfType<MyCardButton>();
                MyCardButton last = myCardButtons.Last();
                last.IsOpen=true;
                last.Content = GetImageCard.GetImage(last.Card);
            }
        }
        private bool TryToAce(MyCardButton myCard, (ObservableCollection<Card>, Canvas) pos)
        {
            bool isAcing = false;
            for(int i=0;i<4;++i)
            {
                if (Myapp.aces[i].Count>0)
                {
                    if (CardHelp.IsAceCompatable(myCard.Card, Myapp.aces[i].Last()))
                    {
                        isAcing = true;
                        Canvas.SetTop(myCard, 0);
                        pos.Item2.Children.Remove(myCard);
                        pos.Item1.Remove(myCard.Card);
                        Myapp.aces[i].Add(myCard.Card);
                        Canvas canvas = allAces.Children.OfType<Canvas>().ToList()[i+1];
                        canvas.Children.Add(myCard);
                        OpenCard(pos.Item2.Children);
                        break;
                    }
                }
            }
            return isAcing;
        }
    }
}
//<Button x:Name="cardDeck" Grid.Column="0" Grid.Row="0" Height="100" Width="67" Content="{StaticResource cardTop}" Click="cardDeck_Click"/>