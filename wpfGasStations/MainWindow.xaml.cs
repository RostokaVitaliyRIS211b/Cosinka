using Cosinka.Model;
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
                MyCardButton? compatable = null;
                (ObservableCollection<Card>, Canvas) pos = Position(button);
                if (button.IsOpen && pos.Item2!=currentAce1Deck && pos.Item2!=currentAce2Deck && pos.Item2!=currentAce3Deck && pos.Item2!=currentAce1Deck)
                {
                    #region Aces
                    if (button.Card.Rank==CardRank.Ace)
                    {
                        pos.Item2.Children.Remove(button);
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
                        if(pos.Item2!= currentCardDeck)
                            OpenCard(pos.Item2.Children.OfType<MyCardButton>());
                    }
                    #endregion

                }
                if (compatable is not null)
                {
                    (ObservableCollection<Card>, Canvas) pos2 = Position(compatable);
                }
            }
        }
        private (ObservableCollection<Card>, Canvas) Position(MyCardButton button)
        {
            (ObservableCollection<Card>, Canvas) pos = new(null, null);
            if (currentAce1Deck.Children.Contains(button))
            {
                pos.Item1=Myapp.ace1;
                pos.Item2=currentAce1Deck;
            }
            if (currentAce2Deck.Children.Contains(button))
            {
                pos.Item1=Myapp.ace2;
                pos.Item2=currentAce2Deck;
            }
            if (currentAce3Deck.Children.Contains(button))
            {
                pos.Item1=Myapp.ace3;
                pos.Item2=currentAce3Deck;
            }
            if (currentAce4Deck.Children.Contains(button))
            {
                pos.Item1=Myapp.ace4;
                pos.Item2=currentAce4Deck;
            }
            if (currentCardDeck.Children.Contains(button))
            {
                pos.Item1=Myapp.deck;
                pos.Item2=currentCardDeck;
            }
            if (currenttable1Deck.Children.Contains(button))
            {
                pos.Item1=Myapp.tableDeck1;
                pos.Item2=currenttable1Deck;
            }
            if (currenttable2Deck.Children.Contains(button))
            {
                pos.Item1=Myapp.tableDeck2;
                pos.Item2=currenttable2Deck;
            }
            if (currenttable3Deck.Children.Contains(button))
            {
                pos.Item1=Myapp.tableDeck3;
                pos.Item2=currenttable3Deck;
            }
            if (currenttable4Deck.Children.Contains(button))
            {
                pos.Item1=Myapp.tableDeck4;
                pos.Item2=currenttable4Deck;
            }
            if (currenttable5Deck.Children.Contains(button))
            {
                pos.Item1=Myapp.tableDeck5;
                pos.Item2=currenttable5Deck;
            }
            if (currenttable6Deck.Children.Contains(button))
            {
                pos.Item1=Myapp.tableDeck6;
                pos.Item2=currenttable6Deck;
            }
            if (currenttable7Deck.Children.Contains(button))
            {
                pos.Item1=Myapp.tableDeck7;
                pos.Item2=currenttable7Deck;
            }

            return pos;
        }
        private void OpenCard(IEnumerable<MyCardButton> myCardButtons)
        {
            if(myCardButtons.Count()>0)
            {
                MyCardButton last = myCardButtons.Last();
                last.IsOpen=true;
                last.Content = GetImageCard.GetImage(last.Card);
            }
        }
    }
}
//<Button x:Name="cardDeck" Grid.Column="0" Grid.Row="0" Height="100" Width="67" Content="{StaticResource cardTop}" Click="cardDeck_Click"/>