using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class DatabaseContext
    {
        private MySqlConnection _connection;

        public async void ConnectToDatabase(string addres, string user, string database, string password)
        {
            string connectionString = $"server={addres};user={user};database={database};password={password};";

            _connection = new MySqlConnection(connectionString);
            await _connection.OpenAsync();
        }
    }
}
