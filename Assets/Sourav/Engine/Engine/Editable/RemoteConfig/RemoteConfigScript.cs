using System.Collections;
using Sourav.DebugRelated;
using UnityEngine;
using Sourav.Utilities.Scripts;

namespace Sourav.Engine.Editable.RemoteConfig
{
    public class RemoteConfigScript : MonoBehaviour
    {
        private bool isFirebaseInitialized;
        private Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
        [SerializeField] private AsyncSceneLoad sceneLoad;

        // Start is called before the first frame update
        private IEnumerator Start()
        {
            sceneLoad.Init();
            
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    isFirebaseInitialized = true;
                }
                else
                {
                    D.LogError("Could not resolve Firebase dependencies: "+dependencyStatus);
                }
            });

            while (!isFirebaseInitialized)
            {
                yield return null;
            }
            
            GameObject holderObj = new GameObject("RemoteConfigHolder");
            RemoteConfigDataHolder holder = holderObj.AddComponent<RemoteConfigDataHolder>();
            holder.IsRemoteConfigSet = isFirebaseInitialized;
            holderObj.AddComponent<DontDestroyOnLoad>();

            sceneLoad.IsFirebaseLoaded = isFirebaseInitialized;
        }
    }
}
