using System;
using Newtonsoft.Json;

namespace GameCore.GeneralExtensions.NewtonsoftJson
{
    public abstract class ConverterBase<TSuperClass, TConvertedType>
        : JsonConverter<TConvertedType>, IEquatable<TSuperClass>
        where TSuperClass : class
    {
        public bool Equals(TSuperClass that)
        {
            return that != null;
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }

        public override bool Equals(object that)
        {
            return that != null && that.GetType() == GetType();
        }
    }
}
