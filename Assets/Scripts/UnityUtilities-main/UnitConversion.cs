using UnityEngine;

namespace Tools.UnityUtilities
{
    public static class UnitConversion
    {
        public const float InchToCM = 2.54f;
        public const float CMToInch = 0.3937007874015748f;

        public static float ScreenPixelsToInches(float pixels)
        {
            return pixels / Screen.dpi;
        }

        public static float ScreenPixelsToCM(float pixels)
        {
            return InchesToCM(ScreenPixelsToInches(pixels));
        }

        public static float InchesToCM(float inches)
        {
            return inches * InchToCM;
        }

        public static float CMToInches(float cms)
        {
            return cms * CMToInch;
        }
    }
}
