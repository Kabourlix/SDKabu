using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;

#nullable enable

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class IsExternalInit
    {
    }
}

namespace SDKabu.KBootstrap
{
    public static class KPackageLoader
    {
        // private record PackageData(string Name, string Path);
        //
        // private static Dictionary<string, string> packagesPathByName = new Dictionary<string, string>();
        // private const string PACKAGE_JSON_FILE_NAME = "package.json";
        //
        // /// <summary>
        // /// Load packages from SDKabu/KPackages directory.
        // /// </summary>
        // private static void LoadPackages(string _sdkFullPath)
        // {
        //     packagesPathByName = new Dictionary<string, string>();
        //     //Get script current location
        //     var rootDir = new DirectoryInfo(_sdkFullPath);
        //     Debug.LogWarning("Root dir: " + rootDir.FullName);
        //     
        //     var packagesDir  = rootDir.GetDirectories();
        //     foreach (var packageDirInfo in packagesDir)
        //     {
        //         var data = ParsePackageDirectory(packageDirInfo);
        //         if (data == null)
        //         {
        //             Debug.LogWarning($"{packageDirInfo.Name} is not a valid package directory.");
        //             continue;
        //         }
        //         packagesPathByName.Add(data.Name, data.Path);
        //     }
        //     
        //     Debug.Log($"Loaded {packagesPathByName.Count} packages internally.");
        // }
        //
        // private static PackageData? ParsePackageDirectory(DirectoryInfo _packageDir)
        // {
        //     var files = _packageDir.GetFiles();
        //     foreach (var fileInfo in files)
        //     {
        //         if (fileInfo.Name != PACKAGE_JSON_FILE_NAME) continue;
        //         
        //         var fileStr = File.ReadAllText(fileInfo.FullName);
        //         var parsedJsonDict = KSerializer.DeserializeDictionary<string, string>(fileStr);
        //         if (parsedJsonDict.TryGetValue("name", out var name))
        //         {
        //             return new PackageData(name, _packageDir.FullName);
        //         }
        //         Debug.LogError($"Package {fileInfo.Name} does not have a name field in its package.json file.");
        //         return null;
        //     }
        //
        //     Debug.LogError($"Package directory {_packageDir.Name} does not contain a {PACKAGE_JSON_FILE_NAME} file.");
        //     return null;
        // }


        /// <summary>
        /// Method to load new packages into the project.
        /// </summary>
        /// <param name="_unityManifest">FileInfo of the current project unity package manifest</param>
        /// <param name="_newDependencies">New dependencies to add (packagesID,location)</param>
        public static void UpdateManifestPackages(FileInfo _unityManifest, Dictionary<string, string> _newDependencies)
        {
            //Load Manifest
            var manifestStr = File.ReadAllText(_unityManifest.FullName);
            var manifestDict = KSerializer.DeserializeDictionary<string, Dictionary<string, string>>(manifestStr);

            if (!manifestDict.TryGetValue("dependencies", out var dependencies))
            {
                Debug.LogError("Manifest does not contain a dependencies field.");
                return;
            }

            foreach (var pair in _newDependencies)
            {
                var name = pair.Key ?? string.Empty;
                var path = pair.Value ?? string.Empty;
                if (dependencies.TryAdd(name, path))
                {
                    Debug.Log("Success");
                    continue;
                }
                
                Debug.LogWarning($"Package {name} is already in the manifest.");
            }
            
            manifestDict["dependencies"] = dependencies; //Apply new dependencies
            
            UpdateManifest(manifestDict, _unityManifest);
        }
        
        private static void UpdateManifest(Dictionary<string, Dictionary<string,string>> _manifestDict, FileInfo _manifestFileInfo)
        {
            var newManifestStr = KSerializer.SerializeDictionary(_manifestDict);
            
            try
            {
                File.WriteAllText(_manifestFileInfo.FullName, newManifestStr);
            }
            catch (Exception e)
            {
                Debug.LogError("Writing to manifest failed. \n" + e);
                throw;
            }
        }
    }
}