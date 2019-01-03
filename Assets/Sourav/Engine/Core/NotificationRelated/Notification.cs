using System.Collections.Generic;
using UnityEngine;

namespace Sourav.Engine.Core.NotificationRelated
{
	public class NotificationParam
	{
		public List<int> intData;
		public List<float> floatData;
		public List<double> doubleData;
		public List<string> stringData;
		public List<GameObject> gameObjectData;
		public List<bool> boolData;
		public List<Object> unityObjectData;
		public List<System.Object> objectData;

		public bool shouldQueue;

		public NotificationParam()
		{
			intData = new List<int>();
			floatData = new List<float>();
			doubleData = new List<double>();
			stringData = new List<string>();
			gameObjectData = new List<GameObject>();
			boolData = new List<bool>();
			unityObjectData = new List<Object>();
			objectData = new List<object>();

			shouldQueue = false;
		}
	}
}
