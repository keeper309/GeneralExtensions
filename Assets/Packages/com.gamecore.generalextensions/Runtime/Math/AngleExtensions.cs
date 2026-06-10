using GameCore.LoggerService;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GameCore.GeneralExtensions
{
    public static class AngleExtensions
    {
        // returns 0..360 corrected angle
        public static float GetZAngle_0_360(Vector3 dir, Vector3 axisDir)
        {
            Log.Assert(
                Mathf.Approximately(dir.z, 0) &&
                Mathf.Approximately(axisDir.z, 0)
            );

            float result = Vector3.Angle(axisDir, dir);
            if (dir.x > 0)
                result = 360 - result;

            return result;
        }

        public static float GetZAngle_0_360(Vector3 fromVector3, Vector3 toVector3, Vector3 axisDir)
        {
            return GetZAngle_0_360(
                GetZAngle_0_360(fromVector3, axisDir),
                toVector3,
                axisDir
            );
        }

        public static float GetZAngle_0_360(float from, Vector3 toVector3, Vector3 axisDir)
        {
            return CorrectAngleTo_0_360(GetZAngle_0_360(toVector3, axisDir) - from);
        }

        public static float CorrectAngleTo_m180_p180(float angle)
        {
            angle = CorrectAngleTo_0_360(angle);
            if (angle >= 180)
                angle -= 360;

            if (angle <= -180)
                angle += 360;

            return angle;
        }

        public static float CorrectAngleTo_0_360(float angle)
        {
            if (angle < 0)
                angle += 360 * (1 + AngleRotationsCount(angle));
            else if (angle >= 360)
                angle -= 360 * AngleRotationsCount(angle);

            return angle;
        }

        public static float AngleRotationsCount(float angle)
        {
            return Mathf.FloorToInt(Mathf.Abs(angle) / 360.0f);
        }

        public static float CorrectAngle_m180_p180(float angle)
        {
            while (angle < -180)
            {
                angle += 360;
            }

            while (angle > 180)
            {
                angle -= 360;
            }

            return angle;
        }

        public static float FlatAngleY(Vector3 to)
        {
            Vector2 to2 = new Vector2(to.x, to.z).normalized;
            float angle = Vector2.Angle(Vector2.up, to2);
            if (to2.x < 0)
                angle = 360 - angle;

            return angle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion RandomYAngle()
        {
            return Quaternion.Euler(0, Random.Range(0, 360), 0);
        }
    }
}
