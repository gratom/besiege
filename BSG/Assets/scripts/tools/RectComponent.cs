//developer -> gratomov@gmail.com

using UnityEngine;

namespace Tools
{
    public interface IRectComponent
    {
        RectTransform RectTransform { get; }
        float Width { get; set; }
        float Height { get; set; }
        Vector2 Size { get; set; }

        float XRight { get; }
        float XLeft { get; }
        float YTop { get; }
        float YBottom { get; }

        Vector2 CornerTopLeft { get; }
        Vector2 CornerTopRight { get; }
        Vector2 CornerBottomLeft { get; }
        Vector2 CornerBottomRight { get; }

        Vector2 Position { get; set; }
        Vector2 GlobalPosition { get; set; }

        float OffsetLocalLeft { get; set; }
        float OffsetLocalRight { get; set; }
        float OffsetLocalBottom { get; set; }
        float OffsetLocalTop { get; set; }

        float OffsetGlobalLeft { get; }
        float OffsetGlobalRight { get; }
        float OffsetGlobalBottom { get; }
        float OffsetGlobalTop { get; }
    }

    public enum StartCoordinateType
    {
        BottomLeft, //default
        BottomRight,
        TopLeft,
        TopRight
    }

    [RequireComponent(typeof(RectTransform))]
    public class RectComponent : MonoBehaviour, IRectComponent
    {
#pragma warning disable
        [SerializeField][HideInInspector] protected RectTransform rectTransform;
#pragma warning restore

        public RectTransform RectTransform => rectTransform;

        public Vector2 Size
        {
            get => rectTransform.rect.size;
            set => rectTransform.sizeDelta = value;
        }

        public float XRight => CornerTopRight.x;
        public float XLeft => CornerTopLeft.x;
        public float YTop => CornerTopRight.y;
        public float YBottom => CornerBottomRight.y;

        public Vector2 CornerTopLeft => GetCorners()[1];
        public Vector2 CornerTopRight => GetCorners()[2];
        public Vector2 CornerBottomLeft => GetCorners()[0];
        public Vector2 CornerBottomRight => GetCorners()[3];

        private Vector3[] GetCorners()
        {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            return corners;
        }

        public float Width
        {
            get => rectTransform.rect.size.x;
            set => rectTransform.sizeDelta = new Vector2(value, rectTransform.sizeDelta.y);
        }

        public float Height
        {
            get => rectTransform.rect.size.y;
            set => rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, value);
        }

        public Vector2 Position
        {
            get => rectTransform.anchoredPosition;
            set => rectTransform.anchoredPosition = value;
        }

        public Vector2 GlobalPosition
        {
            get => rectTransform.position;
            set => rectTransform.position = value;
        }

        public float OffsetLocalLeft
        {
            get => rectTransform.offsetMin.x;
            set => rectTransform.offsetMin = new Vector2(value, rectTransform.offsetMin.y);
        }

        public float OffsetLocalRight
        {
            get => rectTransform.offsetMax.x;
            set => rectTransform.offsetMax = new Vector2(-value, rectTransform.offsetMax.y);
        }

        public float OffsetLocalBottom
        {
            get => rectTransform.offsetMin.y;
            set => rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, value);
        }

        public float OffsetLocalTop
        {
            get => rectTransform.offsetMax.y;
            set => rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -value);
        }

        public float OffsetGlobalLeft => GlobalPosition.x - Size.x * rectTransform.pivot.x;

        public float OffsetGlobalRight => Screen.width - GlobalPosition.x - Size.x * (1 - rectTransform.pivot.x);

        public float OffsetGlobalBottom => GlobalPosition.y - Size.y * rectTransform.pivot.y;

        public float OffsetGlobalTop => Screen.height - GlobalPosition.y - Size.y * (1 - rectTransform.pivot.y);

        public Vector2 World2Local(Vector2 worldPos)
        {
            Vector2 ret = new Vector2(worldPos.x - OffsetGlobalLeft, worldPos.y - OffsetGlobalBottom);
            return ret;
        }

        public Vector2 World2Pivot(Vector2 worldPos)
        {
            Vector2 ret = new Vector2(worldPos.x - OffsetGlobalLeft, worldPos.y - OffsetGlobalBottom);
            ret -= Size * rectTransform.pivot;
            return ret;
        }

        public Vector2 Local2World(Vector2 localPos)
        {
            Vector2 ret = new Vector2(localPos.x + OffsetGlobalLeft, localPos.y + OffsetGlobalBottom);
            return ret;
        }

        #region coordinate transformation by corner

        public Vector2 ConvertCoordinateTo(StartCoordinateType cornerType, Vector2 origin)
        {
            switch (cornerType)
            {
                case StartCoordinateType.BottomLeft:
                    return new Vector2(origin.x, origin.y);
                case StartCoordinateType.BottomRight:
                    return new Vector2(Width - origin.x, origin.y);
                case StartCoordinateType.TopLeft:
                    return new Vector2(origin.x, Height - origin.y);
                case StartCoordinateType.TopRight:
                    return new Vector2(Width - origin.x, Height - origin.y);
            }

            return default;
        }

        #endregion

        #region Unity functions

        protected virtual void OnValidate()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        #endregion
    }
}
