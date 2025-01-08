using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cosinka.Viewmodel.Interfaces
{
    public interface ISetHandlerClick
    {
        public void SetHandler(IEnumerable<Button> buttons);
        public void SetHandler(Button button);
    }
}
