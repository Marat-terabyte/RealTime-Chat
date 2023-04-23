﻿using Client.Models;
using SocketData;
using System;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Client.ViewModels
{
    internal class ChatVM
    {
        private Frame _frame;
        private Socket _socket;
        private string _username;

        public ObservableCollection<Message> Messages { get; set; }
        public RelayCommand SendMessageCommand { get; set; }

        public ChatVM(Frame frame, Socket socket, string username)
        {
            _frame = frame;
            _socket = socket;
            _username = username;

            Messages = new ObservableCollection<Message>();

            SendMessageCommand = new RelayCommand(o => SendMessage(o));
            ReceiveMessages();
        }

        private async void SendMessage(object obj)
        {
            Message message = new Message();

            message.Alignment = HorizontalAlignment.Right;
            message.Text = ((TextBox) obj).Text;
            message.Time = DateTime.Now.ToString("t");
            message.From = _username;

            Messages.Add(message);

            await Task.Run(() =>
            {
                new Data<Message>(_socket).Send(message, 2048);
            });
        }

        private async void ReceiveMessages()
        {
            while (true)
            {
                Message? data = new Message();

                await Task.Run(() =>
                { 
                    data = new Data<Message>(_socket).Receive(2048);
                    data.Alignment = HorizontalAlignment.Left;
                });

                Messages.Add(data);
            }
        }
    }
}
