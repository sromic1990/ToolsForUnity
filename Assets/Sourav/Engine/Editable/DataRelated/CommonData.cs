using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.Engine.Editable.DataRelated
{
	public abstract class CommonData : GameElement
	{
		[SerializeField] private DataType type;

		public DataType GetData()
		{
			return type;
		}
	}

	//FIXME Add more Data type as and when needed
	public enum DataType
	{
		None,
		GameData,
		LevelData,
		CameraData,
		TimerData,
		//IDLE GAME RELATED
		IdleCommonData,
	}
}
