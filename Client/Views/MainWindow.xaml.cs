using Client.Models;
using Client.Views;
using SocketData;
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

namespace Client
{
    public partial class MainWindow : Window
    {
        private Socket _socket;

        public MainWindow(Socket socket)
        {
            InitializeComponent();
            
            _socket = socket;
            mainFrame.Content = new SignIn(mainFrame, socket);
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { ; }
        }

        private void ResizeWindow(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;

            else
                this.WindowState = WindowState.Normal;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Message message = new Message() { Text = "{!Disconnect}" };

            new Data<Message>(_socket).Send(message, 206);
            this.Close();
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;
    }
}
