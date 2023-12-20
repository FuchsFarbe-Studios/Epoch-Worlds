// EpochWorlds
// MetaTemplate.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared.Config.Lookups
{
	/// <summary>
	///     A meta template for directing users in world-meta generation.
	/// </summary>
	public class MetaTemplate
	{
		public Int32 TemplateID { get; set; }
		public Int32 CategoryID { get; set; }
		public String TemplateName { get; set; }
		public String Description { get; set; }
		public String Placeholder { get; set; }
		public String HelpText { get; set; }

		public virtual MetaCategory Category { get; set; }
	}
}