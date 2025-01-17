using Cosinka.Viewmodel.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using wpfCosinka;

namespace Cosinka.Viewmodel.Realizations
{
    public class GetImageOfCard : IGetImageOfCard
    {
        public BitmapImage CardsMapImage;
        public GetImageOfCard()
        {
            Uri uri2 = new Uri(Directory.GetCurrentDirectory()+"\\resources\\521.png", UriKind.Absolute);
            CardsMapImage = new(uri2);
        }
        public Image GetImage(Card card)
        {
            Int32Rect int32Rect = new(12+122*((int)card.Rank-1), 12+180*(int)card.Suit, 108, 162);
            Image image = new();
            image.Source = new CroppedBitmap(CardsMapImage, int32Rect);
            return image;
        }
    }
}
