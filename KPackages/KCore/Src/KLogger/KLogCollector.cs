// Created by Kabourlix Cendrée on 26/10/2023

using System;
using System.IO;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace SDKabu.KCore
{
    public class KLogCollector : MonoBehaviour
    {
        private StringBuilder logBuilder = new();

        private string fileName;

        // Start is called before the first frame update
        private void Start()
        {
            DateTime today = DateTime.Today;
            DateTime now = DateTime.Now;
            string appName = Application.productName;
            fileName =
                $"LOG_{appName}_{today.Day}_{today.Month}_{today.Year}_{now.Hour}_{now.Minute}_{now.Second}.log";
            logBuilder.Append(
                $"{appName} - {today.Day}/{today.Month}/{today.Year} - {now.Hour}:{now.Minute}:{now.Second}\n \n");
            Application.logMessageReceived += HandleLog;
        }

        private void HandleLog(string _condition, string _stacktrace, LogType _type)
        {
            logBuilder.Append(_condition);
            logBuilder.Append("\n");
            logBuilder.Append(_stacktrace);
            logBuilder.Append("\n");
        }


        private void OnApplicationQuit()
        {
            Application.logMessageReceived -= HandleLog;
            string log = logBuilder.ToString();
            string path = Path.Combine(Application.persistentDataPath, fileName);

            File.WriteAllText(path, log);
        }

#if UNITY_EDITOR
        //Menu item that will open the persistent data path in the file explorer with f1 key
        [MenuItem("SDKabu/Open Persistent Data Path _F1", false)]
#endif
        public static void OpenPersistentDataPath()
        {
            string path = Application.persistentDataPath;
            Application.OpenURL(path);
        }

#if UNITY_EDITOR
        [MenuItem("SDKabu/Clear logs", false)]
        public static void ClearLogsFromPersistantData()
        {
            string path = Application.persistentDataPath;
            string[] files = Directory.GetFiles(path);
            string appName = Application.productName;
            foreach (string file in files)
            {
                if (file.StartsWith($"LOG_{appName}"))
                {
                    File.Delete(file);
                }
            }
        }
#endif
    }
}
