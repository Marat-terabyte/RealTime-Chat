using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace SocketData
{
    public class Data<T>
    {
        private readonly Socket _socket;
        private const string _endOfFile = "<EOF>";

        public Data(Socket socket)
        {
            _socket = socket;
        }

        public void Send(T data, int length)
        {
            string json = JsonSerializer.Serialize<T>(data);
            json += _endOfFile;

            byte[] bytes = Encoding.UTF8.GetBytes(json);
            Array.Resize(ref bytes, length);

            _socket.Send(bytes);
        }

        public T? Receive(int length)
        {
            byte[] bytes = new byte[length];
            _socket.Receive(bytes);

            string json = Encoding.UTF8.GetString(bytes);
            int len = json.IndexOf(_endOfFile);
            
            json = json[..len];
            
            T? data = JsonSerializer.Deserialize<T>(json);

            return data;
        }
    }
}