using System;
using Global.Managers.UserInterface;

namespace Global.Components.UserInterface
{
    public abstract class BaseCloseableWindow : BaseWindow
    {
        protected abstract Type WindowType { get; }

        #region buttons functions

        public void HideWindow()
        {
            Services.GetManager<UIManager>().HideWindow(WindowType);
        }

        #endregion
    }
}
