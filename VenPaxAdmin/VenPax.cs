using System;
using VenPaxAdmin.Client.Users;
using VenPaxAdmin.Server.Database;

namespace VenPaxAdmin
{
    public static class VenPax
    {
        public static DatabaseManager DatabaseManager;
        public static UsersManager UsersManager;

        public static void Initialize()
        {
            DatabaseManager = new DatabaseManager();
            UsersManager = new UsersManager();
        }
    }
}
