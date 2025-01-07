using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using wpfGasStations;

namespace Cosinka.Viewmodel
{
    public class CardButton:Button
    {
        public Card card { get; init; }
        public bool IsOpen { get; set; } 
     
    }
}
