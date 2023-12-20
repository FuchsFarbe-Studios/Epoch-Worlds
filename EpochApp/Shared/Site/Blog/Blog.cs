// EpochWorlds
// Blog.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023


// EpochWorlds
// Blog.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared.Config.Lookups;

namespace EpochApp.Shared.Site.Blog
{
	public class Blog
	{
		public Blog()
		{
			BlogPosts = new HashSet<BlogPost>();
		}
		public int BlogTypeID { get; set; }
		public int BlogID { get; set; }

		public string Name { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public BlogTypeInfo Type => GetBlogType(BlogTypeID);

		public virtual BlogType BlogType { get; set; }
		public ICollection<BlogPost> BlogPosts { get; set; }
		public ICollection<BlogOwner> BlogOwners { get; set; }

		public static BlogTypeInfo GetBlogType(int blogTypeID)
		{
			return (BlogTypeInfo)blogTypeID;
		}
	}
}