//developer -> gratomov@gmail.com

#if UNITY_EDITOR

using System.Drawing;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Tools
{
    using UMP = UniversalMousePosition;

    /// <summary>
    /// Add-on for the Unity editor, which helps to quickly move from one scene to another
    /// </summary>
    internal class ScenePicker : EditorWindow
    {

        private static SceneInfo[] sceneInfos = default;
        private const int WIDTH = 200;
        private const int LINE_HEIGHT = 20;
        private const int BOTTOM_OUTFIT = 3;

        [MenuItem("Scenes/Choose scene")]
        private static void Init()
        {
            LoadSceneInfos();
            Vector2Int size = GetWindowSize();
            ScenePicker window = GetWindow<ScenePicker>();
            Point pos = UMP.GetCursorPosition();
            window.maxSize = size;
            window.minSize = size;
            window.position = new Rect(pos.X, pos.Y, size.x, size.y);
            window.Show();
        }

        private static void LoadSceneInfos()
        {
            sceneInfos = SceneTool.GetSceneInfosInBuild();
        }

        private static Vector2Int GetWindowSize()
        {
            return new Vector2Int(WIDTH, sceneInfos.Length * LINE_HEIGHT + BOTTOM_OUTFIT);
        }

        private static void OpenScene(string path)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(path);
        }

        private void OnGUI()
        {
            foreach (SceneInfo t in sceneInfos)
            {
                GUI.enabled = t.Existing;
                if (GUILayout.Button(t.Name + (t.Existing ? "" : "(deleted)")))
                {
                    OpenScene(t.Path);
                    Close();
                }
                GUI.enabled = true;
            }
        }
    }
}

#endif