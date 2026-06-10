using GameCore.LoggerService;
using System;
using UnityEditor;
using UnityEngine;

namespace GameCore.GeneralExtensions
{
    public static class ApplicationHelper
    {
        public static bool? IsPlaying()
        {
            try
            {
                if (ThreadHelper.IsOnMainThread)
                {
#if UNITY_EDITOR
                    if (EditorApplication.isCompiling || EditorApplication.isUpdating)
                    {
                        return false;
                    }
#endif
                    return Application.isPlaying;
                }
            }
            catch (Exception e)
            {
                Log.Exception(e);
            }

            return null;
        }

        public static bool IsEditor()
        {
#if UNITY_EDITOR
            return true;
#else
            return false;
#endif
        }

        public static bool IsAndroid(bool onlyDevice)
        {
            if (onlyDevice && IsEditor())
                return false;

#if UNITY_ANDROID
            return true;
#else
            return false;
#endif
        }

        public static bool IsIos(bool onlyDevice)
        {
            if (onlyDevice && IsEditor())
                return false;

#if UNITY_IOS
            return true;
#else
            return false;
#endif
        }

        public static bool IsAndroidOrIos(bool onlyDevice)
        {
            return IsAndroid(onlyDevice) || IsIos(onlyDevice);
        }
    }
}
