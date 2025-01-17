using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfCosinka;

namespace Cosinka.Model
{
    public record class SaveCard
    {
        public Card SavedCard { get; set; }
        public bool IsOpen { get; set; }
        public SaveCard()
        {

        }
    }
}
