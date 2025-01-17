using Cosinka.Model;
using Cosinka.Viewmodel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using wpfCosinka;

namespace Cosinka.Viewmodel
{
    public static class ButtonTemplates
    {
        public static void GetStyle1Button (Button button)
        {
            button.Height=120;
            button.Width=87;
        }
        public static void GetButtonStyle2(IGetImageOfCard getImageOfCard,MyCardButton button,ApplicationViewModel application )
        {
            ButtonTemplates.GetStyle1Button(button);
            Image image = new() { Source = application.ImageBackCard };
            image.Stretch = System.Windows.Media.Stretch.UniformToFill;
            button.Content=button.IsOpen ? getImageOfCard.GetImage(button.Card) : image;
        }
    }
}
