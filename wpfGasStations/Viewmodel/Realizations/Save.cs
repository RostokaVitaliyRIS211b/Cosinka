using Cosinka.Model;
using Cosinka.Viewmodel.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using wpfCosinka;

namespace Cosinka.Viewmodel.Realizations
{
    public class Save : ISave
    {
        void ISave.Save(ApplicationViewModel model)
        {
            SaveClass saveClass = new();
            saveClass.aces=model.aces;
            saveClass.tableDecks=model.tableDecks;
            saveClass.deck=model.deck;
            XmlSerializer xmlSerializer = new(typeof(SaveClass));
            xmlSerializer.Serialize(new FileStream($"{Directory.GetCurrentDirectory()}/Saves/{DateTime.Now.DayOfYear}{DateTime.Now.Hour}{DateTime.Now.Second}.xml", FileMode.Create), saveClass);
        }
    }
}
