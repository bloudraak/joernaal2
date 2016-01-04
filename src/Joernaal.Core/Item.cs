using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using Joernaal.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Joernaal
{
    /// <summary>
    ///     Represents an item in a project
    /// </summary>
    public abstract class Item
    {
        private static readonly Lazy<JsonSerializer> SerializerLazy;

        private IDictionary<string, string> _metadata;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Item" /> class.
        /// </summary>
        protected Item()
        {
            _metadata = new Dictionary<string, string>();
        }

        static Item()
        {
            SerializerLazy = new Lazy<JsonSerializer>(CreateSerializer);
        }

        /// <summary>
        ///     Gets the metadata of an item
        /// </summary>
        public IDictionary<string, string> Metadata
        {
            get
            {
                if (_metadata == null)
                {
                    _metadata = new Dictionary<string, string>();
                }
                return _metadata;
            }
            set { _metadata = value; }
        }

        /// <summary>
        ///     Gets or sets the kry of the item
        /// </summary>
        [JsonIgnore]
        public string Key { get; set; }

        /// <summary>
        ///     Serializes the item metadata to disk
        /// </summary>
        /// <param name="writer"></param>
        public void Serialize(TextWriter writer)
        {
            using (JsonWriter jsonWriter = new JsonTextWriter(writer))
            {
                Serializer.Serialize(jsonWriter, this);
            }
        }

        /// <summary>
        /// Gets the serializer
        /// </summary>
        protected static JsonSerializer Serializer
        {
            get { return SerializerLazy.Value; }
        }

        /// <summary>
        /// Creates a serializer
        /// </summary>
        /// <returns>An instance of <see cref="JsonSerializer"/></returns>
        private static JsonSerializer CreateSerializer()
        {
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Binder = new KindSerializationBinder();
            serializer.MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead;
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            //serializer.Formatting = Formatting.Indented;
            return serializer;
        }

        public static T Deserialize<T>(TextReader textReader)
        {
            using (JsonReader reader = new JsonTextReader(textReader))
            {
                return Serializer.Deserialize<T>(reader);
            }
        }
    }
}