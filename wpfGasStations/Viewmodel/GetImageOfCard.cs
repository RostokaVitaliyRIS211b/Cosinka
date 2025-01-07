using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using wpfGasStations;

namespace Cosinka.Viewmodel
{
    public class GetImageOfCard : IGetImageOfCard
    {
        ApplicationViewModel app;
        public GetImageOfCard(ApplicationViewModel app)
        {
            this.app=app;
        }
        public Image GetImage(Card card)
        {
            Int32Rect int32Rect = new(12+(122*(((int)card.Rank)-1)), 12+180*((int)card.Suit), 108, 162);
            Image image = new();
            image.Source = new CroppedBitmap(app.CardsMapImage, int32Rect);
            return image;
        }
    }
}
