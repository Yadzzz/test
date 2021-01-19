using System;

namespace VenPaxAdmin.Client.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PinCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAdress { get; set; }
        public string PhoneNumber { get; set; }

        public User(int id, string username, string password, string pinCode, string firstName, string lastName,
                    string emailAdress, string phoneNumber)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.PinCode = pinCode;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailAdress = emailAdress;
            this.PhoneNumber = phoneNumber;
        }
    }
}
