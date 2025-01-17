using Cosinka.Model;
using Cosinka.Viewmodel.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;
using wpfCosinka;

namespace Cosinka.Viewmodel.Realizations
{
    public class SaveA : ISave
    {
        MainWindow window;
        public SaveA(MainWindow window)
        {
            this.window=window;
        }
        public void Save(ApplicationViewModel model)
        {
            SaveClass saveClass = new();
            saveClass.deck=new List<SaveCard>(model.deck.Select(x => new SaveCard() { SavedCard=x, IsOpen=false }));
            saveClass.tableDecks=new List<List<SaveCard>>(window.allTableDecks.Children.OfType<Canvas>().Select(x => x.Children.OfType<MyCardButton>().Select(x => new SaveCard() { SavedCard=x.Card, IsOpen=x.IsOpen }).ToList()));
            saveClass.aces=new List<List<SaveCard>>(window.allAces.Children.OfType<Canvas>().Select(x => x.Children.OfType<MyCardButton>().Select(x => new SaveCard() { SavedCard=x.Card, IsOpen=x.IsOpen }).ToList()));
            XmlSerializer xmlSerializer = new(typeof(SaveClass));
            xmlSerializer.Serialize(new FileStream($"{Directory.GetCurrentDirectory()}/Saves/{DateTime.Now.DayOfYear}{DateTime.Now.Hour}{DateTime.Now.Second}.xml", FileMode.Create), saveClass);
        }
    }
}
