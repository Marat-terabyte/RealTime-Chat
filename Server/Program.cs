using System.Net;
using System.Net.Sockets;
using Models;
using SocketData;

namespace Server
{
    internal class Program
    {
        private static Dictionary<Socket, string> _users = new Dictionary<Socket, string>();
        private static DatabaseContext _databaseContext = new DatabaseContext();

        static void Main(string[] args)
        {
            Socket server = CreateSocket();
            _databaseContext.ConnectToDatabase("localhost", "root", "realtime_chat", "11111111");

            while (true)
            {
                Socket conn = server.Accept();
                InteractWithClient(conn);
            }
        }

        private static Socket CreateSocket()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 7070);
            socket.Bind(ipEndPoint);
            socket.Listen();

            return socket;
        }

        private static async void InteractWithClient(Socket conn)
        {
            // 0 - Sign in
            // 1 - Sign up
            // 2 - Remember password

            await Task.Run(() =>
            {
                while(true)
                {
                    string? choice = new Data<Message>(conn).Receive(206).Text;

                    if (choice is null)
                        continue;

                    if (choice == "{!Disconnect}")
                    {
                        DisconnectUser(conn);
                        break;
                    }

                    Account? account = new Data<Account>(conn).Receive(1024);

                    if (choice.Equals("0"))
                    {
                        bool isSignIn = _databaseContext.SignIn(account);
                        new Data<string>(conn).Send(isSignIn.ToString(), 16);

                        if (isSignIn)
                        {
                            _users.Add(conn, account.Username);
                            Listen(conn);
                            break;
                        }
                    }
                    
                    else if (choice.Equals("1"))
                    {
                        bool isSignUp = _databaseContext.SignUp(account);
                        new Data<string>(conn).Send(isSignUp.ToString(), 16);
                    }

                    else if (choice.Equals("2"))
                    {
                        string? password = _databaseContext.GetPassword(account);
                        new Data<string>(conn).Send(password, 1024);
                    }
                }
            });
        }

        private static void Listen(Socket socket)
        {
            while (true)
            {
                Message? message = new Data<Message>(socket).Receive(2048);

                if (message == null)
                    continue;

                if (message.Text == "{!Disconnect}")
                {
                    DisconnectUser(socket);

                    message.From = "";
                    message.Text = $"{_users[socket]} was disconnected";
                    _users.Remove(socket);

                    SendToAll(socket, message);

                    break;
                }

                SendToAll(socket, message);
            }
        }

        private static void SendToAll(Socket socket, Message message)
        {
            foreach (var user in _users)
            {
                if (user.Key != socket)
                    new Data<Message>(user.Key).Send(message, 2048);

            }
        }

        private static void DisconnectUser(Socket socket)
        {
            socket.Disconnect(false);
            socket.Close();
        }
    }
}