// EpochWorlds
// Consonant.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared.Config.Lookups
{
	public class Consonant : Phoneme
	{
		public String Manner { get; set; }
		public String Place { get; set; }
		public Boolean IsVoiced { get; set; }
	}
}