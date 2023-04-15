using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
