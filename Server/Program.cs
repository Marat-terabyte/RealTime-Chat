using System.Net;
using System.Net.Sockets;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket server = CreateSocket();

            while (true)
            {
                Socket conn = server.Accept();
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
    }
}