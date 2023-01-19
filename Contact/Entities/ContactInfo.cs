namespace ContactApi.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using static ContactApi.Shared.Enums;

    [Table("ContactInfo")]
    public class ContactInfo
    {

        [Key]
        [Column("uuid")]
        public Guid UUID { get; set; }

        public InfoTypes InfoType { get; set; }

        public string Info { get; set; }

        public Guid ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
