using System;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Components.UserInterface
{
    using Global.Managers.UserInterface;
    using Managers.Datas;
    using Tools;

    [NamedBehavior]
    public class MainWindow : BaseCloseableWindow
    {
        protected override Type WindowType => typeof(MainWindow);

        private DataManager dataManager => Services.GetManager<DataManager>();
        private UIManager uiManager => Services.GetManager<UIManager>();

        protected override void OnHide()
        {
        }

        protected override void OnShow()
        {
        }
    }

}
