using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Internal
{
    /// <summary>
    ///     A page to display blogs.
    /// </summary>
    [Authorize(Roles = "ADMIN,INTERNAL")]
    public partial class InternalBlogs
    {
        private IEnumerable<BlogDTO> _blogs = new List<BlogDTO>();
        private BlogDTO _currentEditingBlog;
        private bool _editTriggerRowClick = false;
        private bool _isCellEditMode = true;
        private bool _loading;
        private BlogDTO _modelBlog = new BlogDTO();
        private bool _readOnly = false;
        private bool _showCreateBlog;
        /// <summary> Injected Auth Provider </summary>
        [Inject] public EpochAuthProvider Auth { get; set; }
        /// <summary> </summary>
        [Inject] public HttpClient Client { get; set; }
        /// <summary> Injected Logger </summary>
        [Inject] public ILogger<InternalBlogs> Logger { get; set; }
        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await LoadBlogsAsync();
        }
        private async Task LoadBlogsAsync()
        {
            _loading = true;
            await Task.Delay(500);
            var blogs = await Client.GetFromJsonAsync<IEnumerable<BlogDTO>>("api/v1/Blogs");
            _blogs = blogs.ToList();
            _loading = false;
        }
        private async Task CommitBlogChangesAsync(BlogDTO blogData)
        {
            _loading = true;
            // Put blog changes to server
            var response = await Client.PutAsJsonAsync($"api/v1/Blogs/{blogData.BlogID}", blogData);
            if (!response.IsSuccessStatusCode)
            {
                _loading = false;
            }
            _loading = false;
            await Task.CompletedTask;
        }
        private async Task AddBlogAsync(MouseEventArgs arg)
        {
            _showCreateBlog = true;
            await Task.CompletedTask;
        }
        private async Task CreateNewBlogAsync(EditContext arg)
        {
            _loading = true;
            var blogData = (BlogDTO)arg.Model;
            blogData.CreatedOn = DateTime.Now;
            blogData.CreatedBy = Auth?.CurrentUser?.UserName;
            var response = await Client.PostAsJsonAsync("api/v1/Blogs", blogData);
            if (!response.IsSuccessStatusCode)
            {
                _loading = false;
                _showCreateBlog = false;
                Logger.LogError("Failed to create new blog");
            }
            Logger.LogInformation("New blog created successfully");
            _loading = false;
            _showCreateBlog = false;
            StateHasChanged();
        }
    }
}