// EpochWorlds
// LocalStorageAccessor.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using Microsoft.JSInterop;

namespace EpochApp.Client.Services
{
    /// <summary>
    ///     Local storage service for saving and loading data from the browser's local storage.
    /// </summary>
    public class LocalStorageAccessor : ILocalStorage
    {
        private readonly IJSRuntime _jsRuntime;
        private Lazy<IJSObjectReference> _accessorJsRef = new Lazy<IJSObjectReference>();

        /// <summary> Constructor. </summary>
        /// <param name="jsRuntime"> The JavaScript runtime. </param>
        public LocalStorageAccessor(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        /// <inheritdoc />
        public async Task<T> GetValueAsync<T>(string key)
        {
            await WaitForReference();
            var result = await _accessorJsRef.Value.InvokeAsync<T>("get", key);

            return result;
        }

        /// <inheritdoc />
        public async Task SetValueAsync<T>(string key, T value)
        {
            await WaitForReference();
            await _accessorJsRef.Value.InvokeVoidAsync("set", key, value);
        }

        /// <inheritdoc />
        public async Task Clear()
        {
            await WaitForReference();
            await _accessorJsRef.Value.InvokeVoidAsync("clear");
        }

        /// <inheritdoc />
        public async Task RemoveAsync(string key)
        {
            await WaitForReference();
            await _accessorJsRef.Value.InvokeVoidAsync("remove", key);
        }

        private async Task WaitForReference()
        {
            if (_accessorJsRef.IsValueCreated is false)
            {
                _accessorJsRef = new Lazy<IJSObjectReference>(await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/epoch-script.js"));
            }
        }

        /// <summary>
        ///     Disposes of the local storage accessor.
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            if (_accessorJsRef.IsValueCreated)
            {
                await _accessorJsRef.Value.DisposeAsync();
            }
        }
    }
}