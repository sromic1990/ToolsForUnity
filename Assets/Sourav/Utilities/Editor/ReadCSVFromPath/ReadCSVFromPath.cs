using Sourav.Utilities.Scripts;
using UnityEditor;

namespace Sourav.Utilities.Editor.ReadCSVFromPath
{
	public class ReadCSVFromPath : UnityEditor.Editor 
	{
		//% = cmd or ctrl, # = shift, & = alt
		[MenuItem("ProjectUtility/Utilities/Create CSV Reader %#r")]
		public static void CreateCSVReader()
		{
			if (EditorUtility.DisplayDialog("CSV Reader", "Do you want to create a CSV Reader?", "Yes", "No"))
			{
				CreatePrefabInstance createPrefab = new CreatePrefabInstance("Prefabs/ReadCSVFromPath");
			}
		}
	}
}
