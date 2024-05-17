using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Global.Managers.Localization
{
    using Datas;

    public class LanguageManager : BaseManager
    {
        public override Type ManagerType => typeof(LanguageManager);

        private static Dictionary<SystemLanguage, Action> languageAutosetActionsDictionary = new Dictionary<SystemLanguage, Action>()
        {
            { SystemLanguage.Russian, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Russian },
            { SystemLanguage.Belarusian, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Russian },
            { SystemLanguage.Ukrainian, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Ukrainian },
            { SystemLanguage.French, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.French },
            { SystemLanguage.Portuguese, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Portuguese },
            { SystemLanguage.German, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.German },
            { SystemLanguage.Arabic, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Arabic },
            { SystemLanguage.Danish, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Danish },
            { SystemLanguage.Finnish, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Finnish },
            { SystemLanguage.Dutch, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Dutch },
            { SystemLanguage.Greek, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Greek },
            { SystemLanguage.Italian, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Italian },
            { SystemLanguage.Japanese, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Japanese },
            { SystemLanguage.Korean, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Korean },
            { SystemLanguage.Norwegian, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Norwegian },
            { SystemLanguage.Polish, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Polish },
            { SystemLanguage.Spanish, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Spanish },
            { SystemLanguage.Swedish, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Swedish },
            { SystemLanguage.Turkish, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.Turkish },
            { SystemLanguage.Chinese, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.ChineseSimplified },
            { SystemLanguage.ChineseSimplified, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.ChineseSimplified },
            { SystemLanguage.ChineseTraditional, () => Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.ChineseSimplified }
        };

#pragma warning disable

        protected override async Task<bool> OnInit()
        {
            return true;
        }

#pragma warning restore

        public void RefreshLanguages()
        {
            if (Services.GetManager<DataManager>().DynamicData.Settings.IsFirstLoad)
            {
                Services.GetManager<DataManager>().DynamicData.Settings.IsFirstLoad = false;
                if (languageAutosetActionsDictionary.ContainsKey(Application.systemLanguage))
                {
                    languageAutosetActionsDictionary[Application.systemLanguage]();
                    return;
                }
                Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage = GT.Language.English;
            }
        }

        public string GetTextByID(string key)
        {
            return Services.GetManager<DataManager>().StaticData.LocalizationData.GetTextByID(key);
        }
    }
}
