// EpochWorlds
// EpochStyle.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
using System.ComponentModel;

namespace EpochComponents.Enums
{
    public enum ContainerEdge
    {
        [Description("")]
        None,

        [Description("edge-sharp")]
        Sharp,

        [Description("edge-round-sm")]
        Round,

        [Description("edge-circle")]
        Circle
    }
}