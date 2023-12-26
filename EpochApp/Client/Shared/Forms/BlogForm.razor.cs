using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Shared.Forms
{
    public partial class BlogForm
    {
        [Inject] public ILogger<BlogForm> Logger { get; set; }
        [Inject] public EpochAuthProvider Auth { get; set; }
        [Inject] public HttpClient Client { get; set; }
        [Parameter] public Blog Blog { get; set; } = new Blog();

        private async Task CreateBlogAsync(EditContext arg)
        {
            Blog.ModifiedBy = Auth.CurrentUser.UserName.ToUpper();
            Blog.ModifiedOn = DateTime.Now;
            var response = await Client.PutAsJsonAsync($"api/v1/Blogs/{Blog.BlogID}", Blog);
            if (!response.IsSuccessStatusCode)
                Logger.LogError("Error: " + response.StatusCode);
            else
                Logger.LogInformation("Success: " + response.StatusCode);
        }
    }
}