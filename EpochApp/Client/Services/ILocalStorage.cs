// EpochWorlds
// ILocalStorage.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Client.Services
{
    public interface ILocalStorage
    {
        /// <summary>
        /// Gets a value from local storage based on the given key.
        /// </summary>
        /// <param name="key">Key for the value.</param>
        /// <typeparam name="T">The object type of the expected value object.</typeparam>
        /// <returns><see cref="Task{TResult}"/></returns>
        public Task<T> GetValueAsync<T>(string key);

        /// <summary>
        /// Sets a value in local storage with the given key.
        /// </summary>
        /// <param name="key">Unique key name.</param>
        /// <param name="value">The object to store.</param>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns><see cref="Task"/></returns>
        public Task SetValueAsync<T>(string key, T value);

        /// <summary>
        /// Clears the local storage.
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public Task Clear();

        /// <summary>
        /// Removes a value from local storage based on the given key.
        /// </summary>
        /// <param name="key">Name of the value to remove.</param>
        /// <returns><see cref="Task"/></returns>
        public Task RemoveAsync(string key);
    }
}