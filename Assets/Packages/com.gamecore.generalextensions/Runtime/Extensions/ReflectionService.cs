using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCore.GeneralExtensions
{
    public class ReflectionService
    {
        public enum CachePolicy
        {
            NoCaching,
            ShouldCache
        }

        private readonly CachePolicy _cachePolicy;
        private Type[] _types;

        public IEnumerable<Type> Types
        {
            get
            {
                if (_cachePolicy == CachePolicy.ShouldCache)
                {
                    return _types ??= SelectAllTypes().ToArray();
                }

                return SelectAllTypes();
            }
        }

        public ReflectionService(CachePolicy policy = CachePolicy.NoCaching)
        {
            _cachePolicy = policy;
        }

        public IEnumerable<Type> GetNonAbstractDerivedTypes(Type interfaceType)
        {
            //Log.Assert( interfaceType.IsAbstract || interfaceType.IsInterface );

            IEnumerable<Type> types = Types.Where(type => interfaceType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract);

            return types;
        }

        public IEnumerable<Type> GetNonAbstractDerivedTypes<T>()
        {
            return GetNonAbstractDerivedTypes(typeof(T));
        }

        public Type GetFirstTypeByName(string name)
        {
            return Types.FirstOrDefault(type => type.Name == name);
        }

        public IEnumerable<Type> GetTypesByName(string name)
        {
            return Types.Where(type => type.Name == name);
        }

        private IEnumerable<Type> SelectAllTypes()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes());
        }
    }
}
