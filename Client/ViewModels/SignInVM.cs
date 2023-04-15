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
    class SignInVM
    {
        private readonly Frame _frame;
        private readonly Socket _client;

        public RelayCommand ShowSignUpPageCommand { get; }
        public RelayCommand ShowRememberPasswordPageCommand { get; }
        public RelayCommand SignInCommand { get; }

        public SignInVM(Frame frame, Socket socket)
        {
            _frame = frame;
            _client = socket;

            ShowSignUpPageCommand = new RelayCommand(o => ShowSignUpPage());
            ShowRememberPasswordPageCommand = new RelayCommand(o => ShowRememberPasswordPage());
            SignInCommand = new RelayCommand(o => SignIn());
        }

        private void ShowSignUpPage() => _frame.Content = new SignUp(_frame, _client);

        private void ShowRememberPasswordPage() => _frame.Content = new RememberPassword(_frame, _client);

        private void SignIn()
        {
            ;
        }
    }
}
