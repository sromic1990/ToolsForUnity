using System.Collections.Generic;
using UnityEngine;

namespace Sourav.Engine.Core.DataRelated
{
	[System.Serializable]
	public class LocalPositionRegistry
	{
		private Dictionary<string, Vector3> positionRegistry;

		private List<string> objects;

		private List<Vector3> positions;

		public LocalPositionRegistry()
		{
			positionRegistry = new Dictionary<string, Vector3>();
			objects = new List<string>();
			positions = new List<Vector3>();
		}
		
		public void StorePosition(string objectName, Vector3 position)
		{
			if (positionRegistry == null)
			{
				positionRegistry = new Dictionary<string, Vector3>();
				objects = new List<string>();
				positions = new List<Vector3>();
			}

			if (positionRegistry.ContainsKey(objectName))
			{
				positionRegistry[objectName] = position;
				int i = objects.IndexOf(objectName);
				positions[i] = position;
			}
			else
			{
				positionRegistry.Add(objectName, position);
				objects.Add(objectName);
				positions.Add(position);
			}
		}

		public Vector3 GetPosition(string objectName)
		{
			Vector3 position = Vector3.zero;

			if (positionRegistry.ContainsKey(objectName))
			{
				position = positionRegistry[objectName];
			}

			return position;
		}
	}
}
