using System.ComponentModel.DataAnnotations;

namespace Contact.Model
{
    public class Contact
    {
        [Required]
        public Guid UUID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Company { get; set; }

        public ICollection<ContactInfo> ContactInfos { get; set; }


    }
}
