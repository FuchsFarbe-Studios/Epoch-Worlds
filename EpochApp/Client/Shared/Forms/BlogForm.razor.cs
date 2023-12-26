using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EpochApp.Client.Shared.Forms
{
    public partial class BlogForm
    {
        [Parameter] public Blog Blog { get; set; } = new Blog();

        private Task CreateBlogAsync(EditContext arg)
        {
            return null;
        }
    }
}