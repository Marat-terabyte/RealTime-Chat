using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Net.Sockets;

namespace Client.ViewModels
{
    class ConnectToServerVM
    {
        private Window _window;

        public ConnectToServerVM(Window window)
        {
            _window = window;
            Connect();
        }

        private async void Connect()
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7070);

            while (true)
            {
                try
                {
                    client.Connect(ipEndPoint);
                    break;
                }
                catch (SocketException)
                {
                    MessageBox.Show("Error");
                    await Task.Delay(3000);
                }
            }

            OpenMainWindow();
        }

        private void OpenMainWindow()
        {
            new MainWindow().Show();
            CloseWindow();
        }

        private void CloseWindow() => _window.Close();
    }
}
