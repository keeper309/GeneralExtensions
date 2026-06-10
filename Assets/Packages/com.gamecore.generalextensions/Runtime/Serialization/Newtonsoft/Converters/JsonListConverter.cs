using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace GameCore.GeneralExtensions.NewtonsoftJson
{
    [Preserve]
    public class JsonListConverter<T> : JsonConverter<JsonList<T>>
    {
        //do not remove - need for and\ios
        [JsonConstructor]
        public JsonListConverter() { }

        public override void WriteJson(JsonWriter writer, JsonList<T> value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (!value.data.IsNullOrEmpty())
            {
                for (int i = 0; i < value.data.Count; i++)
                {
                    writer.WritePropertyName(i.ToString());
                    serializer.Serialize(writer, value.data[i]);
                }
            }

            writer.WriteEndObject();
        }

        public override JsonList<T> ReadJson(
            JsonReader reader,
            Type objectType,
            JsonList<T> existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        )
        {
            List<Pair> pairList = new();

            if (reader.TokenType != JsonToken.StartObject)
            {
                throw new JsonSerializationException("Expected StartObject for list-as-dictionary.");
            }

            while (reader.Read())
            {
                JsonToken tokenType = reader.TokenType;

                if (tokenType == JsonToken.EndObject)
                    break;

                if (tokenType == JsonToken.PropertyName)
                {
                    string keyStr = (string)reader.Value;
                    if (!int.TryParse(keyStr, out int index))
                    {
                        throw new JsonSerializationException($"Invalid list index key: {keyStr}");
                    }

                    reader.Read();
                    T item = serializer.Deserialize<T>(reader);
                    pairList.Add(new Pair(index, item));
                }
            }

            pairList.Sort((a, b) => a.Id - b.Id);

            JsonList<T> result = new(pairList.LastObject().Id + 1);

            foreach (Pair pair in pairList)
            {
                while (result.data.Count < pair.Id)
                {
                    result.data.Add(default);
                }

                result.data.Add(pair.Value);
            }

            return result;
        }

        private readonly struct Pair
        {
            public readonly int Id;
            public readonly T Value;

            public Pair(int id, T value)
            {
                Id = id;
                Value = value;
            }
        }
    }
}
