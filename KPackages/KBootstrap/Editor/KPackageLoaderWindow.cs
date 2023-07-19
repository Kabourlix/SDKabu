using System.IO;
using UnityEditor;
using UnityEngine;

#nullable enable

namespace SDKabu.KBootstrap.Editor
{
    internal class KPackageLoaderWindow : EditorWindow
    {

        [MenuItem("SDKabu/PackageLoader")]
        private static void ShowWindow()
        {
            var window = GetWindow<KPackageLoaderWindow>();
            window.titleContent = new UnityEngine.GUIContent("Package Loader");
            window.Show();
        }

        private static string sdkManifestLocation = string.Empty;
        private const string SDK_MANIFEST_LOCATION_KEY = "SDK_MANIFEST_LOCATION_KEY";

        private void OnEnable()
        {
            sdkManifestLocation = PlayerPrefs.GetString(SDK_MANIFEST_LOCATION_KEY, string.Empty);
        }

        private void OnGUI()
        {
            //Begin hor
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("SDKManifest: ", GUILayout.Width(120));
            sdkManifestLocation = EditorGUILayout.TextField(sdkManifestLocation);
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Load Packages"))
            { 
                LoadPackagesToUnity();
            }
        }

        private void OnLostFocus()
        {
            PlayerPrefs.SetString(SDK_MANIFEST_LOCATION_KEY, sdkManifestLocation);
        }

        private void LoadPackagesToUnity()
        {
            //Load the sdkmanifest file
            if (string.IsNullOrEmpty(sdkManifestLocation))
            {
                Debug.LogError("Manifest location is empty.");
                return;
            }
            FileInfo sdkManifest = new(sdkManifestLocation);
            if (!sdkManifest.Exists)
            {
                Debug.LogError("Manifest file does not exist.");
                return;
            }
            
            //Load the packages 
            var newDependencies =
                KSerializer.DeserializeDictionary<string, string>(File.ReadAllText(sdkManifest.FullName));
            //Get unity Packages folder path 
            var unityPackagesPath = Path.Combine(Application.dataPath, "..", "Packages");
            FileInfo unityManifest = new(Path.Combine(unityPackagesPath, "manifest.json"));
            
            
            KPackageLoader.UpdateManifestPackages(unityManifest, newDependencies);
            
        }
    }
}