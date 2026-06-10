using GameCore.LoggerService;
using System;
using System.IO;
using System.Linq;

namespace GameCore.GeneralExtensions
{
    public enum EFileOperation
    {
        Failed,
        Ok,
        OkSkipped,
    }

    public static class FileExtensions
    {
        public static EFileOperation TryDeleteFile(string path, out Exception exception, bool logExceptions = true)
        {
            try
            {
                exception = null;
                if (File.Exists(path))
                {
                    File.Delete(path);

                    return EFileOperation.Ok;
                }

                return EFileOperation.OkSkipped;
            }
            catch (Exception e)
            {
                exception = e;
                if (logExceptions)
                    Log.Exception(e);

                return EFileOperation.Failed;
            }
        }

        public static EFileOperation TryCreateDirectory(string path, out Exception exception, bool logExceptions = true)
        {
            try
            {
                exception = null;

                if (Directory.Exists(path))
                    return EFileOperation.OkSkipped;

                DirectoryInfo info = Directory.CreateDirectory(path);

                return info.Exists ? EFileOperation.Ok : EFileOperation.Failed;
            }
            catch (Exception e)
            {
                exception = e;
                if (logExceptions)
                    Log.Exception(e);

                return EFileOperation.Failed;
            }
        }

        public static EFileOperation TryDeleteDirectory(string path, out Exception exception, bool logExceptions = true)
        {
            try
            {
                exception = null;
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);

                    return EFileOperation.Ok;
                }

                return EFileOperation.OkSkipped;
            }
            catch (Exception e)
            {
                exception = e;
                if (logExceptions)
                    Log.Exception(e);

                return EFileOperation.Failed;
            }
        }

        public static string TryReadAllText(string path, out Exception exception, bool logExceptions = true)
        {
            exception = null;
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                exception = e;

                if (logExceptions)
                    Log.Exception(e);
            }

            return null;
        }

        public static byte[] TryReadAllBytes(string path, out Exception exception, bool logExceptions = true)
        {
            exception = null;
            try
            {
                return File.ReadAllBytes(path);
            }
            catch (Exception e)
            {
                exception = e;

                if (logExceptions)
                    Log.Exception(e);
            }

            return null;
        }

        public static bool IsDirectoryEmpty(string path, out Exception exception, bool logExceptions = true)
        {
            try
            {
                exception = null;

                return !Directory.EnumerateFileSystemEntries(path).Any();
            }
            catch (Exception e)
            {
                exception = e;

                if (logExceptions)
                    Log.Exception(e);

                return false;
            }
        }
    }
}
