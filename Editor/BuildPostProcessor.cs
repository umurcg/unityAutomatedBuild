using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace BuildFiles.Editor
{
    public class BuildPostProcessor {

        [PostProcessBuild]
        public static void ChangeXcodePlist(BuildTarget buildTarget, string path) {

            if (buildTarget == BuildTarget.iOS) {

                string plistPath = path + "/Info.plist";
                PlistDocument plist = new PlistDocument();
                plist.ReadFromFile(plistPath);

                PlistElementDict rootDict = plist.root;

                Debug.Log(">> Automation, plist ... <<");
                rootDict.SetBoolean("ITSAppUsesNonExemptEncryption", false);
                rootDict.SetString("NSCalendarsUsageDescription", "${PRODUCT_NAME} requests access to the Calendar");

                File.WriteAllText(plistPath, plist.WriteToString());
            }
        }
    }
}