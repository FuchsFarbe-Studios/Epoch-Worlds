using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Shared
{
    /// <summary>
    ///     A view component for a blog.
    /// </summary>
    public partial class BlogView
    {
        private List<PostDTO> _blogPosts = new List<PostDTO>();
        /// <summary>
        ///     The blog type to display.
        /// </summary>
        [Parameter] public BlogType BlogType { get; set; }
        /// <summary>
        ///     The HTTP client to use for the request.
        /// </summary>
        [Inject] public HttpClient Client { get; set; }
        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _blogPosts = await Client.GetFromJsonAsync<List<PostDTO>>($"api/v1/Blogs/BlogPosts/Type/{(int)BlogType}");
        }
    }
}