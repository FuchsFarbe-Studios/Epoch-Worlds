// EpochWorlds
// UserReportDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
#pragma warning disable CS1591
namespace EpochApp.Shared.Users
{
    public class UserReportDTO
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
    }
}