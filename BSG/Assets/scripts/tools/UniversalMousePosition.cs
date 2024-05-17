#if UNITY_EDITOR

using System.Runtime.InteropServices;
using System.Drawing;

namespace Tools
{
    public static class UniversalMousePosition
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);

        public static Point GetCursorPosition()
        {
            GetCursorPos(out Point lpPoint);
            return lpPoint;
        }
    }
}

#endif