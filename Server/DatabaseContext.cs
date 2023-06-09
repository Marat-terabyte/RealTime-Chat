﻿using Models;
using MySql.Data.MySqlClient;

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

        public bool SignIn(Account account)
        {
            string query = $"SELECT password FROM users WHERE username = '{account.Username}';";

            string? password = GetPassword(account);

            if (password == null || !account.Password.Equals(password))
                return false;

            return true;
        }

        public bool SignUp(Account account)
        {
            string query = $"INSERT INTO users(username, secret_word, password) " +
                $"VALUES('{account.Username}', '{account.SecretWord}', '{account.Password}');";

            try
            {
                MySqlCommand command = new MySqlCommand(query, _connection);
                command.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                return false;
            }

            return true;
        }

        public string? GetPassword(Account account)
        {
            string query = $"SELECT password FROM users WHERE username = '{account.Username}'";

            MySqlCommand command = new MySqlCommand(query, _connection);
            string? password = command.ExecuteScalar() as string;

            return password;
        }
    }
}
