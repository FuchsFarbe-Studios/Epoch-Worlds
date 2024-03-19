// EpochWorlds
// BanTicketDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
#pragma warning disable CS1591
namespace EpochApp.Shared
{
    public class BanTicketDTO
    {
        public long TicketId { get; set; }
        public Guid UserID { get; set; }
        public Guid AdminID { get; set; }
        public bool IsIndefinite { get; set; } = false;
        public DateTime? CreatedOn { get; set; }
        public DateTime? RemovedOn { get; set; }
        public string Reason { get; set; }
    }
}