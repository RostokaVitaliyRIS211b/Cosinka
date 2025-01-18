using Cosinka.Model;
using Cosinka.Viewmodel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;
using wpfCosinka;

namespace Cosinka.Viewmodel.Realizations
{
    public class Load : ILoad
    {
        IGetImageOfCard GetImageCard;
        ISetHandlerClick SetHandler;
        public Load(IGetImageOfCard GetImageCard, ISetHandlerClick SetHandler)
        {
            this.GetImageCard=GetImageCard;
            this.SetHandler=SetHandler;
        }
        void ILoad.Load(MainWindow window, ApplicationViewModel MyApp)
        {
            XmlSerializer xmlSerializer = new(typeof(SaveClass));
            SaveClass saveClass = xmlSerializer.Deserialize(new FileStream($"{Directory.GetCurrentDirectory()}/Saves/{window.ListBox.SelectedItem}", FileMode.Open)) as SaveClass;
            MyApp.deck=new(saveClass.deck.Select(x => x.SavedCard));
            List<IEnumerable<Card>> cards = new(saveClass.aces.Select(x => x.Select(x => x.SavedCard)));
            MyApp.aces=new();
            cards.RemoveAt(0);
            foreach (var obj in cards)
            {
                ObservableCollection<Card> cards1 = new(obj);
                MyApp.aces.Add(cards1);
            }
            cards = new(saveClass.tableDecks.Select(x => x.Select(x => x.SavedCard)));
            MyApp.tableDecks=new();
            foreach (var obj in cards)
            {
                ObservableCollection<Card> cards1 = new(obj);
                MyApp.tableDecks.Add(cards1);
            }
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
            foreach (Canvas canvas in window.allAces.Children.OfType<Canvas>())
            {
                while (canvas.Children.Count>1)
                    canvas.Children.RemoveAt(1);
            }
            foreach (Canvas canvas in window.allTableDecks.Children.OfType<Canvas>())
            {
                while (canvas.Children.Count>1)
                    canvas.Children.RemoveAt(1);
            }
            window.currentCardDeck.Children.Clear();
            for (int i = 0; i<5; ++i)
            {
                Canvas canvas = window.allAces.Children.OfType<Canvas>().ToList()[i];
                if (saveClass.aces[i].Count>0)
                {
                    MyCardButton button = new(saveClass.aces[i].Last().SavedCard, saveClass.aces[i].Last().IsOpen);
                    ButtonTemplates.GetButtonStyle2(GetImageCard, button, MyApp);
                    Canvas.SetTop(button, 0);
                    canvas.Children.Add(button);
                }
            }
            for (int i = 0; i < 7; i++)
            {
                int j = 0;
                Canvas canvas = window.allTableDecks.Children.OfType<Canvas>().ToList()[i];
                foreach (var item in saveClass.tableDecks[i])
                {
                    MyCardButton button = new(item.SavedCard, item.IsOpen);
                    ButtonTemplates.GetButtonStyle2(GetImageCard, button, MyApp);
                    Canvas.SetTop(button, 30*j);
                    canvas.Children.Add(button);
                    ++j;
                }
            }
            foreach (Canvas deck in window.allTableDecks.Children)
            {
                SetHandler.SetHandler(deck.Children.OfType<Button>());
            }
        }
    }
}
