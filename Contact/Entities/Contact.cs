using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactApi.Entities
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        [Column("uuid")]        
        public Guid UUID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Company { get; set; }

        public ICollection<ContactInfo> ContactInfos { get; set; }


    }
}
