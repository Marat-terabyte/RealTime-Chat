using Client.ViewModels;
using System.Net.Sockets;
using System.Windows.Controls;

namespace Client.Views
{
    /// <summary>
    /// Логика взаимодействия для RememberPassword.xaml
    /// </summary>
    public partial class RememberPassword : Page
    {
        public RememberPassword(Frame frame, Socket socket)
        {
            InitializeComponent();
            this.DataContext = new RememberPasswordVM(frame, socket);
        }
    }
}
