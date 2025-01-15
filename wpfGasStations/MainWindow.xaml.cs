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
using System.Xml.Serialization;

namespace wpfCosinka
{

    public partial class MainWindow : Window
    {
        public ApplicationViewModel MyApp { get; set; }
        public IGetImageOfCard GetImageCard { get; set; }
        public IGetNextCard NextCard { get; set; }
        public IGetNewPostition GetNewPostition { get; set; }
        public IGenerateDecksInterface GenerateDecksInterface { get; set; }
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
            serviceCollection.AddSingleton<IGetNewPostition, GetNewPositionRealization>();
            serviceCollection.AddScoped<ISetHandlerClick, SetHandler>();
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            MyApp=serviceProvider.GetRequiredService<ApplicationViewModel>();
            DataContext=MyApp;
            GetNewPostition = serviceProvider.GetRequiredService<IGetNewPostition>();
            NextCard =serviceProvider.GetRequiredService<IGetNextCard>();
            GetImageCard=serviceProvider.GetRequiredService<IGetImageOfCard>();
            GenerateDecksInterface = serviceProvider.GetRequiredService<IGenerateDecksInterface>();
            GenerateDecksInterface.GenerateInterface(this);
            ISetHandlerClick setHandler = serviceProvider.GetRequiredService<ISetHandlerClick>();
            foreach (Canvas deck in allTableDecks.Children)
            {
                setHandler.SetHandler(deck.Children.OfType<Button>());
            }
            ListBox.ItemsSource=new DirectoryInfo(Directory.GetCurrentDirectory()+"/Saves/").GetFiles().Select(x=>x.Name);
        }

        private void cardDeck_Click(object sender, RoutedEventArgs e)
        {
            Card card = NextCard.GetNextCard();
            if(card!=null)
            {
                MyCardButton button = new(card, true);
                ButtonTemplates.GetStyle1Button(button);
                button.Click+=card_Click;
                button.Content=GetImageCard.GetImage(card);
                if (currentCardDeck.Children.Count>0)
                    currentCardDeck.Children.Clear();
                currentCardDeck.Children.Add(button);
                if (MyApp.deck.Count==1)
                    cardDeck.Content = null;
            }
        }
        private void card_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MyCardButton button)
            {
                (ObservableCollection<Card>, Canvas) myPosition = Position(button);
                (ObservableCollection<Card>?, Canvas?) newPosition = GetNewPostition.GetNewPosition(button);
                if(newPosition.Item1 is not null)
                {
                    if(allAces.Children.Contains(newPosition.Item2))
                    {
                        myPosition.Item2.Children.Remove(button);
                        myPosition.Item1.Remove(button.Card);
                        Canvas.SetTop(button, 0);
                        newPosition.Item2.Children.Add(button);
                        newPosition.Item1.Add(button.Card);
                    }
                    else
                    {
                        List<MyCardButton> needToMove = new() { button };
                        if (myPosition.Item1.IndexOf(button.Card)!=myPosition.Item1.Count-1 && myPosition.Item2.Children.Count>2)
                        {
                            for (int j = myPosition.Item1.IndexOf(button.Card)+2; j<myPosition.Item1.Count+1; ++j)
                            {
                                MyCardButton button1 = myPosition.Item2.Children[j] as MyCardButton;
                                needToMove.Add(button1);
                            }
                        }
                        for (int j = 0; j<needToMove.Count; ++j)
                        {
                            myPosition.Item2.Children.Remove(needToMove[j]);
                            myPosition.Item1.Remove(needToMove[j].Card);
                            newPosition.Item1.Add(needToMove[j].Card);

                            Canvas.SetTop(needToMove[j], 30*(newPosition.Item2.Children.Count-1));
                            newPosition.Item2.Children.Add(needToMove[j]);
                        }
                    }
                    OpenCard(myPosition.Item2.Children);

                }
                if (MyApp.ace1.Count==13 && MyApp.ace2.Count==13 && MyApp.ace3.Count==13 && MyApp.ace4.Count==13)
                {
                    MessageBox.Show("ПОБЕДА");
                }
            }
        }
        private void OpenCard(UIElementCollection collection)
        {
            if (collection.Count>1)
            {
                IEnumerable<MyCardButton> myCardButtons = collection.OfType<MyCardButton>();
                MyCardButton last = myCardButtons.Last();
                last.IsOpen=true;
                last.Content = GetImageCard.GetImage(last.Card);
            }
        }
        public (ObservableCollection<Card>, Canvas) Position(MyCardButton button)
        {
            (ObservableCollection<Card>, Canvas) pos = new(null, null);
            for (int i = 0; i<MyApp.aces.Count; ++i)
            {
                if (MyApp.aces[i].Contains(button.Card))
                {
                    pos.Item1 = MyApp.aces[i];
                    pos.Item2 = allAces.Children.OfType<Canvas>().ToList()[i+1];
                    break;
                }
            }
            if (currentCardDeck.Children.Contains(button))
            {
                pos.Item1=MyApp.deck;
                pos.Item2=currentCardDeck;
            }
            for (int i = 0; i<MyApp.tableDecks.Count; ++i)
            {
                if (MyApp.tableDecks[i].Contains(button.Card))
                {
                    pos.Item1 = MyApp.tableDecks[i];
                    pos.Item2 = allTableDecks.Children.OfType<Canvas>().ToList()[i];
                    break;
                }
            }
            return pos;
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveClass saveClass = new();
            saveClass.aces=MyApp.aces;
            saveClass.tableDecks=MyApp.tableDecks;
            saveClass.deck=MyApp.deck;
            XmlSerializer xmlSerializer = new(typeof(SaveClass));
            xmlSerializer.Serialize(new FileStream($"{Directory.GetCurrentDirectory()}/Saves/{DateTime.Now.DayOfYear}{DateTime.Now.Hour}{DateTime.Now.Second}.xml", FileMode.Create),saveClass);
        }

        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer xmlSerializer = new(typeof(SaveClass));
            SaveClass saveClass = xmlSerializer.Deserialize(new FileStream($"{Directory.GetCurrentDirectory()}/Saves/{ListBox.SelectedItem}",FileMode.Open)) as SaveClass;
            MyApp.deck=saveClass.deck;
            MyApp.aces=saveClass.aces;
            MyApp.tableDecks=saveClass.tableDecks;
            MyApp.ace1=MyApp.aces[0];
            MyApp.ace2=MyApp.aces[1];
            MyApp.ace3=MyApp.aces[2];
            MyApp.ace4=MyApp.aces[3];
            MyApp.tableDeck1=MyApp.tableDecks[0];
            MyApp.tableDeck2=MyApp.tableDecks[1];
            MyApp.tableDeck3=MyApp.tableDecks[2];
            MyApp.tableDeck4=MyApp.tableDecks[3];
            MyApp.tableDeck5=MyApp.tableDecks[4];
            MyApp.tableDeck6=MyApp.tableDecks[5];
            MyApp.tableDeck7=MyApp.tableDecks[6];
           
                //<Rectangle Width="67" Height="100" StrokeThickness="5" Stroke="Black"/>
            foreach (var obj in allAces.Children.OfType<Canvas>())
            {
                
            }
            foreach (var obj in allTableDecks.Children.OfType<Canvas>())
            {
               
            }
            currentCardDeck.Children.Clear();
            GenerateDecksInterface.GenerateInterface(this);
        }
    }
}