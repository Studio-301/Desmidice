using System.Collections;
using UnityEngine;

namespace Tools.UnityUtilities
{
    public class Integrity
    {
        public static float Sanitize(float f) => (float.IsNaN(f) || float.IsInfinity(f)) ? 0 : f;
        public static Vector2 Sanitize(Vector2 v2)
        {
            v2.x = Sanitize(v2.x);
            v2.y = Sanitize(v2.y);
            return v2;
        }
        public static Vector3 Sanitize(Vector3 v3)
        {
            v3.x = Sanitize(v3.x);
            v3.y = Sanitize(v3.y);
            v3.z = Sanitize(v3.z);
            return v3;
        }



        public static void Sanitize(ref float f) { if (float.IsNaN(f) || float.IsInfinity(f)) f = 0; }
        public static void Sanitize(ref Vector2 v2)
        {
            Sanitize(ref v2.x);
            Sanitize(ref v2.y);
        }
        public static void Sanitize(ref Vector3 v3)
        {
            Sanitize(ref v3.x);
            Sanitize(ref v3.y);
            Sanitize(ref v3.z);
        }
    }
}