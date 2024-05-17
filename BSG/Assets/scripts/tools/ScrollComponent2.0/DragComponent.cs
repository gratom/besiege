using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools.ScrollComponent
{
    public class DragComponent : RectComponent, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IScrollHandler
    {
        public event Action<PointerEventData, float> OnDragEvent;
        public event Action<PointerEventData, Vector2> OnBeginDragEvent;
        public event Action<PointerEventData, float> OnEndDragEvent;
        public event Action<PointerEventData, Vector2> OnTryGrab; // попытка взять элемент
        public event Action<PointerEventData, Vector2> OnClickEvent;
        public event Action<PointerEventData, float> OnScrollEvent;

        private AverageVector2 averageImpulse = new AverageVector2();
        private Vector2 startPoint;
        private Vector2 startPointGrab = Vector2.zero;
        private float xTreshhold = 5;
        private bool isDrag = false;

        public Vector2 StartPoint => startPoint;

        private void OnValidate()
        {
            rectTransform.pivot = new Vector2(0, 1);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (startPointGrab == Vector2.zero)
            {
                averageImpulse.AddNext(eventData.delta);
                OnDragEvent?.Invoke(eventData, averageImpulse.Average.y);
                if (Mathf.Abs(averageImpulse.Average.x) > Mathf.Abs(averageImpulse.Average.y * 2) && averageImpulse.Average.x > xTreshhold)
                {
                    startPointGrab = eventData.position;
                    OnTryGrab?.Invoke(eventData, startPointGrab);
                }
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            isDrag = true;
            startPoint = eventData.position;
            averageImpulse.Clear();
            startPointGrab = Vector2.zero;
            OnBeginDragEvent?.Invoke(eventData, startPoint);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDrag = false;
            averageImpulse.AddNext(eventData.delta);
            OnEndDragEvent?.Invoke(eventData, averageImpulse.Average.y);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isDrag)
            {
                OnClickEvent?.Invoke(eventData, eventData.position);
            }
        }

        public void OnScroll(PointerEventData eventData)
        {
            OnScrollEvent?.Invoke(eventData, eventData.scrollDelta.y);
        }
    }
}
