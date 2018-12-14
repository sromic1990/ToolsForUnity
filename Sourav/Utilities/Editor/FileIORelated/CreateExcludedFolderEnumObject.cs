using Sourav.Utilities.Editor.ScriptsRelated;
using UnityEditor;
using UnityEngine;

namespace Sourav.Utilities.Editor.FileIORelated
{
    public class CreateExcludedFolderEnumObject : MonoBehaviour
    {
        [MenuItem("ProjectUtility/Utilities/Create List Of Excluded folders")]
        public static void CreateExcludedFolderObject()
        {
#if GENERATED_FILES
            if(FindObjectOfType<ExcludedFolders>() == null)
            {
                GameObject go = new GameObject("ExcludedFoldersList");
                go.tag = "Generated";
                go.AddComponent<DontDestroyOnLoad>();
                go.AddComponent<ExcludedFolders>();
            }
#endif
            SortScripts.SortScriptsAsPerFolder();

        }
    }
    
}
