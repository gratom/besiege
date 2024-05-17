#if UNITY_EDITOR

using UnityEditor;

namespace Global.EditorScripts
{
    public class ScriptCreator : Editor
    {
        private const string PATH_TO_SCRIPT_TEMPLATE_MANAGER = "Assets/scripts/core/editor/templates/managerTemplate.cs.txt";
        private const string PATH_TO_SCRIPT_TEMPLATE_UI = "Assets/scripts/core/editor/templates/uiTemplate.cs.txt";

        [MenuItem("Assets/Create/Architecture/Service manager", priority = 51)]
        private static void CreateManager()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(PATH_TO_SCRIPT_TEMPLATE_MANAGER, "DefaultManager.cs");
        }

        [MenuItem("Assets/Create/Architecture/UI panel", priority = 51)]
        private static void CreateUI()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(PATH_TO_SCRIPT_TEMPLATE_UI, "NewUIPanel.cs");
        }

    }
}

#endif
