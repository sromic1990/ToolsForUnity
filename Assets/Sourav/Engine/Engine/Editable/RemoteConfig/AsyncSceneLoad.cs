using System.Collections;
// using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sourav.Engine.Editable.RemoteConfig
{
    public class AsyncSceneLoad : MonoBehaviour
    {
        [SerializeField] private Image loadFillImage;
        [SerializeField] private float minimumTimer;
        [SerializeField] private float fillUpto;
        [SerializeField] private float secondsWaitAfterLoad;
        /*[ReadOnly]*/[SerializeField] private float currentFill;
        /*[ReadOnly]*/[SerializeField] private bool isLoaded;
        /*[ReadOnly]*/[SerializeField] private bool isSceneLoadStarted;
        /*[ReadOnly]*/[SerializeField] private bool isLoadReady;
        private AsyncOperation asyncOperation;

        private bool isFirebaseLoaded;
        public bool IsFirebaseLoaded
        {
            get => isFirebaseLoaded;
            set => isFirebaseLoaded = value;
        }
        
        public void Init()
        {
            StartCoroutine(LoadSceneVisual());
        }

        private IEnumerator LoadSceneVisual()
        {
            Debug.Log("flow begin");
            isLoaded = false;
            currentFill = 0;
            yield return null;

            loadFillImage.fillAmount = currentFill;
            while (currentFill <= fillUpto)
            {
                Debug.Log("flow1");
                currentFill += (Time.deltaTime / minimumTimer);
                loadFillImage.fillAmount = currentFill;
                yield return null;

                if (currentFill > (fillUpto * 0.1f) && !(isSceneLoadStarted))
                {
                    isSceneLoadStarted = true;
                    
                    asyncOperation = SceneManager.LoadSceneAsync("Idle_BodyBuilder");
                    asyncOperation.allowSceneActivation = false;
                }
            }
            
            while (!asyncOperation.isDone)
            {
                Debug.Log("flow2");
                yield return null;

                while (!isFirebaseLoaded)
                {
                    yield return null;
                }
                
                if (asyncOperation.progress >= 0.9f)
                {
                    if (currentFill < fillUpto)
                    {
                        yield return null;
                    }
                    else
                    {
                        if (!isLoadReady)
                        {
                            isLoadReady = true;
                            StartCoroutine(LoadScene());
                        }
                    }
                }
            }

            while (currentFill < fillUpto)
            {
                Debug.Log("flow3");
                yield return null;
            }
        }

        private IEnumerator LoadScene()
        {
            isLoaded = true;
            loadFillImage.fillAmount = 1.0f;
            yield return new WaitForSeconds(secondsWaitAfterLoad);
            asyncOperation.allowSceneActivation = true;
        }
    }
}
