using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ContactApi.Shared.Enums;

namespace ContactApi.Entities
{
    [Table("ReportDetail")]
    public class ReportDetail
    {
        [Key]
        [Column("uuid")]
        public Guid UUID { get; set; }
        public string LocationName { get; set; }
        public int ContactCount { get; set; }
        public int PhoneCount { get; set; }        
        public Guid ReportId { get; set; }
        public virtual Report Report { get; set; }

    }
}
