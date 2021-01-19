using System;
using System.Collections.Generic;

namespace VenPaxAdmin.Client.Clients
{
    public class ClientsManager
    {
        private Dictionary<int, Client> clients;

        public ClientsManager()
        {
            this.clients = new Dictionary<int, Client>();
            this.LoadUsers();
        }

        private void LoadUsers()
        {
            using (var dbCommand = VenPax.DatabaseManager.NewCommand())
            {
                dbCommand.SetQuery("SELECT * FROM clients");

                using (var dbReader = dbCommand.RunQuery())
                {
                    if (dbReader == null)
                    {
                        return;
                    }

                    while (dbReader.Read())
                    {
                        this.clients.Add(Convert.ToInt32(dbReader["id"].ToString()), new Client(Convert.ToInt32(dbReader["id"].ToString()), dbReader["company"].ToString(),
                                                dbReader["first_name"].ToString(),dbReader["last_name"].ToString(), dbReader["social_security_number"].ToString(),
                                                dbReader["email_adress"].ToString(), dbReader["phone_number"].ToString()));
                    }
                }
            }
        }

        public void CreateProject(string company, string firstName, string lastName, string socialSecurityNumber,
                        string emailAdress, string phoneNumber)
        {
            int id = 0;
            using (var dbCommand = VenPax.DatabaseManager.NewCommand())
            {
                dbCommand.SetQuery("INSERT INTO users (company, first_name, last_name, social_security_number, email_adress, phone_number) VALUES (@company, @firstname, @lastname, @socialsecuritynumber, @emailadress, @phonenumber);");
                dbCommand.AddParameter("company", company);
                dbCommand.AddParameter("firstname", firstName);
                dbCommand.AddParameter("lastname", lastName);
                dbCommand.AddParameter("socialsecuritynumber", socialSecurityNumber);
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

            this.clients.Add(id, new Client(id, company, firstName, lastName, socialSecurityNumber, emailAdress, phoneNumber));
        }

        public void EditClient(int id, string company, string firstName, string lastName, string socialSecurityNumber,
                        string emailAdress, string phoneNumber)
        {
            if (!this.clients.ContainsKey(id))
            {
                //Does not exist in memory
                //Display & log error
                return;
            }

            using (var dbCommand = VenPax.DatabaseManager.NewCommand())
            {
                dbCommand.SetQuery("SELECT * FROM clients WHERE id = @id");
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
                dbCommand.SetQuery("UPDATE users SET company = @company, first_name = @firstname, last_name = @lastname, " +
                                    "social_security_number = @socialsecuritynumber, email_adress = @email_adress, " +
                                    "phone_number = @phonenumber WHERE id = @id");
                dbCommand.AddParameter("id", id);
                dbCommand.AddParameter("company", company);
                dbCommand.AddParameter("firstname", firstName);
                dbCommand.AddParameter("lastname", lastName);
                dbCommand.AddParameter("socialsecuritynumber", socialSecurityNumber);
                dbCommand.AddParameter("emailadress", emailAdress);
                dbCommand.AddParameter("phonenumber", phoneNumber);
                dbCommand.RunNonQuery();
            }

            Client client = this.clients[id];
            client.Company = company;
            client.FirstName = firstName;
            client.LastName = lastName;
            client.SocialSecurityNumber = socialSecurityNumber;
            client.EmailAdress = emailAdress;
            client.PhoneNumber = phoneNumber;
        }

        public void DeleteClient(int id)
        {
            if (!this.clients.ContainsKey(id))
            {
                //Display & log error
                return;
            }

            using (var dbCommand = VenPax.DatabaseManager.NewCommand())
            {
                dbCommand.SetQuery("SELECT * FROM clients WHERE id = @id");
                dbCommand.AddParameter("id", id);

                using (var dbReader = dbCommand.RunQuery())
                {
                    if (dbReader == null)
                    {
                        //Display & log error
                        return;
                    }
                }
            }

            using (var dbCommand = VenPax.DatabaseManager.NewCommand())
            {
                dbCommand.SetQuery("DELETE FROM clients WHERE id = @id");
                dbCommand.AddParameter("id", id);
                dbCommand.RunNonQuery();
            }

            this.clients.Remove(id);
        }

        public bool TryGetClient(int id, out Client client)
        {
            return this.clients.TryGetValue(id, out client);
        }
    }
}
