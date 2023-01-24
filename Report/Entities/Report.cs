using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ContactApi.Shared.Enums;

namespace ContactApi.Entities
{
    [Table("Contact")]
    public class Report
    {
        [Key]
        [Column("uuid")]
        public Guid UUID { get; set; }

        public DateTime RequestDate { get; set; }

        public ReportStatus Status { get; set; }

        public virtual ReportDetail ReportDetail{ get; set; }

    }
}
