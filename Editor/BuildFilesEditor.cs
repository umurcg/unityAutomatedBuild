using UnityEditor;
using UnityEngine;

namespace BuildFiles.Editor
{
    public static class BuildFilesEditor
    {
        [MenuItem("Tools/Automated Build Settings")]
        public static void EditSettings()
        {
            var settings = Resources.Load<AutomatedBuildConfig>("AutomatedBuildConfig");

            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<AutomatedBuildConfig>();

                var path = "Assets/Resources";

                if (!AssetDatabase.IsValidFolder(path))
                    AssetDatabase.CreateFolder("Assets", "Resources");

                var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/AutomatedBuildConfig.asset");

                AssetDatabase.CreateAsset(settings, assetPathAndName);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = settings;
        }
    }
}