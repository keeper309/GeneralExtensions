using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GameCore.GeneralExtensions
{
    public static class VectorExtension
    {
        public const float Eps = 0.001f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XY2XZ(this Vector2 vec2, float y = 0)
        {
            return new Vector3(vec2.x, y, vec2.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ToVector2(this Vector3 vec3)
        {
            return vec3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Divide(this Vector2 divided, Vector2 divider)
        {
            return new Vector2(divided.x / divider.x, divided.y / divider.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceSqr(Vector3 pos0, Vector3 pos1)
        {
            Vector3 dir = pos1 - pos0;

            return dir.x * dir.x + dir.y * dir.y + dir.z * dir.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNotZero(this Vector3 vec3, float eps = Eps)
        {
            return Math.Abs(vec3.x) + Math.Abs(vec3.y) + Math.Abs(vec3.z) > eps;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNotZeroXZ(this Vector3 vec3, float eps = Eps)
        {
            return Math.Abs(vec3.x) + Math.Abs(vec3.z) > eps;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroXZ(this Vector3 vec3, float eps = Eps)
        {
            return Math.Abs(vec3.x) + Math.Abs(vec3.z) < eps;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MagnitudeSqrXZ(this Vector3 vec3)
        {
            return vec3.x * vec3.x + vec3.z * vec3.z;
        }

        public static void AccelerateXY(ref Vector2 velocity, Vector2 frameAcceleration, float maxSpeed)
        {
            velocity += frameAcceleration;

            float xzSpeed = velocity.magnitude;
            if (xzSpeed > maxSpeed)
            {
                float factor = maxSpeed / xzSpeed;
                velocity *= factor;
            }
        }

        public static void AccelerateXZ(ref Vector3 velocity, Vector3 frameAcceleration, float maxSpeed)
        {
            velocity += frameAcceleration;

            float xzSpeed = velocity.MagnitudeXZ();
            if (xzSpeed > maxSpeed)
            {
                float factor = maxSpeed / xzSpeed;
                velocity.x *= factor;
                velocity.z *= factor;
            }
        }

        public static void ClampLength(ref Vector3 vector, float maxLength)
        {
            float lengthSqr = vector.sqrMagnitude;
            float maxLengthSqr = maxLength * maxLength;

            if (lengthSqr > maxLengthSqr)
            {
                vector *= maxLength / Mathf.Sqrt(lengthSqr);
            }
        }

        public static void DecelerateXZ(ref Vector3 velocity, float frameDeceleration)
        {
            float velocityXZMagnitude = velocity.MagnitudeXZ();
            float newVelocityXZMagnitude = velocityXZMagnitude - frameDeceleration;

            if (newVelocityXZMagnitude < 0)
            {
                velocity.x = 0;
                velocity.z = 0;
            }
            else
            {
                float factor = newVelocityXZMagnitude / velocityXZMagnitude;
                velocity.x *= factor;
                velocity.z *= factor;
            }
        }

        public static void DecelerateXY(ref Vector2 velocity, float frameDeceleration)
        {
            float velocityMagnitude = velocity.magnitude;
            float newVelocityMagnitude = velocityMagnitude - frameDeceleration;

            if (newVelocityMagnitude < 0)
            {
                velocity = Vector2.zero;
            }
            else
            {
                velocity *= newVelocityMagnitude / velocityMagnitude;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MagnitudeXZ(this Vector3 vec3)
        {
            return Mathf.Sqrt(MagnitudeSqrXZ(vec3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToVector3(this Vector2 vec2)
        {
            return vec2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Add(this Vector3 thisVector, Vector2 vec2)
        {
            return thisVector + vec2.ToVector3();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Add(this Vector2 thisVector, Vector3 vec3)
        {
            return thisVector + vec3.ToVector2();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Sub(this Vector3 thisVector, Vector2 vec2)
        {
            return thisVector - vec2.ToVector3();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Sub(this Vector2 thisVector, Vector3 vec3)
        {
            return thisVector - vec3.ToVector2();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Translate(this Vector3 vector, float deltaX, float deltaY, float deltaZ)
        {
            vector.x += deltaX;
            vector.y += deltaY;
            vector.z += deltaZ;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Translate(this Vector3 vector, Vector3 offset)
        {
            vector.x += offset.x;
            vector.y += offset.y;
            vector.z += offset.z;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Translate(this Vector3 vector, Vector2 offset)
        {
            vector.x += offset.x;
            vector.y += offset.y;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Translate(this Vector2 vector, float deltaX, float deltaY)
        {
            vector.x += deltaX;
            vector.y += deltaY;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Translate(this Vector2 vector, Vector2 offset)
        {
            vector.x += offset.x;
            vector.y += offset.y;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 SetX(this Vector3 vector, float x)
        {
            vector.x = x;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 SetY(this Vector3 vector, float y)
        {
            vector.y = y;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 SetZ(this Vector3 vector, float z)
        {
            vector.z = z;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 SetX(this Vector2 vector, float x)
        {
            vector.x = x;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 SetY(this Vector2 vector, float y)
        {
            vector.y = y;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 FlipX(this Vector3 vector)
        {
            vector.x = -vector.x;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 FlipY(this Vector3 vector)
        {
            vector.y = -vector.y;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 FlipZ(this Vector3 vector)
        {
            vector.z = -vector.z;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 FlipX(this Vector2 vector)
        {
            vector.x = -vector.x;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 FlipY(this Vector2 vector)
        {
            vector.y = -vector.y;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Translate(this Vector2 vector, float deltaX, float deltaY, float deltaZ)
        {
            return new Vector3(vector.x + deltaX, vector.y + deltaY, deltaZ);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CompareTo(this Vector3 a, Vector3 b, float epsilon = float.Epsilon)
        {
            return Math.Abs(a.x - b.x) < epsilon && Math.Abs(a.y - b.y) < epsilon && Math.Abs(a.z - b.z) < epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CompareTo(this Vector2 a, Vector2 b, float epsilon = float.Epsilon)
        {
            return Math.Abs(a.x - b.x) < epsilon && Math.Abs(a.y - b.y) < epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Clamp(this Vector2 v, float a, float b)
        {
            v.x = Mathf.Clamp(v.x, a, b);
            v.y = Mathf.Clamp(v.y, a, b);

            return v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Clamp(this Vector3 v, float a, float b)
        {
            v.x = Mathf.Clamp(v.x, a, b);
            v.y = Mathf.Clamp(v.y, a, b);
            v.z = Mathf.Clamp(v.z, a, b);

            return v;
        }
    }
}
