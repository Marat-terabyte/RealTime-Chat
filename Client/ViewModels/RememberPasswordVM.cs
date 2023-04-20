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
    class RememberPasswordVM
    {
        private readonly Frame _frame;
        private readonly Socket _client;

        public RelayCommand ShowSignInCommand { get; }
        public RelayCommand RememberPasswordCommand { get; }

        public Account Account { get; set; }

        public RememberPasswordVM(Frame frame, Socket socket)
        {
            this._frame = frame;
            this._client = socket;

            ShowSignInCommand = new RelayCommand(o => ShowSignIn());
            RememberPasswordCommand = new RelayCommand(o => RememberPassword());

            Account = new Account();
        }

        private void ShowSignIn() => _frame.Content = new SignIn(_frame, _client);

        private async void RememberPassword()
        {
            string? password = null;

            await Task.Run(() =>
            {
                bool isUsernameValid = FieldChecker.IsValidStr(Account.Username);
                bool isSecretWordValid = FieldChecker.IsValidStr(Account.Username);

                if (isUsernameValid && isSecretWordValid)
                {
                    new Data<string>(_client).Send("2", 16);
                    new Data<Account>(_client).Send(Account, 1024);
                    password = new Data<string>(_client).Receive(1024);
                }
            });

            if (password == null)
            {
                MessageBox.Show("Wrong username or secret word!");
                return;
            }

            MessageBox.Show(password);
        }
    }
}
