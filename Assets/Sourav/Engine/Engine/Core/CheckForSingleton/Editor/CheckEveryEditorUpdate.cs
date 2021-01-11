using System.Collections.Generic;
using Sourav.Utilities.Scripts;
using Sourav.Utilities.Scripts.Utilities;
using UnityEditor;
using UnityEngine;

namespace Sourav.Utilities.Editor.RunEveryEditorUpdate
{
	[InitializeOnLoad]
	public class CheckEveryEditorUpdate : UnityEditor.Editor
	{
		private static bool hasWarningBeenShown;
		private SingletonTypes currentSingletonType;

		private static Dictionary<SingletonTypes, GameObject> allSingletonTypes = new Dictionary<SingletonTypes, GameObject>();
		private static List<GameObject> duplicateObjects;
		
		[ExecuteInEditMode]
		static CheckEveryEditorUpdate()
		{
			duplicateObjects = new List<GameObject>();
			hasWarningBeenShown = false;
			EditorApplication.update += new EditorApplication.CallbackFunction(Update);
		}

		private static void Update()
		{
			CheckGameObjectsPresetInScene<SingletonObject> singleton = new CheckGameObjectsPresetInScene<SingletonObject>();
			GameObjectSerachResult result = singleton.CheckForGameObject();

			if (result.numberOfObjects > 1)
			{
				List<GameObject> gObjs = result.foundGameObjects;

				int index = 0;
				for (int i = 0; i < gObjs.Count; i++)
				{
					SingletonTypes t = gObjs[i].GetComponent<SingletonObject>().type;
					if (allSingletonTypes.ContainsKey(t))
					{
						duplicateObjects.Add(gObjs[i]);
						index++;
					}
					else
					{
						allSingletonTypes.Add(t, gObjs[i]);
					}
				}
			}

			for (int i = 0; i < duplicateObjects.Count; i++)
			{
				DestroyImmediate(duplicateObjects[i]);
			}
			duplicateObjects.Clear();
			allSingletonTypes = new Dictionary<SingletonTypes, GameObject>();
		}

//		private void OnDisable()
//		{
//			EditorApplication.update -= this.Update;
//		}
	}
}
