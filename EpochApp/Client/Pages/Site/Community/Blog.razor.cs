using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Site.Community
{
    public partial class Blog
    {
        private Blog _blog;
        [Inject] private NavigationManager Nav { get; set; }
        [Inject] private HttpClient Client { get; set; }

        /// <summary>
        ///     The blog id to display.
        /// </summary>
        [Parameter] public int? BlogId { get; set; }

        /// <inheritdoc />
        protected override async Task OnParametersSetAsync()
        {
            if (BlogId != null)
            {
                var response = await Client.GetFromJsonAsync<Blog>($"api/v1/Blogs/{BlogId}");
                if (response != null)
                {
                    _blog = response;
                }

            }

            await base.OnParametersSetAsync();
        }
    }
}