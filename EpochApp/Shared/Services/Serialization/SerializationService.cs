// EpochWorlds
// SerializationService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
using System.Xml.Serialization;

namespace EpochApp.Shared.Services
{
    /// <summary>
    ///     Service for serializing and deserializing objects.
    /// </summary>
    public class SerializationService : ISerializationService
    {
        /// <inheritdoc />
        public Task<string> SerializeToXmlAsync<TObject>(TObject objToSerialize) where TObject : class
        {
            var xmlSerializer = new XmlSerializer(typeof(TObject));
            using (var textWriter = new StringWriter())
            {
                if (objToSerialize == null)
                    return Task.FromResult("");

                xmlSerializer.Serialize(textWriter, objToSerialize);
                var xmlString = textWriter.ToString();
                return Task.FromResult(xmlString);
            }
        }

        /// <inheritdoc />
        public Task<TObject> DeserializeFromXmlAsync<TObject>(string xmlString) where TObject : class
        {
            var xmlSerializer = new XmlSerializer(typeof(TObject));
            using (var textReader = new StringReader(xmlString))
            {
                var deserializedObject = xmlSerializer.Deserialize(textReader) as TObject;
                return Task.FromResult(deserializedObject);
            }
        }
    }
}