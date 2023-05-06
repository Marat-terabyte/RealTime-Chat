using Client.ViewModels;
using System.Net.Sockets;
using System.Windows.Controls;

namespace Client.Views
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        public SignIn(Frame frame, Socket socket)
        {
            InitializeComponent();
            this.DataContext = new SignInVM(frame, socket);
        }
    }
}
