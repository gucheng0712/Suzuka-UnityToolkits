using UnityEditor;
using UnityEngine;

namespace CodeGeneration
{
    public class ScriptTemplateGeneratorWindow : EditorWindow
    {

        public static EditorSettings settings;

        private void OnEnable()
        {
            settings = Resources.Load("SavedSettings") as EditorSettings;
        }

        [MenuItem("Tools/ScriptTemplateGenerator")]
        static void Open()
        {
            ScriptTemplateGeneratorWindow win = GetWindow<ScriptTemplateGeneratorWindow>(true, "ScriptTemplateGenerator");
            win.maxSize = win.minSize = new Vector2(300, 100);
        }

        private void OnGUI()
        {
            GUILayout.Space(20);
            settings.enableCodeGeneration = EditorGUILayout.Toggle("Script Template", settings.enableCodeGeneration);

            // if not enable, just RETURN
            if (!settings.enableCodeGeneration) return;
            GUILayout.BeginHorizontal();
            settings.codeTemplateData = (CodeTemplateData)EditorGUILayout.ObjectField(settings.codeTemplateData, typeof(CodeTemplateData), false);


            GUILayout.EndHorizontal();
            if (settings.codeTemplateData != null)
                EditorUtility.SetDirty(settings.codeTemplateData);
        }
    }

}
