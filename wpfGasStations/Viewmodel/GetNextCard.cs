﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfGasStations;

namespace Cosinka.Viewmodel
{
    public class GetNextCard : IGetNextCard
    {
        protected int i = 0;
        protected ObservableCollection<Card> cards;
        public GetNextCard(ApplicationViewModel model)
        {
            cards = model.deck;
        }
        Card IGetNextCard.GetNextCard()
        {
            i=(i+1)%cards.Count;
            return cards[i];
        }
    }
}
