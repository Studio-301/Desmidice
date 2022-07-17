using System.Collections;
using UnityEngine;

namespace Tools.UnityUtilities
{
    public static class MathfUtilities
    {
        public static float RepeatValue(float value, float min, float max)
        {
            return (((value - min) % (max - min)) + (max - min)) % (max - min) + min;
        }

        /// <summary>
        /// Method that helps me remap number from one range to another (ex. .5 in 0-1 to 1 in 0-2)
        /// </summary>
        public static float RemapValue(float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
        public static float Remap(this float value, float from1, float to1, float from2, float to2) => RemapValue(value, from1, to1, from2, to2);
    }
}