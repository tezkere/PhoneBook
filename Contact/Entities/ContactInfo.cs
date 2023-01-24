namespace ContactApi.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using static ContactApi.Shared.Enums;
    using Newtonsoft.Json;

    [Table("ContactInfo")]
    public class ContactInfo
    {

        [Key]
        [Column("uuid")]
        public Guid UUID { get; set; }
        [Required]
        public InfoTypes InfoType { get; set; }
        [Required]
        public string Info { get; set; }
        [Required]
        public Guid ContactId { get; set; }
        [JsonIgnore]
        public virtual Contact Contact { get; set; }
    }
}
