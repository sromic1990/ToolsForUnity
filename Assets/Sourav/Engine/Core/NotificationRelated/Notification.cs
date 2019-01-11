using System.Collections.Generic;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;
using UnityEngine.Networking;

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
			this.intData = new List<int>();
			this.floatData = new List<float>();
			this.doubleData = new List<double>();
			this.stringData = new List<string>();
			this.gameObjectData = new List<GameObject>();
			this.boolData = new List<bool>();
			this.unityObjectData = new List<Object>();
			this.objectData = new List<object>();

			this.shouldQueue = false;
		}
	}
}
