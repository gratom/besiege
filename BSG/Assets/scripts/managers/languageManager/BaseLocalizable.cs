using System;
using UnityEngine;

namespace Global.Components.Localization
{
    using Managers.Localization;
    using Managers.Datas;

    [Serializable]
    public abstract class BaseLocalizable
    {
        [SerializeField] protected string key = "";

        public void Init()
        {
            Services.GetManager<DataManager>().DynamicData.Settings.OnLanguageChange += OnLanguageChange;
            LanguageChangeAction(Services.GetManager<LanguageManager>().GetTextByID(key));
        }

        public void UnInit()
        {
            Services.GetManager<DataManager>().DynamicData.Settings.OnLanguageChange -= OnLanguageChange;
        }

        private void OnLanguageChange(GT.Language language)
        {
            LanguageChangeAction(Services.GetManager<LanguageManager>().GetTextByID(key));
        }

        protected abstract void LanguageChangeAction(string newValue);
    }
}
