using System;

namespace VenPaxAdmin.Client.Contact
{
    public class OfferRequest
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string EmailAdress { get; private set; }
        public string Message { get; private set; }
        public string Date { get; private set; }
        public string Category { get; private set; }
        public bool Notified { get; private set; }
        public bool Opened { get; private set; }

        public OfferRequest(int id, string name, string phoneNumber, string emailAdress, string message, string date,
                                string category, bool notified, bool opened)
        {
            this.Id = id;
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.EmailAdress = emailAdress;
            this.Message = message;
            this.Date = date;
            this.Category = category;
            this.Notified = notified;
            this.Opened = opened;
        }
    }
}
