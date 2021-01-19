using System;
using MySql.Data.MySqlClient;

namespace VenPaxAdmin.Server.Database
{
    public class DatabaseConnection
    {
        protected MySqlConnection mySqlConnection;

        public DatabaseConnection()
        {
            this.mySqlConnection = new MySqlConnection("Server=127.0.0.1;Database=boutique;Uid=root;Pwd=Server1042!;");
        }

        public void Open()
        {
            this.mySqlConnection.Open();
        }

        public void Close()
        {
            this.mySqlConnection.Close();
        }

        public void DisposeConnection()
        {
            if (this.mySqlConnection.State != System.Data.ConnectionState.Closed)
                this.mySqlConnection.Close();

            this.mySqlConnection.Dispose();
        }
    }
}
