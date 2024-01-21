using UnityEngine;

namespace BuildFiles
{
    [CreateAssetMenu(fileName = "AutomatedBuildConfig", menuName = "Build/AutomatedBuildConfig", order = 0)]
    public class AutomatedBuildConfig: ScriptableObject
    {
        public string[] scenes;
        public string keyStorePath;
        public string keyStorePassword;
        public string keyAliasName;
        public string keyAliasPassword;
    }
}