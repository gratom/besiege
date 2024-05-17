using System;
using UnityEngine;

namespace Tools
{
    [Serializable]
    public struct RectStruct : IRectComponent
    {
        public RectTransform RectTransform => rectTransform;
        [SerializeField] private RectTransform rectTransform;

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

        public RectStruct(RectTransform rectTransform)
        {
            this.rectTransform = rectTransform;
        }

        public RectStruct(IRectComponent rectComponent)
        {
            rectTransform = rectComponent.RectTransform;
        }
    }
}
