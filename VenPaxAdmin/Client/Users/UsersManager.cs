using System;
using System.Collections.Generic;

namespace VenPaxAdmin.Client.Users
{
    public class UsersManager
    {
        private Dictionary<int, User> users;

        public UsersManager()
        {
            this.users = new Dictionary<int, User>();
            this.LoadUsers();
        }

        private void LoadUsers()
        {
            using (var dbCommand = VenPax.DatabaseManager.NewCommand())
            {
                dbCommand.SetQuery("SELECT * FROM users");

                using (var dbReader = dbCommand.RunQuery())
                {
                    if(dbReader == null)
                    {
                        return;
                    }

                    while(dbReader.Read())
                    {
                        this.users.Add(Convert.ToInt32(dbReader["id"].ToString()), new User(Convert.ToInt32(dbReader["id"].ToString()), dbReader["username"].ToString(),
                                                dbReader["password"].ToString(), dbReader["pin_code"].ToString(), dbReader["first_name"].ToString(),
                                                dbReader["last_name"].ToString(), dbReader["email_adress"].ToString(), dbReader["phone_number"].ToString()));
                    }
                }
            }
        }

        public void CreateUser(string username, string password, string pinCode, string firstName, string lastName,
                                    string emailAdress, string phoneNumber)
        {
            using (var dbCommand = VenPax.DatabaseManager.NewCommand())
            {
                dbCommand.SetQuery("SELECT * FROM users WHERE username = @username");
                dbCommand.AddParameter("username", username);

                using (var dbReader = dbCommand.RunQuery())
                {
                    if (dbReader == null)
                    {
                        //Does not exist in database
                        //Display & log error
                        return;
                    }
                }
            }

            int id = 0;
            using (var dbCommand = VenPax.DatabaseManager.NewCommand())
            {
                dbCommand.SetQuery("INSERT INTO users (username, password, pin, first_name, last_name, email_adress, phone_number) VALUES (@username, @password, @pincode, @firstname, @lastname, @emailadress, @phonenumber);");
                dbCommand.AddParameter("username", username);
                dbCommand.AddParameter("password", password);
                dbCommand.AddParameter("pincode", pinCode);
                dbCommand.AddParameter("firstname", firstName);
                dbCommand.AddParameter("lastname", lastName);
                dbCommand.AddParameter("emailadress", emailAdress);
                dbCommand.AddParameter("phonenumber", phoneNumber);

                try
                {
                    id = Convert.ToInt32(dbCommand.ExecuteScalar());
                }
                catch
                {
                    //Tries to get the user id, null was returned if this executed
                    //Display & log error
                    return;
                }
            }

            User user = new User(id, username, password, pinCode, firstName, lastName, emailAdress, phoneNumber);
            this.users.Add(id, user);
        }

        public void EditUser(int id, string username, string password, string pinCode, string firstName, string lastName,
                                    string emailAdress, string phoneNumber)
        {
            if (!this.users.ContainsKey(id))
            {
                //Does not exist in memory
                //Display & log error
                return;
            }

            using (var dbCommand = VenPax.DatabaseManager.NewCommand())
            {
                dbCommand.SetQuery("SELECT * FROM users WHERE id = @id");
                dbCommand.AddParameter("id", id);

                using (var dbReader = dbCommand.RunQuery())
                {
                    if (dbReader == null)
                    {
                        //Does not exist in database
                        //Display & log error
                        return;
                    }
                }
            }

            using (var dbCommand = VenPax.DatabaseManager.NewCommand())
            {
                dbCommand.SetQuery("UPDATE users SET username = @username, password = @password, pincode = @pincode, " +
                                    "first_name = @firstname, last_name = @lastname, email_adress = @emailadress, " +
                                    "phone_number = @phonenumber WHERE id = @id");
                dbCommand.AddParameter("id", id);
                dbCommand.AddParameter("username", username);
                dbCommand.AddParameter("password", password);
                dbCommand.AddParameter("pincode", pinCode);
                dbCommand.AddParameter("firstname", firstName);
                dbCommand.AddParameter("lastname", lastName);
                dbCommand.AddParameter("emailadress", emailAdress);
                dbCommand.AddParameter("phonenumber", phoneNumber);
                dbCommand.RunNonQuery();
            }

            User user = this.users[id];
            user.Username = username;
            user.Password = password;
            user.PinCode = pinCode;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.EmailAdress = emailAdress;
            user.PhoneNumber = phoneNumber;
        }

        public void DeleteUser(int id)
        {

            if(!this.users.ContainsKey(id))
            {
                //Display & log error
                return;
            }

            using (var dbCommand = VenPax.DatabaseManager.NewCommand())
            {
                dbCommand.SetQuery("SELECT * FROM users WHERE id = @id");
                dbCommand.AddParameter("id", id);

                using (var dbReader = dbCommand.RunQuery())
                {
                    if(dbReader == null)
                    {
                        //Display & log error
                        return;
                    }
                }
            }

            using (var dbCommand = VenPax.DatabaseManager.NewCommand())
            {
                dbCommand.SetQuery("DELETE FROM users WHERE id = @id");
                dbCommand.AddParameter("id", id);
                dbCommand.RunNonQuery();
            }

            this.users.Remove(id);
        }

        public bool TryGetUser(int id, out User user)
        {
            return this.users.TryGetValue(id, out user);
        }
    }
}
