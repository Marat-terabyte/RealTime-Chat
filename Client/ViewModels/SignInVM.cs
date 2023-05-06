using Client.Models;
using Client.Views;
using Models;
using SocketData;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client.ViewModels
{
    class SignInVM
    {
        private readonly Frame _frame;
        private readonly Socket _client;

        public Account Account { get; set; }

        public RelayCommand ShowSignUpPageCommand { get; }
        public RelayCommand ShowRememberPasswordPageCommand { get; }
        public RelayCommand SignInCommand { get; }

        public SignInVM(Frame frame, Socket socket)
        {
            _frame = frame;
            _client = socket;

            this.Account = new Account();

            ShowSignUpPageCommand = new RelayCommand(o => ShowSignUpPage());
            ShowRememberPasswordPageCommand = new RelayCommand(o => ShowRememberPasswordPage());
            SignInCommand = new RelayCommand(SignIn);
        }

        private void ShowSignUpPage() => _frame.Content = new SignUp(_frame, _client);

        private void ShowRememberPasswordPage() => _frame.Content = new RememberPassword(_frame, _client);

        private async void SignIn(object obj)
        {
            bool isSignIn = false;

            await Task.Run(() =>
            {
                Account.Password = ((PasswordBox)obj).Password;

                bool isUsernameValid = FieldChecker.IsValidStr(Account.Username);
                bool isPasswordValid = FieldChecker.IsValidPassword(Account.Password);

                if (isUsernameValid && isPasswordValid)
                {
                    Message message = new Message() { Text = "0" };

                    new Data<Message>(_client).Send(message, 206);
                    new Data<Account>(_client).Send(this.Account, 1024);

                    isSignIn = Convert.ToBoolean(new Data<string>(_client).Receive(16));
                }
            });

            if (isSignIn)
            {
                _frame.Content = new Chat(_frame, _client, Account.Username);
                return;
            }

            MessageBox.Show("Wrong username or password!");
        }
    }
}
