using Cosinka.Model;
using Cosinka.Viewmodel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using wpfCosinka;

namespace Cosinka.Viewmodel.Realizations
{
    public class GetNewPositionRealization : IGetNewPostition
    {
        ApplicationViewModel MyApp;
        MainWindow window;
        public GetNewPositionRealization(ApplicationViewModel MyApp,MainWindow window)
        {
            this.MyApp=MyApp;
            this.window = window;
        }
        public (ObservableCollection<Card>?,Canvas?) GetNewPosition(MyCardButton myCardButton)
        {
            (ObservableCollection<Card>?, Canvas?) pos = window.Position(myCardButton);
            (ObservableCollection<Card>?, Canvas?) newPos = (null,null);
            if (myCardButton.IsOpen && pos.Item1!=MyApp.ace1 && pos.Item1!=MyApp.ace2 && pos.Item1!=MyApp.ace3 && pos.Item1!=MyApp.ace4)
            {
                #region Aces
                if (myCardButton.Card.Rank==CardRank.Ace)
                {
                    if (myCardButton.Card.Suit==CardSuit.Heart)
                    {
                        newPos.Item1 = MyApp.ace1;
                        newPos.Item2 = window.currentAce1Deck;
                    }
                    else if (myCardButton.Card.Suit==CardSuit.Diamond)
                    {
                        newPos.Item2 = window.currentAce2Deck;
                        newPos.Item1 = MyApp.ace2;
                    }
                    else if (myCardButton.Card.Suit==CardSuit.Club)
                    {
                        newPos.Item1 = MyApp.ace3;
                        newPos.Item2 = window.currentAce3Deck;
                    }
                    else
                    {
                        newPos.Item1 = MyApp.ace4;
                        newPos.Item2 = window.currentAce4Deck;
                    }
                }
                #endregion
                #region Kings
                else if (myCardButton.Card.Rank==CardRank.King)
                {
                    newPos  = TryToAce(myCardButton, pos.Item1);
                    if (newPos.Item1 is null)
                    {
                        for (int i = 0; i<MyApp.tableDecks.Count; ++i)
                        {
                            if (MyApp.tableDecks[i].Count==0)
                            {
                                newPos.Item1 = MyApp.tableDecks[i];
                                newPos.Item2 = window.allTableDecks.Children.OfType<Canvas>().ToList()[i];
                                break;
                            }
                        }
                    }
                }
                #endregion
                #region EveryElseCard
                else
                {
                    newPos  = TryToAce(myCardButton, pos.Item1);
                    if (newPos.Item1 is null)
                    {
                        for (int i = 0; i<MyApp.tableDecks.Count; ++i)
                        {
                            Card? card = MyApp.tableDecks[i].LastOrDefault();
                            if (card is not null && CardHelp.IsCompatable(myCardButton.Card, card))
                            {
                                newPos.Item1 = MyApp.tableDecks[i];
                                newPos.Item2 = window.allTableDecks.Children.OfType<Canvas>().ToList()[i];
  
                                break;
                            }
                        }
                    }
                }
                #endregion
            }
            return newPos;
        }
        private (ObservableCollection<Card>?, Canvas?) TryToAce(MyCardButton myCard, ObservableCollection<Card> pos)
        {
            (ObservableCollection<Card> ?, Canvas ?) AceCollection = (null,null);
            if (pos.IndexOf(myCard.Card)==pos.Count-1 || pos==MyApp.deck)
            {
                for (int i = 0; i<4; ++i)
                {
                    if (MyApp.aces[i].Count>0)
                    {
                        if (CardHelp.IsAceCompatable(myCard.Card, MyApp.aces[i].Last()))
                        {
                            AceCollection.Item1 = MyApp.aces[i];
                            AceCollection.Item2 = window.allAces.Children.OfType<Canvas>().ToList()[i+1];
                            break;
                        }
                    }
                }
            }
            return AceCollection;
        }
    }
}
