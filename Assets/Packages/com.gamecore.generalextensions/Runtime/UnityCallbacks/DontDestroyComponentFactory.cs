using UnityEngine;

namespace GameCore.GeneralExtensions
{
    public static class DontDestroyComponentFactory
    {
        public static TMono CreateInstance<TMono>(string nameSuffix = "")
            where TMono : MonoBehaviour
        {
            string name = typeof(TMono).Name + nameSuffix;
            TMono result = new GameObject(name).AddComponent<TMono>();

            Object.DontDestroyOnLoad(result.gameObject);

            return result;
        }
    }
}
