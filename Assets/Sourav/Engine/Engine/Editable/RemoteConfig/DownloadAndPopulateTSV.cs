using System.Collections;
using System.IO;
using Sourav.DebugRelated;
using UnityEngine;
using UnityEngine.Networking;

namespace Sourav.Engine.Editable.RemoteConfig
{
    public class DownloadAndPopulateTSV
    {
        public void DownloadData(string filename, string url, System.Action callback, MonoBehaviour mono)
        {
            mono.StartCoroutine(LoadFile(filename, url, callback));
        }

        public void FetchData(string filename, System.Action callback)
        {
            if (FileExists(filename))
            {
                // D.Log($"file exists {filename}");
                callback.Invoke();
            }
            else
            {
                // D.Log($"file does not exist {filename}");
                CopyExistingCSVToSaveLocation(filename);
                callback.Invoke();
            }
        }

        private void CopyExistingCSVToSaveLocation(string fileName)
        {
            string[] name = fileName.Split('.');
            
            TextAsset textAsset = Resources.Load(name[0]) as TextAsset;
            SaveDateToPersistentPath(fileName, textAsset.text);
        }

        private void SaveDateToPersistentPath(string filename, string fileContent)
        {
            string STRINGPATH = Application.persistentDataPath + "/" + filename;
            File.WriteAllText(STRINGPATH, fileContent);
        }

        private IEnumerator LoadFile(string filename, string filePath, System.Action onComplete)
        {
            using (UnityWebRequest uwr = UnityWebRequest.Get(filePath))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    D.Log(uwr.error);
                }
                else
                {
                    SaveDateToPersistentPath(filename, uwr.downloadHandler.text);
                    onComplete?.Invoke();
                }
            }
        }

        private static bool FileExists(string filename)
        {
            if(File.Exists(Application.persistentDataPath + "/" + filename))
            {
                return true;
            }
            return false;
        }
    }
}
