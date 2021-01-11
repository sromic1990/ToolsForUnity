using Sourav.Tools.OrientationManager;
using Sourav.Utilities.Scripts;
using UnityEditor;
using UnityEngine;

namespace Sourav.Utilities.Editor.OrientationManagerRelated
{
	public class CreateOrientationManager : UnityEditor.Editor 
	{
		[MenuItem("ProjectUtility/Tools/Create Orientation Manager %#o")]
		public static void CreateCoreEngine()
		{
			Debug.Log("Orientation Manager created");
			if (EditorUtility.DisplayDialog("Create Orientation Manager",
				"Do you want to create Orientation Manager?", "Yes", "No"))
			{
				CheckGameObjectsPresetInScene<OrientationManager> id = new CheckGameObjectsPresetInScene<OrientationManager>();
				GameObjectSerachResult result = id.CheckForGameObject();

				if (result.numberOfObjects >= 1)
				{
					EditorUtility.DisplayDialog("ERROR!",
						"An instance of the Orientation Manager is already present in scene. No more is created.", "Ok");
				}
				else
				{
					CreatePrefabInstance createPrefab = new CreatePrefabInstance("Prefabs/OrientationManager");
					EditorUtility.DisplayDialog("SUCCESS!",
						"Orientation Manager created. All the best with the rest of the Game!", "Ok");
				}
			}
		}
	}
}
