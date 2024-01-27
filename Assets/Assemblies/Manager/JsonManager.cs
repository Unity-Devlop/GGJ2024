using System.IO;
using UnityEngine;
using UnityToolkit;

namespace GGJ2024
{
    public static class JsonManager
    {
        private static string streamingAssetsPath => Application.streamingAssetsPath;

        public static TObj LoadJsonFromStreamingAssets<TObj>(string path)
        {
            string json = ReadFile($"{streamingAssetsPath}/{path}");
            return JsonUtility.FromJson<TObj>(json);
        }

        public static void SaveJsonToStreamingAssets<TObj>(string path, TObj obj)
        {
            string json = JsonUtility.ToJson(obj);
            SaveFile($"{streamingAssetsPath}/{path}", json);
        }


        public static TObj LoadJson<TObj>(string path)
        {
            string json = ReadFile(path);
            return JsonUtility.FromJson<TObj>(json);
        }

        public static void SaveJson<TObj>(string path, TObj obj)
        {
            string json = JsonUtility.ToJson(obj);
            SaveFile(path, json);
        }
        
        public static void SaveFile(string path, string content)
        {
            if (File.Exists(path))
            {
                FileUtil.ReplaceContent(content, path);
            }
            else
            {
                FileUtil.Create(path);
                FileUtil.ReplaceContent(content, path);
            }
        }

        public static string ReadFile(string path)
        {
            if (FileUtil.TryReadAllText(path, out var content))
            {
                return content;
            }

            return "";
        }
    }
}