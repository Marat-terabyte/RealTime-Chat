using Client.Models;
using Client.Views;
using Models;
using SocketData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client.ViewModels
{
    class SignUpVM
    {
        private readonly Frame _frame;
        private readonly Socket _client;

        public RelayCommand ShowSignInPageCommand { get; }
        public RelayCommand SignUpCommand { get; }

        public Account Account { get; set; }

        public SignUpVM(Frame frame, Socket socket)
        {
            this._frame = frame;
            this._client = socket;

            ShowSignInPageCommand = new RelayCommand(o => ShowSignInPage());
            SignUpCommand = new RelayCommand(SignUp);

            Account = new Account();
        }

        private void ShowSignInPage() => _frame.Content = new SignIn(_frame, _client);

        private async void SignUp(object obj)
        {
            bool isSignUp = false;

            await Task.Run(() =>
            {
                Account.Password = ((PasswordBox) obj).Password;

                bool isUsernameValid = FieldChecker.IsValidStr(Account.Username);
                bool isSecretWordValid = FieldChecker.IsValidStr(Account.SecretWord);
                bool isPasswordValid = FieldChecker.IsValidPassword(Account.Password);

                if (isUsernameValid && isSecretWordValid && isPasswordValid)
                {
                    Message message = new Message() { Text = "1" };

                    new Data<Message>(_client).Send(message, 206);
                    new Data<Account>(_client).Send(Account, 1024);
                    isSignUp = Convert.ToBoolean(new Data<string>(_client).Receive(16));
                }
            });

            if (isSignUp)
            {
                _frame.Content = new SignIn(_frame, _client);
                return;
            }

            MessageBox.Show("Username exists!");
        }
    }
}
