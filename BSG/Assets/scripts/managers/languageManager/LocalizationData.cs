using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Global.Managers.Datas
{
    using Tools;


    [CreateAssetMenu(fileName = "LocalizationData", menuName = "Scriptables/Localization data", order = 51)]
    public class LocalizationData : ScriptableObject
    {
        private const string DEFAULT_DIRECTORY = "Assets/scriptables/localization/";

        private const string EXTENSION = "csv";
        private const string TITLE = "Select CSV";
        private const string GOOGLE_DOC_ID = "1b0vhTZJDPU3v80Ov6YJTbMxMwxowag6BXdrIFQniS64";

        [SerializeField] private List<ActiveLanguageContainer> allLanguages;
        [SerializeField] private List<LocalizationDataContainer> containers;

        private Dictionary<string, LocalizationDataContainer> dictionaryContainers;

        public void InitDictionary()
        {
            if (dictionaryContainers == null)
            {
                Debug.Log("init");
                dictionaryContainers = new Dictionary<string, LocalizationDataContainer>(containers.Count);
                foreach (LocalizationDataContainer container in containers)
                {
                    dictionaryContainers.Add(container.Key, container);
                }
            }
        }

        public string GetTextByID(string key)
        {
            return dictionaryContainers[key].GetTextByLanguage(Services.GetManager<DataManager>().DynamicData.Settings.CurrentLanguage);
        }

#if UNITY_EDITOR

        public string GetTextByIDDef(string key)
        {
            InitDictionary();
            return dictionaryContainers[key].GetTextByLanguage(GT.Language.English);
        }

        public bool IsContain(string key)
        {
            InitDictionary();
            return dictionaryContainers.ContainsKey(key);
        }

        [MenuItem("Localization/Create main Asset")]
        public static void CreateLocalizationData()
        {
            // Create the directory if it doesn't exist
            if (!Directory.Exists(DEFAULT_DIRECTORY))
            {
                Directory.CreateDirectory(DEFAULT_DIRECTORY);
            }

            // Generate a unique filename
            string fileName = "MainLocalizationData.asset";
            string filePath = AssetDatabase.GenerateUniqueAssetPath(DEFAULT_DIRECTORY + fileName);

            // Create a new instance of the ScriptableObject
            LocalizationData newData = CreateInstance<LocalizationData>();

            // Save the ScriptableObject at the specified path
            AssetDatabase.CreateAsset(newData, filePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = newData;
        }

        [ContextMenu("From CSV")]
        private void LoadFromFile()
        {
            SetContainers(CSVParser.GetArrayFromFile(EditorUtility.OpenFilePanel(TITLE, Application.streamingAssetsPath, EXTENSION)));
        }

        [ContextMenu("From GoogleSheet")]
        private void UpdateFromGoogleSheet()
        {
            Action<string> x = str => { SetContainers(CSVParser.ParseArrayFromString(str)); };
            CSVDownloader.Download(GOOGLE_DOC_ID, x);
        }

        public string GetNewValueKey(string key)
        {
            //create new value, put it in list
            if (string.IsNullOrEmpty(key))
            {
                key = Guid.NewGuid().ToString();
            }
            key = key.Replace(" ", "");
            if (IsContain(key))
            {
                string newKey = key;
                int iterator = 0;
                while (IsContain(newKey))
                {
                    newKey = key + iterator;
                }
                key = newKey;
            }
            LocalizationDataContainer dataContainer = new LocalizationDataContainer();

            foreach (ActiveLanguageContainer languageContainer in allLanguages.Where(x => x.IsActive))
            {
                dataContainer.LanguageContainers.Add(new LanguageContentContainer(languageContainer.Language, ""));
            }
            dataContainer.Key = key;
            containers.Add(dataContainer);
            dictionaryContainers.Add(dataContainer.Key, dataContainer);
            EditorUtility.SetDirty(this);
            return key;
        }

        public void UpdateValue(string key, string newValue)
        {
            LocalizationDataContainer container = dictionaryContainers[key];
            if (container.LanguageContainers[0].Text != newValue)
            {
                container.LanguageContainers[0].Text = newValue;
                container.TranslateAll();
            }

            EditorUtility.SetDirty(this);
        }

        public List<string> GetAllValues(string key)
        {
            return dictionaryContainers[key].LanguageContainers.Select(x => x.Text).ToList();
        }

        public void DeleteElement(int index)
        {
            if (index >= 0 && index < containers.Count)
            {
                if (dictionaryContainers.ContainsKey(containers[index].Key))
                {
                    dictionaryContainers.Remove(containers[index].Key);
                }
                containers.RemoveAt(index);
            }
            EditorUtility.SetDirty(this);
        }
#endif

        private void SetContainers(List<List<string>> unpreparedData)
        {
            if (unpreparedData != null)
            {
                containers = new List<LocalizationDataContainer>(unpreparedData.Count);
                foreach (List<string> languageContents in unpreparedData)
                {
                    //containers.Add(new LocalizationDataContainer(languageContents));
                }
            }
        }
    }
}
