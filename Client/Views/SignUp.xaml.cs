using Client.ViewModels;
using System.Net.Sockets;
using System.Windows.Controls;

namespace Client.Views
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        public SignUp(Frame frame, Socket socket)
        {
            InitializeComponent();
            this.DataContext = new SignUpVM(frame, socket);
        }
    }
}
