using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfGasStations;

namespace Cosinka.Viewmodel
{
    public class ViewModelBuilder : IViewModelBuilder
    {
        Random random = new();
        public IEnumerable<ObservableCollection<Card>> GetDecks()
        {
            ObservableCollection<Card> deck = new();
            for(int i=0;i<4;++i)
            {
                for(int j=1;j<14;++j)
                {
                    deck.Add(new Card((CardRank)j, (CardSuit)i));
                }
            }
            List<ObservableCollection<Card>> cards = new();
            for(int i=1;i<8;++i)
            {
                ObservableCollection<Card> Somedeck = new();
                for(int j=0;j<i;++j)
                {
                    int random3 = random.Next(0, deck.Count-1);
                    Somedeck.Add(deck[random3]);
                    deck.RemoveAt(random3);
                }
                cards.Add(Somedeck);
            }
            Card[] cards1 = deck.ToArray();
            random.Shuffle(cards1);
            deck = new(cards1);
            cards.Add(deck);
            return cards;
        }
    }
}
