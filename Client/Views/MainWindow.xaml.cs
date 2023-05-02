using Client.Models;
using Client.Views;
using Microsoft.Win32;
using SocketData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

            LoadTheme();
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { ; }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                Message message = new Message() { Text = "{!Disconnect}" };

                new Data<Message>(_socket).Send(message, 206);
            }
            finally
            {
                this.Close();
            }
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;

        private void ChangeFrameToSettings(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Settings(mainFrame, _socket);
        }

        private void LoadTheme()
        {
            string? theme = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\RealTime-Chat").GetValue("Theme") as string;

            if (theme is null)
                return;

            Theme.ChangeTheme(theme);
        }
    }
}
