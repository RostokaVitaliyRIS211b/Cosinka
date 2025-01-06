using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpfGasStations.Viewmodel;

namespace wpfGasStations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
            
        }

        private void cardDeck_Click(object sender, RoutedEventArgs e)
        {
            Button button = new();
            button.Height=100;
            button.Width=67;
            button.Content=Resources["cardSelect"];
            if (currentCardDeck.Children.Count>0)
                currentCardDeck.Children.Clear();
            currentCardDeck.Children.Add(button);
        }
    }
}
//<Button x:Name="cardDeck" Grid.Column="0" Grid.Row="0" Height="100" Width="67" Content="{StaticResource cardTop}" Click="cardDeck_Click"/>