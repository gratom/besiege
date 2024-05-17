using UnityEngine;

namespace Tools
{
    public static class Vector2Tool
    {
        public static Vector2 ScreenToLocalByRect(this Vector2 origin, RectComponent rect)
        {
            return rect.World2Local(origin);
        }

        public static Vector2Int ToInt(this Vector2 origin)
        {
            return new Vector2Int(Mathf.RoundToInt(origin.x), Mathf.RoundToInt(origin.y));
        }

    }
}
