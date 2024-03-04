// EpochWorlds
// ForgotPasswordDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    public class ForgotPasswordDTO
    {
        [Required] [MaxLength(256)]
        public string User { get; set; }
    }
}