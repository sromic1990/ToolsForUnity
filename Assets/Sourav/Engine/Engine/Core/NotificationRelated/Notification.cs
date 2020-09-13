using System.Collections.Generic;
using UnityEngine;
using Sourav.Idle;

namespace Sourav.Engine.Core.NotificationRelated
{	
	[System.Serializable]
	public class NotificationParam
	{		
		public Dictionary<string, int> intData;
		public Dictionary<string, float> floatData;
		public Dictionary<string, double> doubleData;
		public Dictionary<string, string> stringData;
		public Dictionary<string, GameObject> gameObjectData;
		public Dictionary<string, bool> boolData;
		public Dictionary<string, Object> unityObjectData;
		public Dictionary<string, System.Object> objectData;
		public Dictionary<string, Vector2Int> vector2IntData;
		public Dictionary<string, Vector2> vector2Data;
		public Dictionary<string, Vector3> vector3Data;
		public Dictionary<string, Vector3Int> vector3IntData;
		public Dictionary<string, IdleCurrency> idleCurrencyData;
		

		public bool shouldQueue;

		public NotificationParam() : this(Mode.intData | Mode.floatData | Mode.doubleData | Mode.stringData |
		                                  Mode.gameObjectData | Mode.boolData | Mode.unityObjectData | Mode.objectData |
		                                  Mode.vector2IntData | Mode.vector2Data | Mode.vector3Data | Mode.vector3IntData |
		                                  Mode.idleCurrencyData)
		{}
		
		public NotificationParam(Mode mode)
		{
			if (mode.HasFlag(Mode.intData))
			{
				this.intData = new Dictionary<string, int>();
			}
			if (mode.HasFlag(Mode.floatData))
			{
				this.floatData = new Dictionary<string, float>();
			}
			if (mode.HasFlag(Mode.doubleData))
			{
				this.doubleData = new Dictionary<string, double>();
			}
			if (mode.HasFlag(Mode.stringData))
			{
				this.stringData = new Dictionary<string, string>();
			}
			if (mode.HasFlag(Mode.gameObjectData))
			{
				this.gameObjectData = new Dictionary<string, GameObject>();
			}
			if (mode.HasFlag(Mode.boolData))
			{
				this.boolData = new Dictionary<string, bool>();
			}
			if (mode.HasFlag(Mode.unityObjectData))
			{
				this.unityObjectData = new Dictionary<string, Object>();
			}
			if (mode.HasFlag(Mode.objectData))
			{
				this.objectData = new Dictionary<string, object>();
			}
			if (mode.HasFlag(Mode.vector2IntData))
			{
				this.vector2IntData = new Dictionary<string, Vector2Int>();
			}
			if (mode.HasFlag(Mode.vector2Data))
			{
				this.vector2Data = new Dictionary<string, Vector2>();
			}
			if (mode.HasFlag(Mode.vector3Data))
			{
				this.vector3Data = new Dictionary<string, Vector3>();
			}
			if (mode.HasFlag(Mode.vector3IntData))
			{
				this.vector3IntData = new Dictionary<string, Vector3Int>();
			}
			if (mode.HasFlag(Mode.idleCurrencyData))
			{
				this.idleCurrencyData = new Dictionary<string, IdleCurrency>();
			}
			
			this.shouldQueue = false;
		}
	}

	[System.Flags]
	public enum Mode
	{
		intData = 0,
		floatData = 1,
		doubleData = floatData << 1,
		stringData = doubleData << 1,
		gameObjectData = stringData << 1,
		boolData = gameObjectData << 1,
		unityObjectData = boolData << 1,
		objectData = unityObjectData << 1,
		vector2IntData = objectData << 1,
		vector2Data = vector2IntData << 1,
		vector3Data = vector2Data << 1,
		vector3IntData = vector3Data << 1,
		idleCurrencyData = vector3IntData << 1

	}
}
