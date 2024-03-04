// EpochWorlds
// UserReports.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
namespace EpochApp.Shared.Users
{
    #pragma warning disable CS1591
    public class UserReport
    {
        public long ReportId { get; set; }

        public Guid PlaintiffId { get; set; }

        public Guid DefendantId { get; set; }

        public Guid OverseerId { get; set; }

        public ReportType ReportType { get; set; }

        public string Details { get; set; }

        public string Ruling { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ResolvedOn { get; set; }

        public virtual User Overseer { get; set; }

        public virtual User Plaintiff { get; set; }

        public virtual User Defendant { get; set; }
    }
}