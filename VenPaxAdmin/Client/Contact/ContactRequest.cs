using System;

namespace VenPaxAdmin.Client.Contact
{
    public class ContactRequest
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string EmailAdress { get; private set; }
        public string Message { get; private set; }
        public string Date { get; private set; }
        public bool Notified { get; private set; }
        public bool Opened { get; private set; }

        public ContactRequest(int id, string name, string phoneNumber, string emailAdress, string message, string date,
                                bool notified, bool opened)
        {
            this.Id = id;
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.EmailAdress = emailAdress;
            this.Message = message;
            this.Date = date;
            this.Notified = notified;
            this.Opened = opened;
        }
    }
}
