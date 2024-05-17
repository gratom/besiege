using System;

namespace Tools
{

    public interface IUIPanel
    {
        bool IsOpen { get; }
        bool DefaultState { get; }

        void Open();
        void Close();
        void Switch();
        void ToDefault();
    }

    public class RectUIPanel : RectComponent, IUIPanel
    {
        private bool defaultState;

        protected override void OnValidate()
        {
            base.OnValidate();
        }

        private void Awake()
        {
            defaultState = gameObject.activeSelf;
        }

        #region panel

        public bool IsOpen
        {
            get => gameObject.activeSelf;
            private set => gameObject.SetActive(value);
        }

        public bool DefaultState
        {
            get => defaultState;
            private set => defaultState = value;
        }

        public void Open()
        {
            IsOpen = true;
            OnOpen();
        }

        public void Close()
        {
            IsOpen = false;
            OnClose();
        }

        public void Switch()
        {
            IsOpen = !IsOpen;
            OnSwitch();
        }

        public void ToDefault()
        {
            IsOpen = defaultState;
            OnDefault();
        }

        protected virtual void OnOpen() { }
        protected virtual void OnClose() { }
        protected virtual void OnSwitch() { }
        protected virtual void OnDefault() { }

        #endregion
    }
}
