using Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Client.ViewModels
{
    class RememberPasswordVM
    {
        private readonly Frame _frame;
        private readonly Socket _client;

        public RelayCommand ShowSignInCommand { get; }
        public RelayCommand RememberPasswordCommand { get; }

        public RememberPasswordVM(Frame frame, Socket socket)
        {
            this._frame = frame;
            this._client = socket;

            ShowSignInCommand = new RelayCommand(o => ShowSignIn());
            RememberPasswordCommand = new RelayCommand(o => RememberPassword());
        }

        private void ShowSignIn() => _frame.Content = new SignIn(_frame, _client);

        private void RememberPassword()
        {
            ;
        }
    }
}
