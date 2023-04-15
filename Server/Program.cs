using System.Net;
using System.Net.Sockets;
using MySql.Data.MySqlClient;

namespace Server
{
    internal class Program
    {
        private static Dictionary<Socket, string> _users = new Dictionary<Socket, string>();

        static void Main(string[] args)
        {
            Socket server = CreateSocket();

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
            await Task.Run(() =>
            {
                while(true)
                {
                    
                }
            });
        }
    }
}