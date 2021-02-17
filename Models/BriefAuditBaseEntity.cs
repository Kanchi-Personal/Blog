using System;
using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models
{
    public class BriefAuditBaseEntity
    {
        public BriefAuditBaseEntity()
        {
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
            ModifiedBy = 1;
            CreatedBy = 1;
        }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        [Timestamp]
        public byte[] RowVersionStamp { get; set; }
    }
}
