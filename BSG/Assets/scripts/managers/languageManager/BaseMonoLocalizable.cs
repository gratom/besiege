﻿using UnityEngine;

namespace Global.Components.Localization
{
    using Managers.Localization;
    using Managers.Datas;

    public abstract class BaseMonoLocalizable : MonoBehaviour
    {
        [SerializeField] protected string key = "";

        private void Start()
        {
            Services.GetManager<DataManager>().DynamicData.Settings.OnLanguageChange += OnLanguageChange;
            LanguageChangeAction(Services.GetManager<LanguageManager>().GetTextByID(key));
        }

        private void OnDestroy()
        {
            Services.GetManager<DataManager>().DynamicData.Settings.OnLanguageChange -= OnLanguageChange;
        }

        private void OnLanguageChange(GT.Language language)
        {
            if (this != null)
            {
                LanguageChangeAction(Services.GetManager<LanguageManager>().GetTextByID(key));
            }
        }

        protected abstract void LanguageChangeAction(string newValue);
    }
}
