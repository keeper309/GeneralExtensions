using System;
using Newtonsoft.Json;
using UnityEngine;

namespace GameCore.GeneralExtensions.NewtonsoftJson
{
    public class Color32Converter : ConverterBase<Color32Converter, Color32>
    {
        public override void WriteJson(JsonWriter writer, Color32 value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("r");
            writer.WriteValue(value.r);

            writer.WritePropertyName("g");
            writer.WriteValue(value.g);

            writer.WritePropertyName("b");
            writer.WriteValue(value.b);

            writer.WritePropertyName("a");
            writer.WriteValue(value.a);

            writer.WriteEndObject();
        }

        public override Color32 ReadJson(
            JsonReader reader,
            Type objectType,
            Color32 existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        )
        {
            byte r = 0, g = 0, b = 0, a = 255;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    switch ((string)reader.Value)
                    {
                        case "r":
                            reader.Read();
                            r = Convert.ToByte(reader.Value);

                            break;
                        case "g":
                            reader.Read();
                            g = Convert.ToByte(reader.Value);

                            break;
                        case "b":
                            reader.Read();
                            b = Convert.ToByte(reader.Value);

                            break;
                        case "a":
                            reader.Read();
                            a = Convert.ToByte(reader.Value);

                            break;
                    }
                }
                else if (reader.TokenType == JsonToken.EndObject)
                {
                    break;
                }
            }

            return new Color32(r, g, b, a);
        }
    }
}
