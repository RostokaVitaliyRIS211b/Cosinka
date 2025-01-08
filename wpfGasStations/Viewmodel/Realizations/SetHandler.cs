using Cosinka.Viewmodel.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace wpfCosinka
{

    public partial class MainWindow
    {
        class SetHandler : ISetHandlerClick
        {
            MainWindow window;
            public SetHandler(MainWindow window)
            {
                this.window = window;
            }
            void ISetHandlerClick.SetHandler(IEnumerable<Button> buttons)
            {
                foreach(Button button in buttons)
                {
                    button.Click+=window.card_Click;
                }
            }

            void ISetHandlerClick.SetHandler(Button button)
            {
                button.Click+=window.card_Click;
            }
        }
    }
}
//<Button x:Name="cardDeck" Grid.Column="0" Grid.Row="0" Height="100" Width="67" Content="{StaticResource cardTop}" Click="cardDeck_Click"/>