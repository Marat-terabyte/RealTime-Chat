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
    class SignUpVM
    {
        private readonly Frame _frame;
        private readonly Socket _client;

        public RelayCommand ShowSignInPageCommand { get; }
        public RelayCommand SignUpCommand { get; }

        public SignUpVM(Frame frame, Socket socket)
        {
            this._frame = frame;
            this._client = socket;

            ShowSignInPageCommand = new RelayCommand(o => ShowSignInPage());
            SignUpCommand = new RelayCommand(o => SignUp());
        }

        private void ShowSignInPage() => _frame.Content = new SignIn(_frame, _client);

        private void SignUp()
        {
            ;
        }
    }
}
