#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using Global.Components.Localization;
using Global.Managers.Datas;
using UnityEditor;
using UnityEngine;

namespace Global.EditorScripts.Drawers
{

    [CustomPropertyDrawer(typeof(StringLocalizer))]
    public class StringLocalizerDrawer : PropertyDrawer
    {
        private static GUIStyle errorStyle = new GUIStyle(EditorStyles.label);
        private static GUIStyle disableStyle = new GUIStyle(EditorStyles.iconButton);
        private static LocalizationData data;

        private SerializedProperty key;

        private string tempStringValue;
        private bool isChange = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            errorStyle.normal.textColor = Color.red;

            property.FindPropertyRelative("Text");
            key = property.FindPropertyRelative("key");

            EditorGUI.BeginProperty(position, label, property);
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;


            int width = 0;

            TryFindData();

            if (data != null)
            {
                if (string.IsNullOrEmpty(key.stringValue))
                {
                    EditorGUI.LabelField(new Rect(position.x, position.y, 60, position.height), label.text);
                    tempStringValue = GUI.TextField(new Rect(position.x + 154, position.y, 200, position.height), tempStringValue);
                    if (string.IsNullOrEmpty(tempStringValue))
                    {
                        GUI.enabled = false;
                    }
                    if (GUI.Button(new Rect(position.x + 62, position.y, 90, position.height), "generate Key"))
                    {
                        key.stringValue = data.GetNewValueKey(tempStringValue);
                        data.UpdateValue(key.stringValue, tempStringValue);
                    }
                    GUI.enabled = true;
                }
                else
                {
                    if (data.IsContain(key.stringValue))
                    {
                        EditorGUI.LabelField(new Rect(position.x, position.y, 60, position.height), label.text);
                        if (!isChange)
                        {
                            tempStringValue = data.GetTextByIDDef(key.stringValue);
                        }
                        string newValue = GUI.TextField(new Rect(position.x + 62, position.y - 1, 250, position.height + 2), tempStringValue);
                        if (newValue != tempStringValue)
                        {
                            isChange = true;
                            tempStringValue = newValue;
                        }
                        if (GUI.Button(new Rect(position.x + 314, position.y - 1, 40, position.height + 2), "save"))
                        {
                            data.UpdateValue(key.stringValue, tempStringValue);
                            isChange = false;
                        }
                    }
                    else
                    {
                        EditorGUI.LabelField(new Rect(position.x, position.y, 130, position.height), "error : key not register", errorStyle);
                        if (GUI.Button(new Rect(position.x + 132, position.y - 1, 100, position.height + 2), "regenerate Key"))
                        {
                            key.stringValue = data.GetNewValueKey(key.stringValue);
                        }
                    }
                }

                // EditorGUI.LabelField(textRect, property.name + ": " + str);
                // if (!string.IsNullOrEmpty(key.stringValue) && !string.IsNullOrEmpty(textValue) && data.IsContain(key.stringValue) && textValue != data.GetTextByIDDef(key.stringValue))
                // {
                //     if (GUILayout.Button("update"))
                //     {
                //         data.UpdateValue(key.stringValue, textValue);
                //     }
                // }
                // if (!string.IsNullOrEmpty(key.stringValue) && data.IsContain(key.stringValue))
                // {
                //     IEnumerable<string> listAll = data.GetAllValues(key.stringValue).Skip(1);
                //     foreach (string s in listAll)
                //     {
                //         EditorGUILayout.LabelField(s);
                //     }
                // }
                // if (!data.IsContain(key.stringValue))
                // {
                //     EditorGUILayout.BeginHorizontal();
                //     EditorGUILayout.LabelField("error : missing key", errorStyle);
                //     if (GUILayout.Button("regenerate"))
                //     {
                //         key.stringValue = data.GetNewValueKey(textValue);
                //     }
                //     EditorGUILayout.EndHorizontal();
                // }

            }
            else
            {
                EditorGUI.LabelField(new Rect(position.x, position.y, 200, position.height), "error : localization data not found", errorStyle);
                if (GUI.Button(new Rect(position.x + 202, position.y, 80, position.height + 2), "generate"))
                {
                    LocalizationData.CreateLocalizationData();
                }
            }

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
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
