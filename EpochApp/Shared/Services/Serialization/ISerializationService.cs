// EpochWorlds
// ISerializeObject.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
namespace EpochApp.Shared.Services
{
    /// <summary>
    ///     Service contract for serializing and deserializing objects.
    /// </summary>
    public interface ISerializationService
    {
        /// <summary>
        ///     Serializes an object to XML.
        /// </summary>
        /// <returns>
        ///     <see cref="Task" />
        /// </returns>
        Task<string> SerializeToXml<TObject>(TObject objToSerialize) where TObject : class;

        /// <summary>
        ///     Deserializes an object from XML.
        /// </summary>
        /// <returns>
        ///     <see cref="Task" />
        /// </returns>
        Task<TObject> DeserializeFromXml<TObject>(string xmlString) where TObject : class;
    }

}