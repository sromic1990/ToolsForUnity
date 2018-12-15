using System.Collections.Generic;
using System.IO;
using Sourav.Utilities.Editor.BuildTargetGroupRelated;
using Sourav.Utilities.Editor.MacroRelated;
using Sourav.Utilities.Extensions;
using UnityEditor;
using UnityEngine;

namespace Sourav.Utilities.Editor.FileIORelated
{
    public class FindAllFolders : UnityEditor.Editor 
    {
        static List<string> folders;
        static string[] files1;
        static string[] files2;
        static string[] files;

        [MenuItem("ProjectUtility/Utilities/FindAllFolders")]
        public static void FindFolders()
        {
            folders = new List<string>();
            files1 = FindFiles.FindAllFiles(Definitions.Definitions.SCRIPT_EXTENSION);
            files2 = Directory.GetDirectories(Application.dataPath, "*", SearchOption.AllDirectories);
            files = files.Add(files1, files2);

            for (int i = 0; i < files.Length; i++)
            {
                string[] folders2 = files[i].Split('/');

                string folderName = folders2[folders2.Length - 2];
                if(!folders.Contains(folderName))
                {
                    //Debug.Log("Foldername = "+folderName);
                    folders.Add(folderName);
                }
            }

            GenerateEnums.GenerateCode("Folders", folders);

            SwitchOnOffMacro.MacroOnOff(GetBuildTargetGroup.GetCorrectBuildTargetGroup(), Definitions.Definitions.GENERATED_MACRO, MacroAction.On);

            AssetDatabase.Refresh();
        }
    }
    
}
