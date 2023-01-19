using static Contact.Shared.Enums;

namespace Contact.Model
{
    public class ContactInfo
    {
        public Guid UUID { get; set; }

        public InfoTypes InfoType { get; set; }

        public string Info { get; set; }
    }
}
