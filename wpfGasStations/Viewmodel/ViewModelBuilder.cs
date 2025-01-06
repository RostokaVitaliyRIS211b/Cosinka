using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfGasStations
{
    public interface IViewModelBuilder
    {
        public IEnumerable<ObservableCollection<Card>> GetDecks();
    }
}
