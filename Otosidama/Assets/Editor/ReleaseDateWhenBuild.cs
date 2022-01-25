using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using System;

namespace MyProject
{
    /// <summary>
    /// ビルド時の時刻を取得する
    /// </summary>
    public class ReleaseDateWhenBuild : MonoBehaviour
    {
        private static readonly string FILE_PATH = "Assets/Resources/BuildDate.txt";

        [PostProcessBuild(1)]
        public static void OnPostProcessBuild(BuildTarget target, string path)
        {
            using (var writer = new BinaryWriter(File.Open(FILE_PATH, FileMode.OpenOrCreate, FileAccess.Write)))
            {
                writer.Write(DateTime.Now.ToString("yyyy.MM.dd.HH.mm"));
            }
        }
    }
}