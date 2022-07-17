using System.Collections;
using UnityEngine;

namespace Tools.UnityUtilities
{
    public static class QuaternionExtentions
    {

        public static Quaternion GetXAxisRotation(this Quaternion quaternion)
        {
            float a = Mathf.Sqrt((quaternion.w * quaternion.w) + (quaternion.x * quaternion.x));
            return new Quaternion(x: quaternion.x, y: 0, z: 0, w: quaternion.w / a);

        }

        public static Quaternion GetYAxisRotation(this Quaternion quaternion)
        {
            float a = Mathf.Sqrt((quaternion.w * quaternion.w) + (quaternion.y * quaternion.y));
            return new Quaternion(x: 0, y: quaternion.y, z: 0, w: quaternion.w / a);

        }

        public static Quaternion GetZAxisRotation(this Quaternion quaternion)
        {
            float a = Mathf.Sqrt((quaternion.w * quaternion.w) + (quaternion.z * quaternion.z));
            return new Quaternion(x: 0, y: 0, z: quaternion.z, w: quaternion.w / a);
        }
    }

}