using UnityEngine;

namespace Tools
{
    public static class ColorExtentions
    {
        public static string ToHtmlStringRGBA(this Color color)
        {
            int r = (int)(color.r * 255);
            int g = (int)(color.g * 255);
            int b = (int)(color.b * 255);
            int a = (int)(color.a * 255);

            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", r, g, b, a);
        }
    }
}
