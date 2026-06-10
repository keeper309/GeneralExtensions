using System;
using Newtonsoft.Json;
using UnityEngine;

namespace GameCore.GeneralExtensions.NewtonsoftJson
{
    public class Vector3IntConverter : ConverterBase<Vector3IntConverter, Vector3Int>
    {
        public override void WriteJson(JsonWriter writer, Vector3Int value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("x");
            writer.WriteValue(value.x);

            writer.WritePropertyName("y");
            writer.WriteValue(value.y);

            writer.WritePropertyName("z");
            writer.WriteValue(value.z);

            writer.WriteEndObject();
        }

        public override Vector3Int ReadJson(
            JsonReader reader,
            Type objectType,
            Vector3Int existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        )
        {
            int x = 0, y = 0, z = 0;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    switch ((string)reader.Value)
                    {
                        case "x":
                            reader.Read();
                            x = Convert.ToInt32(reader.Value);

                            break;
                        case "y":
                            reader.Read();
                            y = Convert.ToInt32(reader.Value);

                            break;
                        case "z":
                            reader.Read();
                            z = Convert.ToInt32(reader.Value);

                            break;
                    }
                }
                else if (reader.TokenType == JsonToken.EndObject)
                {
                    break;
                }
            }

            return new Vector3Int(x, y, z);
        }
    }
}
