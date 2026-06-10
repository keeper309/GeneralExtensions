using System;
using Newtonsoft.Json;

namespace GameCore.GeneralExtensions
{
    public interface IJsonExportable
    {
        Type SerializedType { get; }
        EJsonType JsonType { get; }

        void ImportFromJson(string json, JsonSerializerSettings settings, EJsonType jsonType);
        string ExportToJson(bool prettyPrint, JsonSerializerSettings settings, EJsonType jsonType);
    }
}
