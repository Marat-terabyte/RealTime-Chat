using Client.ViewModels;
using System.Net.Sockets;
using System.Windows.Controls;

namespace Client.Views
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings(Frame frame, Socket socket)
        {
            InitializeComponent();

            DataContext = new SettingsVM(frame, socket);
        }
    }
}
