using Cosinka.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using wpfCosinka;

namespace Cosinka.Viewmodel.Interfaces
{
    public interface IGetNewPostition
    {
        (ObservableCollection<Card>?,Canvas?) GetNewPosition(MyCardButton myCardButton);
    }
}
