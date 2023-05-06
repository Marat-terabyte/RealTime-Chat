using Client.ViewModels;
using System.Net.Sockets;
using System.Windows.Controls;

namespace Client.Views
{
    /// <summary>
    /// Логика взаимодействия для Chat.xaml
    /// </summary>
    public partial class Chat : Page
    {
        public Chat(Frame frame, Socket socket, string username)
        {
            InitializeComponent();
            DataContext = new ChatVM(frame, socket, username);
        }
    }
}
