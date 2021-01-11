using Sourav.Engine.Core.EngineRelated;
using Sourav.Utilities.Scripts;
using UnityEditor;
using UnityEngine;

namespace Sourav.Utilities.Editor.EngineRelated
{
	public class CreateEngineCore : UnityEditor.Editor 
	{
		//% = cmd or ctrl, # = shift, & = alt
		[MenuItem("ProjectUtility/Tools/Create Core Engine %#e")]
		public static void CreateCoreEngine()
		{
			Debug.Log("Engine created");
			if (EditorUtility.DisplayDialog("Create Core Engine",
				"Do you want to create the core of Sourav Engine?", "Yes", "No"))
			{
				CheckGameObjectsPresetInScene<EngineId> engineID = new CheckGameObjectsPresetInScene<EngineId>();
				GameObjectSerachResult result = engineID.CheckForGameObject();

				if (result.numberOfObjects >= 1)
				{
					EditorUtility.DisplayDialog("ERROR!",
						"An instance of the Core Engine is already present in scene. No more is created.", "Ok");
				}
				else
				{
					CreatePrefabInstance createPrefab = new CreatePrefabInstance("Prefabs/Engine");
					EditorUtility.DisplayDialog("SUCCESS!",
						"Core Engine created. All the best with the rest of the Game!", "Ok");
				}
			}
		}
	}
}
