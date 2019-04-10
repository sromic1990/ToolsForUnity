using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.Engine.Editable.DataRelated
{
	public class CameraCommonData : CommonData
	{
		//FIXME enter cameraData items
		[Range(-10, 10)]
		public float cameraZoomInMaxValue;
	}
}
