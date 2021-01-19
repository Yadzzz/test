using System;
using MySql.Data.MySqlClient;

namespace VenPaxAdmin.Server.Database
{
    public class DatabaseCommand : DatabaseConnection, IDisposable
    {
        private MySqlCommand mySqlCommand;

        public DatabaseCommand()
        {
            this.mySqlCommand = new MySqlCommand();
            this.mySqlCommand.Connection = base.mySqlConnection;
        }

        public void SetQuery(string command)
        {
            this.mySqlCommand.CommandText = command;
        }

        public void AddParameter(string param, object value)
        {
            this.mySqlCommand.Parameters.AddWithValue(param, value);
        }

        public MySqlDataReader RunQuery()
        {
            if (base.mySqlConnection.State == System.Data.ConnectionState.Closed)
            {
                base.Open();
            }

            return this.mySqlCommand.ExecuteReader();
        }

        public int RunNonQuery()
        {
            if (base.mySqlConnection.State == System.Data.ConnectionState.Closed)
            {
                base.Open();
            }

            return this.mySqlCommand.ExecuteNonQuery();
        }

        public object ExecuteScalar()
        {
            if (base.mySqlConnection.State == System.Data.ConnectionState.Closed)
            {
                base.Open();
            }

            return this.mySqlCommand.ExecuteScalar();
        }

        public void Dispose()
        {
            this.mySqlCommand.Dispose();
            base.DisposeConnection();
        }
    }
}
