using System.Collections.Generic;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.IdleGameEngine.IdleCurrency.IdleCurrency;
using UnityEngine;
using UnityEngine.Networking;

namespace Sourav.Engine.Core.NotificationRelated
{	
	[System.Serializable]
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
		public List<Vector2Int> vector2IntData;
		public List<Vector2> vector2Data;
		public List<Vector3> vector3Data;
		public List<Vector3Int> vector3IntData;
		public List<IdleCurrency> idleCurrencyData;
		

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
				this.intData = new List<int>();
			}
			if (mode.HasFlag(Mode.floatData))
			{
				this.floatData = new List<float>();
			}
			if (mode.HasFlag(Mode.doubleData))
			{
				this.doubleData = new List<double>();
			}
			if (mode.HasFlag(Mode.stringData))
			{
				this.stringData = new List<string>();
			}
			if (mode.HasFlag(Mode.gameObjectData))
			{
				this.gameObjectData = new List<GameObject>();
			}
			if (mode.HasFlag(Mode.boolData))
			{
				this.boolData = new List<bool>();
			}
			if (mode.HasFlag(Mode.unityObjectData))
			{
				this.unityObjectData = new List<Object>();
			}
			if (mode.HasFlag(Mode.objectData))
			{
				this.objectData = new List<object>();
			}
			if (mode.HasFlag(Mode.vector2IntData))
			{
				this.vector2IntData = new List<Vector2Int>();
			}
			if (mode.HasFlag(Mode.vector2Data))
			{
				this.vector2Data = new List<Vector2>();
			}
			if (mode.HasFlag(Mode.vector3Data))
			{
				this.vector3Data = new List<Vector3>();
			}
			if (mode.HasFlag(Mode.vector3IntData))
			{
				this.vector3IntData = new List<Vector3Int>();
			}
			if (mode.HasFlag(Mode.idleCurrencyData))
			{
				this.idleCurrencyData = new List<IdleCurrency>();
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
