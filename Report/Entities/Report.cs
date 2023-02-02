using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ReportApi.Shared.Enums;

namespace ReportApi.Entities
{
    [Table("Report")]
    public class Report
    {
        [Key]
        [Column("uuid")]
        public Guid UUID { get; set; }

        public DateTime RequestDate { get; set; }

        public ReportStatus Status { get; set; }

        public virtual ICollection<ReportDetail> ReportDetails{ get; set; }

    }
}
