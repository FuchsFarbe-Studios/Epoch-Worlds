// EpochWorlds
// BanList.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
#pragma warning disable CS1591
namespace EpochApp.Shared.Users
{
    public class BanTicket
    {
        public long TicketId { get; set; }
        /// <summary>
        /// The banned user.
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// The admin who banned this user.
        /// </summary>
        public Guid AdminID { get; set; }
        public bool IsIndefinite { get; set; } = false;
        public DateTime? CreatedOn { get; set; }
        public DateTime? RemovedOn { get; set; }
        public string Reason { get; set; }
        public virtual User Admin { get; set; }
        public virtual User User { get; set; }
    }

}