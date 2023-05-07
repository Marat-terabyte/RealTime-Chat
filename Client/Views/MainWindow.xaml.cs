using Client.Models;
using Client.Views;
using Microsoft.Win32;
using SocketData;
using System;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;

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
            catch (InvalidOperationException) {; }
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
            string? theme = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\RealTime-Chat\Theme").GetValue("Theme") as string;

            if (theme is null)
                return;

            Theme.ChangeTheme(theme);
        }
    }
}
