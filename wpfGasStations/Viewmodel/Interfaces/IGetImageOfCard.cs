using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using wpfCosinka;

namespace Cosinka.Viewmodel.Interfaces
{
    public interface IGetImageOfCard
    {
        public Image GetImage(Card card);
    }
}
