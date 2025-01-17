using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using wpfCosinka;

namespace Cosinka.Model
{
    public record class SaveClass
    {
        [XmlArray]
        public List<SaveCard> deck { get; set; }
        [XmlArray]
        public List<List<SaveCard>> aces { get; set; }
        [XmlArray]
        public List<List<SaveCard>> tableDecks { get; set; }
        public SaveClass()
        {

        }

    }
}
