using System.Runtime.CompilerServices;
using UnityEngine;

namespace GameCore.GeneralExtensions
{
    public static class RectExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rect ScaleSizeBy(this Rect rect, float scale)
        {
            return rect.ScaleSizeBy(scale, rect.center);
        }

        public static Rect ScaleSizeBy(this Rect rect, float scale, Vector2 pivotPoint)
        {
            Rect result = rect;
            result.x -= pivotPoint.x;
            result.y -= pivotPoint.y;

            result.xMin *= scale;
            result.xMax *= scale;
            result.yMin *= scale;
            result.yMax *= scale;

            result.x += pivotPoint.x;
            result.y += pivotPoint.y;

            return result;
        }

        //
        // Summary:
        //     Returns a Rect, with the specified width, that has been aligned to the right
        //     of the original Rect.
        //
        // Parameters:
        //   rect:
        //     The original Rect.
        //
        //   width:
        //     The desired width of the new Rect.
        public static Rect AlignRight(this Rect rect, float width)
        {
            rect.x = rect.x + rect.width - width;
            rect.width = width;
            return rect;
        }

        //
        // Summary:
        //     Subtracts a Rect's X max position.
        //
        // Parameters:
        //   rect:
        //     The original Rect.
        //
        //   value:
        //     The value to subtract.
        public static Rect SubXMax(this Rect rect, float value)
        {
            rect.xMax -= value;
            return rect;
        }
    }
}
