using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using wpfCosinka;

namespace Cosinka.Model
{
    public class MyCardButton:Button
    {
        public Card Card { get; }
        public bool IsOpen { get; set; }
        public MyCardButton(Card card, bool isOpen)
        {
            this.Card = card;
            this.IsOpen=isOpen;
        }

    }
}
