using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BuildFiles.Editor
{
    public class BuildScript
    {
        static void BuildForAndroid()
        {
            AutomatedBuildConfig config = Resources.Load<AutomatedBuildConfig>("AutomatedBuildConfig");
            
            if (config == null)
            {
                Debug.LogError("AutomatedBuildConfig not found");
                EditorApplication.Exit(1);
            }
            
            
            PlayerSettings.Android.keystoreName = config.keyStorePath;
            PlayerSettings.Android.keystorePass = config.keyStorePassword;
            PlayerSettings.Android.keyaliasName = config.keyAliasName;
            PlayerSettings.Android.keyaliasPass = config.keyAliasPassword;
        
            string buildFormat = Environment.GetEnvironmentVariable("BUILD_FORMAT");
            bool buildAAB = string.Equals(buildFormat, "AAB", StringComparison.OrdinalIgnoreCase);
            // Set the extension and build target accordingly
            string extension = buildAAB ? ".aab" : ".apk";
        
            EditorUserBuildSettings.buildAppBundle = buildAAB;
            
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

            string buildName = PlayerSettings.productName + "_" + PlayerSettings.bundleVersion + "_" +
                               PlayerSettings.Android.bundleVersionCode + extension;
        
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = config.scenes,
                locationPathName = "Builds/" + buildName,
                target = BuildTarget.Android,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                string buildPath = "Builds/" + buildName;
                string buildPathFilePath = "Builds/buildPath.txt"; // File to store build path
                System.IO.File.WriteAllText(buildPathFilePath, buildPath);
                Debug.Log("Build path: " + buildPath);
                Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
                EditorApplication.Exit(0);
            }

            if (summary.result == BuildResult.Failed)
            {
                Debug.LogError("Build failed");
                EditorApplication.Exit(1);
            }
        }

        static void BuildForiOS()
        {
            var config = Resources.Load<AutomatedBuildConfig>("AutomatedBuildConfig");
            if (config == null)
            {
                Debug.LogError("AutomatedBuildConfig not found");
                EditorApplication.Exit(1);
            }
            
            // Switch to iOS build target
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);

            string buildName = PlayerSettings.productName + "_" + PlayerSettings.bundleVersion + "_" +
                               PlayerSettings.iOS.buildNumber;
        
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = config.scenes,
                locationPathName = "Builds/" + buildName,
                target = BuildTarget.iOS,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                string buildPath = "Builds/" + buildName;
                string buildPathFilePath = "Builds/buildPath.txt"; // File to store build path
                System.IO.File.WriteAllText(buildPathFilePath, buildPath);
                Debug.Log("Build path: " + buildPath);
            
                Debug.Log("iOS Build succeeded: " + summary.totalSize + " bytes");
                EditorApplication.Exit(0);
            
            }

            if (summary.result == BuildResult.Failed)
            {
                Debug.LogError("iOS Build failed");
                EditorApplication.Exit(1);
            }
        }
    }
}