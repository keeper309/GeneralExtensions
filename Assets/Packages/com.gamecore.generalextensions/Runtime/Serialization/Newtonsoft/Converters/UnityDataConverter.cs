using Newtonsoft.Json;

namespace GameCore.GeneralExtensions.NewtonsoftJson
{
    public static class UnityDataConverterExtensions
    {
        public static JsonSerializerSettings EnrichWithUnityDataConverters(this JsonSerializerSettings settings)
        {
            settings ??= new JsonSerializerSettings();

            settings.Converters.AddExclusive(new ColorConverter());
            settings.Converters.AddExclusive(new Color32Converter());

            settings.Converters.AddExclusive(new Vector2Converter());
            settings.Converters.AddExclusive(new Vector3Converter());
            settings.Converters.AddExclusive(new Vector4Converter());

            settings.Converters.AddExclusive(new Vector2IntConverter());
            settings.Converters.AddExclusive(new Vector3IntConverter());

            settings.Converters.AddExclusive(new QuaternionConverter());

            return settings;
        }
    }
}