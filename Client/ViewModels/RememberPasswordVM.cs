﻿using Client.Models;
using Client.Views;
using Models;
using SocketData;
using System.Net.Sockets;
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
            RememberPasswordCommand = new RelayCommand(RememberPassword);

            Account = new Account();
        }

        private void ShowSignIn() => _frame.Content = new SignIn(_frame, _client);

        private async void RememberPassword(object obj)
        {
            string? password = null;

            await Task.Run(() =>
            {
                Account.SecretWord = ((PasswordBox)obj).Password;

                bool isUsernameValid = FieldChecker.IsValidStr(Account.Username);
                bool isSecretWordValid = FieldChecker.IsValidStr(Account.SecretWord);

                if (isUsernameValid && isSecretWordValid)
                {
                    Message message = new Message() { Text = "2" };

                    new Data<Message>(_client).Send(message, 206);
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
