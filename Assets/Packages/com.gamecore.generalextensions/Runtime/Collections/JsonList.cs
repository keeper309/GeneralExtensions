using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Scripting;

namespace GameCore.GeneralExtensions
{
    //used for special key-value serialization as dictionary in json format.
    [Preserve]
    [Serializable]
    public class JsonList<TItem>
    {
        private static HashSet<int> _verifiedTypeHashCodes = new(32);

        //do not iherit from List<T>!
        //unity doesn't support raw-serialization in that case
        [SerializeField] public List<TItem> data;

        public TItem this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }

        //do not remove - need for and\ios
        [JsonConstructor]
        public JsonList()
        {
            data = new List<TItem>(4);
        }

        public JsonList(int capacity = 4)
        {
            Validate();
            data = new List<TItem>(capacity);
        }

        public JsonList(List<TItem> list, bool clone = true)
        {
            Validate();
            data = clone ? new List<TItem>(list) : list;
        }

        public JsonList(IEnumerable<TItem> iterator)
        {
            Validate();
            data = iterator.ToList();
        }

        public override string ToString()
        {
            if (data == null)
                return "null";

            if (data.Count == 0)
                return "empty";

            StringBuilder sb = new(data.Count * 16);
            foreach (TItem item in data)
            {
                sb.Append(item);
                sb.Append(", ");
            }

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            if (data.IsNullOrEmpty())
                return 0;

            HashCode hash = new HashCode();
            foreach (TItem item in data)
            {
                hash.Add(item);
            }

            return hash.ToHashCode();
        }

        public static JsonList<TItem> TransformCreate<TItemRaw>(IEnumerable<TItemRaw> iterator, Func<TItemRaw, TItem> transform)
        {
            List<TItem> list = new(iterator.Count());
            foreach (TItemRaw itemRaw in iterator)
            {
                list.Add(transform(itemRaw));
            }

            return new JsonList<TItem>(list, false);
        }

        public void Add(TItem item)
        {
            data.Add(item);
        }

        public bool Remove(TItem item)
        {
            return data.Remove(item);
        }

        private void Validate()
        {
            Type itemType = typeof(TItem);
            int hashcode = itemType.GetHashCode();

            if (!_verifiedTypeHashCodes.Add(hashcode))
                return;

            if (!itemType.IsDefined(typeof(SerializableAttribute), false))
            {
                throw new InvalidOperationException(
                    $"Type {itemType} must be marked with [Serializable], needed for unity-serialization optimization"
                );
            }
        }
    }
}
