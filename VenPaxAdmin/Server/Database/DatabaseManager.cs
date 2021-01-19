using System;
namespace VenPaxAdmin.Server.Database
{
    public class DatabaseManager
    {
        public string ConnectionString { get; private set; }

        public DatabaseManager()
        {
            this.ConnectionString = "Server=127.0.0.1;Database=live;Uid=root;Pwd=123;";
            //Try db connection
        }

        public DatabaseCommand NewCommand()
        {
            return new DatabaseCommand();
        }
    }
}
