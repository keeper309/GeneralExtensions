using System;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;
using ILogger = GameCore.LoggerService.ILogger;

namespace GameCore.GeneralExtensions
{
    public struct StructPlayerPrefsStorage<T>
        where T : struct, IEquatable<T>
    {
        public enum EState : byte
        {
            None = 0,
            Exists,
            Null
        }

        private readonly ILogger _logger;
        private readonly EJsonType _jsonType;
        private readonly string _key;
        private readonly JsonSerializerSettings _serializerSettings;

        private T? _data;

        public EState State { get; private set; }

        public StructPlayerPrefsStorage(
            ILogger logger,
            string playerPrefsKey,
            JsonSerializerSettings serializerSettings = null,
            EJsonType jsonType = EJsonType.Unity
        )
        {
            _logger = logger;
            _jsonType = jsonType;
            _key = playerPrefsKey;
            _serializerSettings = serializerSettings;
            _data = null;
            State = EState.None;

#if UNITY_EDITOR
            _logger.Assert(typeof(T).Attributes.HasFlag(TypeAttributes.Serializable));
#endif
        }

        public void Kill(bool savePlayerPrefs = false)
        {
            if (State != EState.Null)
                ForceSetNull(savePlayerPrefs);
        }

        public T? Get()
        {
            try
            {
                if (State == EState.Exists || State == EState.Null)
                    return _data;

                if (!PlayerPrefs.HasKey(_key))
                {
                    State = EState.Null;

                    return null;
                }

                string json = PlayerPrefs.GetString(_key, null);

                if (json.IsNullOrWhiteSpace())
                {
                    ForceSetNull(false);

                    return null;
                }

                object rawData = json.DeserializeFromJson(typeof(T), _serializerSettings, _jsonType);
                if (rawData != null)
                {
                    _data = (T)rawData;
                    State = EState.Exists;
                }
                else
                {
                    _logger.Error("Deserialize failed with null");
                    ForceSetNull(true);
                }

                return _data;
            }
            catch (Exception e)
            {
                State = EState.None;
                _logger?.Exception(e);

                return null;
            }
        }

        public void DataChanged(bool savePlayerPrefs = false)
        {
            try
            {
                if (_data == null)
                {
                    ForceSetNull(savePlayerPrefs);

                    return;
                }

                string json = _data.Value.SerializeToJson(false, _serializerSettings, _jsonType);
                JsonExtensions.SetPlayerPrefs(_key, json, savePlayerPrefs);
                State = EState.Exists;
            }
            catch (Exception e)
            {
                State = EState.None;
                _logger?.Exception(e);
            }
        }

        public void Set(T? data, bool savePlayerPrefs = false)
        {
            if (IsEqual(data))
                return;

            _data = data;
            DataChanged(savePlayerPrefs);
        }

        private void ForceSetNull(bool savePlayerPrefs)
        {
            JsonExtensions.RemovePlayerPrefs(_key, savePlayerPrefs);
            _data = null;
            State = EState.Null;
        }

        private bool IsEqual(T? data)
        {
            if (_data.HasValue != data.HasValue)
                return false;

            return
                _data != null &&
                data != null &&
                _data.Value.Equals(data.Value);
        }
    }
}
