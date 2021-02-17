// using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Tests
{
    public class ChangeScene : MonoBehaviour
    {
        // [Button]
        public void GoToScene1()
        {
            SceneManager.UnloadSceneAsync(1);
        }
    
        // [Button]
        public void GoToScene2()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }
}
