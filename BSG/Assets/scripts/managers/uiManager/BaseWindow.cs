using System;
using UnityEngine;

namespace Global.Components.UserInterface
{
    [RequireComponent(typeof(Canvas))]
    public abstract class BaseWindow : MonoBehaviour
    {
        [SerializeField] protected Canvas canvas;

        public int Order
        {
            get => canvas.sortingOrder;
            set => canvas.sortingOrder = value;
        }

        private void OnValidate()
        {
            canvas = GetComponent<Canvas>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            OnShow();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            OnHide();
        }

        protected abstract void OnHide();
        protected abstract void OnShow();
    }
}
