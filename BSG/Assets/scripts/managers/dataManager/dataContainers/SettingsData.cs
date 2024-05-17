using System;
using Global.Managers.Localization;
using UnityEngine;

namespace Global.Managers.Datas
{
    [Serializable]
    public class SettingsData
    {
        [SerializeField] private GT.Language currentLanguage = GT.Language.English;
        [SerializeField] private bool isFirstLoad = true;

        public GT.Language CurrentLanguage
        {
            get => currentLanguage;
            set
            {
                if (currentLanguage != value && value != GT.Language.English)
                {
                    currentLanguage = value;
                    OnLanguageChange?.Invoke(currentLanguage);
                }
            }
        }

        public bool IsFirstLoad
        {
            get => isFirstLoad;
            set => isFirstLoad = value;
        }

        public event Action<GT.Language> OnLanguageChange;
    }
}
