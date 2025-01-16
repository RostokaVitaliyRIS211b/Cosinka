using Cosinka.Viewmodel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfCosinka;

namespace Cosinka.Viewmodel.Realizations
{
    public class GetNextCard : IGetNextCard
    {
        protected int i = 0;
        ApplicationViewModel model;
        public GetNextCard(ApplicationViewModel model)
        {
            this.model = model;
        }
        Card? IGetNextCard.GetNextCard()
        {
            if (model.deck.Count>0)
                i=(i+1)%model.deck.Count;
            else
                return null;
            return model.deck[i];
        }
    }
}
