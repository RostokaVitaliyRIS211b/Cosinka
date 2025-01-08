using Cosinka.Model;
using Cosinka.Viewmodel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using wpfCosinka;

namespace Cosinka.Viewmodel.Realizations
{
    public class GenerateDecksInteface : IGenerateDecksInterface
    {
        ApplicationViewModel application;
        IGetImageOfCard getImage;
        public GenerateDecksInteface(ApplicationViewModel application, IGetImageOfCard GetImage)
        {
            this.application =application;
            getImage=GetImage;
        }
        public void GenerateInterface(MainWindow window)
        {
            List<MyCardButton> myCardButtons = new(GenerateForSomeCollection(application.tableDeck1));
            window.currenttable1Deck.Children.Add(myCardButtons[0]);

            myCardButtons= new(GenerateForSomeCollection(application.tableDeck2));
            for (int i = 0; i<myCardButtons.Count; ++i)
            {
                Canvas.SetTop(myCardButtons[i], 30*i);
                window.currenttable2Deck.Children.Add(myCardButtons[i]);
            }

            myCardButtons= new(GenerateForSomeCollection(application.tableDeck3));
            for (int i = 0; i<myCardButtons.Count; ++i)
            {
                Canvas.SetTop(myCardButtons[i], 30*i);
                window.currenttable3Deck.Children.Add(myCardButtons[i]);
            }
            myCardButtons= new(GenerateForSomeCollection(application.tableDeck4));
            for (int i = 0; i<myCardButtons.Count; ++i)
            {
                Canvas.SetTop(myCardButtons[i], 30*i);
                window.currenttable4Deck.Children.Add(myCardButtons[i]);
            }
            myCardButtons= new(GenerateForSomeCollection(application.tableDeck5));
            for (int i = 0; i<myCardButtons.Count; ++i)
            {
                Canvas.SetTop(myCardButtons[i], 30*i);
                window.currenttable5Deck.Children.Add(myCardButtons[i]);
            }
            myCardButtons= new(GenerateForSomeCollection(application.tableDeck6));
            for (int i = 0; i<myCardButtons.Count; ++i)
            {
                Canvas.SetTop(myCardButtons[i], 30*i);
                window.currenttable6Deck.Children.Add(myCardButtons[i]);
            }
            myCardButtons= new(GenerateForSomeCollection(application.tableDeck7));
            for (int i = 0; i<myCardButtons.Count; ++i)
            {
                Canvas.SetTop(myCardButtons[i], 30*i);
                window.currenttable7Deck.Children.Add(myCardButtons[i]);
            }
        }
        protected IList<MyCardButton> GenerateForSomeCollection(IList<Card> cards)
        {
            List<MyCardButton> buttons = new();
            for (int i = 0; i<cards.Count; ++i)
            {
                MyCardButton myCardButton = new(cards[i], i==cards.Count-1);
                ButtonTemplates.GetStyle1Button(myCardButton);
                Image image = new() { Source = application.ImageBackCard };
                image.Stretch = System.Windows.Media.Stretch.UniformToFill;
                myCardButton.Content=i==cards.Count-1 ? getImage.GetImage(cards[i]) : image;
                buttons.Add(myCardButton);
            }
            return buttons;
        }
    }
}
