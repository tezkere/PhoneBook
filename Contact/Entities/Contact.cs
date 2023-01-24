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
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Company { get; set; }        
        public virtual ICollection<ContactInfo> ContactInfos { get; set; }


    }
}
