using System.IO;
using Sourav.Utilities.Editor.BuildTargetGroupRelated;
using Sourav.Utilities.Editor.MacroRelated;
using UnityEditor;
using UnityEngine;

namespace Sourav.Utilities.Editor.DeleteUtilities
{
    public class RemoveAllGeneratedCode : UnityEditor.Editor 
    {
        [MenuItem("ProjectUtility/Utilities/Delete All Generated")]
        public static void RemoveGenerated()
        {
            string path = Application.dataPath + "/Generated";

            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                File.Delete(files[i]);
            }
            Directory.Delete(path);

            GameObject[] InHierarchy = Object.FindObjectsOfType<GameObject>();
            for (int i = 0; i < InHierarchy.Length; i++)
            {
                if (InHierarchy[i].name.Equals("ExcludedFoldersList"))
                {
                    DestroyImmediate(InHierarchy[i]);
                }
            }

            SwitchOnOffMacro.MacroOnOff(GetBuildTargetGroup.GetCorrectBuildTargetGroup(), Definitions.Definitions.GENERATED_MACRO, MacroAction.Off);

            AssetDatabase.Refresh();
        }
    }
    
}
