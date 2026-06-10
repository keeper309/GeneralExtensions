using System;
using System.Runtime.CompilerServices;
using GameCore.LoggerService;
using Newtonsoft.Json;
using UnityEngine;
using ILogger = GameCore.LoggerService.ILogger;

namespace GameCore.GeneralExtensions
{
    public static class JsonExtensions
    {
        private const string NullSerialized = "null";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPlayerPrefs(string key, string data, bool savePlayerPrefs)
        {
            PlayerPrefs.SetString(key, data);

            if (savePlayerPrefs)
            {
                PlayerPrefs.Save();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemovePlayerPrefs(string key, bool savePlayerPrefs)
        {
            PlayerPrefs.DeleteKey(key);

            if (savePlayerPrefs)
            {
                PlayerPrefs.Save();
            }
        }

        public static object DeserializeFromJson(
            this string text,
            Type type,
            JsonSerializerSettings settings = null,
            EJsonType jsonType = EJsonType.Newtonsoft,
            bool throwWhenFail = true,
            ILogger logger = null
        )
        {
            if (text.IsNullOrWhiteSpace())
            {
                if (throwWhenFail)
                {
                    throw new Exception($"empty string to deserialize: {type.FullName}");
                }
                LogError(logger, "text is empty event to try to deserialize");

                return null;
            }

            if (text.Equals(NullSerialized))
            {
                return null;
            }

            try
            {
                switch (jsonType)
                {
                    case EJsonType.Newtonsoft: return JsonConvert.DeserializeObject(text, type, settings);

                    case EJsonType.Unity: return JsonUtility.FromJson(text, type);

                    default: throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                if (throwWhenFail)
                    throw;

                LogException(logger, e, $"exception when {nameof(DeserializeFromJson)}");

                return null;
            }
        }

        public static string SerializeToJson(
            this object data,
            bool prettyPrint,
            JsonSerializerSettings settings,
            EJsonType jsonType = EJsonType.Newtonsoft,
            bool throwWhenFail = true,
            ILogger logger = null
        )
        {
            if (data == null)
            {
                return NullSerialized;
            }

            try
            {
                Formatting formatting = prettyPrint ? Formatting.Indented : Formatting.None;
                string result = null;

                switch (jsonType)
                {
                    case EJsonType.Newtonsoft:
                        result = JsonConvert.SerializeObject(data, formatting, settings);

                        break;

                    case EJsonType.Unity:
                        result = JsonUtility.ToJson(data, prettyPrint);

                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (result.IsNullOrWhiteSpace())
                {
                    LogError(logger, $"failed to serialize: {data.GetType().FullName}");
                }

                return result;
            }
            catch (Exception e)
            {
                if (throwWhenFail)
                    throw;

                LogException(logger, e, $"exception when {nameof(SerializeToJson)}");

                return null;
            }
        }

        private static void LogException(ILogger logger, Exception e, string message)
        {
            if (logger != null)
            {
                logger.Exception(e, message);
            }
            else
            {
                Log.Exception(e);
                Log.Error(message);
            }
        }

        private static void LogError(ILogger logger, string message)
        {
            if (logger != null)
            {
                logger.Error(message);
            }
            else
            {
                Log.Error(message);
            }
        }
    }
}
