#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using Global.Components.Localization;
using Global.Managers.Datas;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Global.EditorScripts.Drawers
{
    [CustomEditor(typeof(TextLocalizer))]
    public class TextLocalizerEditor : Editor
    {
        private static GUIStyle errorStyle = new GUIStyle(EditorStyles.label);
        private static LocalizationData data;

        private SerializedProperty key;
        private SerializedProperty textProperty;

        private string textValue
        {
            get
            {
                if (textProperty != null)
                {
                    Text textComponent = textProperty.objectReferenceValue as Text;
                    if (textComponent != null)
                    {
                        return textComponent.text;
                    }
                }
                return "Text component reference is null";
            }
        }

        private void OnEnable()
        {
            errorStyle.normal.textColor = Color.red;
            textProperty = serializedObject.FindProperty("text");
            key = serializedObject.FindProperty("key");
        }

        public override void OnInspectorGUI()
        {
            TryFindData();
            serializedObject.Update();
            EditorGUILayout.LabelField(key.stringValue);
            EditorGUILayout.LabelField(textValue);

            // data can be null
            if (data != null)
            {
                if (string.IsNullOrEmpty(key.stringValue))
                {
                    key.stringValue = data.GetNewValueKey(textValue);
                }
                if (!string.IsNullOrEmpty(key.stringValue) && !string.IsNullOrEmpty(textValue) && data.IsContain(key.stringValue) && textValue != data.GetTextByIDDef(key.stringValue))
                {
                    if (GUILayout.Button("update"))
                    {
                        data.UpdateValue(key.stringValue, textValue);
                    }
                }
                if (!string.IsNullOrEmpty(key.stringValue) && data.IsContain(key.stringValue))
                {
                    IEnumerable<string> listAll = data.GetAllValues(key.stringValue).Skip(1);
                    foreach (string s in listAll)
                    {
                        EditorGUILayout.LabelField(s);
                    }
                }
                if (!data.IsContain(key.stringValue))
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("error : missing key", errorStyle);
                    if (GUILayout.Button("regenerate"))
                    {
                        key.stringValue = data.GetNewValueKey(textValue);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                EditorGUILayout.LabelField("error : localization data not found", errorStyle);
                if (GUILayout.Button("generate"))
                {
                    LocalizationData.CreateLocalizationData();
                }
            }


            serializedObject.ApplyModifiedProperties();
        }

        private static void TryFindData()
        {
            const string filter = "t:LocalizationData";

            if (data == null)
            {
                string[] guid = AssetDatabase.FindAssets(filter);
                if (guid != null && guid.Length > 0)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(guid[0]); //only first will be noticed
                    if (!string.IsNullOrEmpty(assetPath))
                    {
                        data = AssetDatabase.LoadAssetAtPath<LocalizationData>(assetPath);
                    }
                }
            }
        }
    }
}
#endif
