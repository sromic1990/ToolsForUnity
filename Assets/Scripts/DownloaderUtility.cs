//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Net.Security;
//using System.Security.Cryptography.X509Certificates;

//using UnityEngine;


//namespace GameAnax.Core.Net
//{
//    public class DownloaderUtility : MonoBehaviour
//    {
//        public bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
//        {
//            return true;
//        }

//        /// <summary>
//        /// Downloads the asset bundle.
//        /// </summary>
//        /// <returns>The asset bundle.</returns>
//        /// <param name="url">URL.</param>
//        /// <param name="path">Path.</param>
//        /// <param name="saveFileName">Save file name.</param>
//        /// <param name="keep">If set to <c>true</c> keep.</param>
//        /// <param name="onDone">On done.</param>
//        public IEnumerator DownloadAssetBundle(string url, string path, string saveFileName, bool keep, Action<string, AssetBundle, Dictionary<string, string>> onDone)
//        {
//            WWW www = new WWW(url);
//            yield return www;
//            if (!string.IsNullOrEmpty(www.error))
//            {
//                Debug.LogWarning(www.error);
//                if (onDone != null) { onDone(www.error, null, www.responseHeaders); }
//            }
//            else
//            {
//                if (keep)
//                {
//                    //File.WriteFile(path, saveFileName, www.bytes);
//                }
//                if (onDone != null) { onDone(string.Empty, www.assetBundle, www.responseHeaders); }
//            }
//        }

//        /// <summary>
//        /// Downloads the voice clip.
//        /// </summary>
//        /// <returns>The voice clip.</returns>
//        /// <param name="url">URL.</param>
//        /// <param name="saveFileName">Save file name.</param>
//        /// <param name="keep">If set to <c>true</c> keep.</param>
//        public IEnumerator DownloadVoiceClip(string url, string path, string saveFileName, bool keep, Action<string, AudioClip, Dictionary<string, string>> onDone)
//        {
//            WWW www = new WWW(url);
//            yield return www;
//            if (!string.IsNullOrEmpty(www.error))
//            {
//                MyDebug.Warning(www.error);
//                if (onDone != null) { onDone(www.error, null, www.responseHeaders); }
//            }
//            else
//            {
//                if (keep)
//                {
//                    File.WriteFile(path, saveFileName, www.bytes);
//                }
//                if (onDone != null) { onDone(string.Empty, www.GetAudioClip(), www.responseHeaders); }
//            }
//        }

//        /// <summary>
//        /// Downloads the Text Data
//        /// </summary>
//        /// <returns>The text data.</returns>
//        /// <param name="url">URL.</param>
//        /// <param name="saveFileName">Save file name.</param>
//        /// <param name="keep">If set to <c>true</c> keep.</param>
//        public IEnumerator DownloadText(string url, string path, string saveFileName, bool keep, Action<string, string, Dictionary<string, string>> onDone)
//        {
//            WWW www = new WWW(url);
//            yield return www;
//            if (!string.IsNullOrEmpty(www.error))
//            {
//                MyDebug.Warning(www.error);
//                if (onDone != null) { onDone(www.error, null, www.responseHeaders); }
//            }
//            else
//            {
//                if (keep)
//                {
//                    File.WriteFile(path, saveFileName, www.bytes);
//                }
//                if (onDone != null) { onDone(string.Empty, www.text, www.responseHeaders); }
//            }
//        }

//        /// <summary>
//        /// Downloads the image.
//        /// </summary>
//        /// <returns>The image.</returns>
//        /// <param name="url">URL.</param>
//        /// <param name="saveFileName">Save file name.</param>
//        /// <param name="keep">If set to <c>true</c> keep.</param>
//        public IEnumerator DownloadImage(string url, string path, string saveFileName, bool keep, Action<string, Texture2D, Dictionary<string, string>> onDone)
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
//            WWW www = new WWW(url);
//            yield return www;
//            if (!string.IsNullOrEmpty(www.error))
//            {
//                MyDebug.Warning(www.error);
//                if (onDone != null) { onDone(www.error, null, www.responseHeaders); }
//            }
//            else
//            {
//                if (keep)
//                {
//                    File.WriteFile(path, saveFileName, www.bytes);
//                }
//                if (onDone != null) { onDone(string.Empty, www.texture, www.responseHeaders); }
//            }
//        }
//#if UNITY_STANDALONE
//		public IEnumerator DownloadMovieTexture(string url, string path, string saveFileName, bool keep, Action<string, MovieTexture, Dictionary<string, string>> onDone) {
//			System.Net.ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
//			WWW www = new WWW(url);
//			yield return www;
//			if(!string.IsNullOrEmpty(www.error)) {
//				MyDebug.Warning(www.error);
//				if(onDone != null) { onDone(www.error, null, www.responseHeaders); }
//			} else {
//				if(keep) {
//					File.WriteFile(path, saveFileName, www.bytes);
//				}
//				if(onDone != null) { onDone(string.Empty, www.GetMovieTexture(), www.responseHeaders); }
//			}
//		}
//#endif
//    }
//}