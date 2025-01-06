using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfGasStations
{
    public enum CardRank
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace,
    }
    public enum CardSuit
    {
        Heart = 0,
        Diamond,
        Club,
        Spade
    }

    public readonly record struct Card
    {
        public string ImagePath { get; init; }
        public CardRank Rank { get; init; }
        public CardSuit Suit { get; init; }
        public Card(string imagePath, CardRank cardRank, CardSuit cardSuit)
        {
            ImagePath = imagePath;
            Rank = cardRank;
            Suit = cardSuit;
        }
    }
}
