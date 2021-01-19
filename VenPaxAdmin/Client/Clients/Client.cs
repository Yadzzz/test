using System;

namespace VenPaxAdmin.Client.Clients
{
    public class Client
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string EmailAdress { get; set; }
        public string PhoneNumber { get; set; }

        public Client(int id, string company, string firstName, string lastName, string socialSecurityNumber,
                        string emailAdress, string phoneNumber)
        {
            this.Id = id;
            this.Company = company;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.SocialSecurityNumber = socialSecurityNumber;
            this.EmailAdress = emailAdress;
            this.PhoneNumber = phoneNumber;
        }
    }
}
