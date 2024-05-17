using System;

namespace Tools.ScrollComponent
{
    [Serializable]
    public struct Offset
    {
        public float left;
        public float right;
        public float top;
        public float bottom;

        public float Width => left + right;
        public float Height => top + bottom;
    }
}
