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
            for(int i=1;i<application.tableDecks.Count;++i)
            {
                myCardButtons= new(GenerateForSomeCollection(application.tableDecks[i]));
                Canvas canvas = window.allTableDecks.Children[i] as Canvas;
                for (int j = 0; j<myCardButtons.Count; ++j)
                {
                    Canvas.SetTop(myCardButtons[j], 30*j);
                    canvas.Children.Add(myCardButtons[j]);
                }
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
