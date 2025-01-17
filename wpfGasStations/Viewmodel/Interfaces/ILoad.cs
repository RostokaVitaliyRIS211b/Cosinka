using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using wpfCosinka;

namespace Cosinka.Viewmodel.Interfaces
{
    public interface ILoad
    {
        public void Load(MainWindow window, ApplicationViewModel MyApp);
    }
}
