using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;

#nullable enable

namespace SDKabu.KBootstrap
{
    public class KSerializer
    {
        /// <summary>
        /// Serialize a Dictionary<TKey, TValue> to json string.
        /// </summary>
        /// <param name="_dict">The dict to serialize</param>
        /// <typeparam name="TKey">Key type of the dictionary to serialize</typeparam>
        /// <typeparam name="TValue">Value type of the dictionary to serialize</typeparam>
        /// <returns>The serialized dict</returns>
        public static string SerializeDictionary<TKey,TValue>(Dictionary<TKey, TValue>? _dict)
        {
            
            return _dict == null ? string.Empty : JsonConvert.SerializeObject(_dict);
        }

        /// <summary>
        /// Gather data from a serialized json Dictionary<TKey, TValue> and return it as a Dictionary<string, T>.
        /// </summary>
        /// <param name="_jsonString">The serialized dict</param>
        /// <typeparam name="TKey">Key type of the dictionary to deserialize</typeparam>
        /// <typeparam name="TValue">Value type of the dictionary to deserialize</typeparam>
        /// <returns>The deserialized dict</returns>
        public static Dictionary<TKey, TValue> DeserializeDictionary<TKey, TValue>(string _jsonString)
        {
            return JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(_jsonString) ?? new Dictionary<TKey, TValue>();
        }

    }
}