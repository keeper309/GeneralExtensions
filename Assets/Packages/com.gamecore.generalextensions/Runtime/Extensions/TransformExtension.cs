using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace GameCore.GeneralExtensions
{
    public static class TransformExtension
    {
        public static string BuildHierarchyName(this GameObject gameObject, string separator = "/")
        {
            List<string> names = new List<string>(8);

            int charsCount = 0;
            Transform node = gameObject.transform;
            while (node != null)
            {
                charsCount += node.name.Length;
                names.Add(node.name);
                node = node.parent;
            }

            StringBuilder result = new StringBuilder(names.Count * separator.Length + charsCount);
            for (int i = names.Count - 1; i >= 0; i--)
            {
                result.Append(names[i]);
                if (i != 0)
                    result.Append(separator);
            }

            return result.ToString();
        }

        public static Transform GetTopParent(this Transform transform)
        {
            if (!transform.HasParent())
                return null;

            Transform parentNode = transform.parent;
            while (parentNode.HasParent())
            {
                parentNode = parentNode.parent;
            }

            return parentNode;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasParent(this Transform transform)
        {
            return transform.parent != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalPositionX(this Transform t, float x)
        {
            Vector3 pos = t.localPosition;
            pos.x = x;
            t.localPosition = pos;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalPositionY(this Transform t, float y)
        {
            Vector3 pos = t.localPosition;
            pos.y = y;
            t.localPosition = pos;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalPositionZ(this Transform t, float z)
        {
            Vector3 pos = t.localPosition;
            pos.z = z;
            t.localPosition = pos;
        }

        public static string FullTransformName(this Transform t)
        {
            Transform current = t;
            string path = string.Empty;

            while (current != null)
            {
                path = "/" + current.name + path;
                current = current.parent;
            }

            return path;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetGlobalPositionX(this Transform t, float x)
        {
            Vector3 pos = t.position;
            pos.x = x;
            t.position = pos;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetGlobalPositionY(this Transform t, float y)
        {
            Vector3 pos = t.position;
            pos.y = y;
            t.position = pos;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetGlobalPositionZ(this Transform t, float z)
        {
            Vector3 pos = t.position;
            pos.z = z;
            t.position = pos;
        }
    }
}
