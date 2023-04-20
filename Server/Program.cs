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
                    string? choice = new Data<string>(conn).Receive(16);

                    if (choice is null)
                        continue;

                    Account? account = new Data<Account>(conn).Receive(1024);

                    if (choice.Equals("0"))
                    {
                        bool isSignIn = _databaseContext.SignIn(account);
                        new Data<string>(conn).Send(isSignIn.ToString(), 16);
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
                    
                    else
                        DisconnectUser(conn);
                }
            });
        }

        private static void DisconnectUser(Socket socket)
        {
            socket.Disconnect(true);
            socket.Close();
        }
    }
}