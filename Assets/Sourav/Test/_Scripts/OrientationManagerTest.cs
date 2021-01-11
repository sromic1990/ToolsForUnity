using Sourav.Tools.OrientationManager;
using UnityEngine;
using UnityEngine.UI;

namespace Sourav.Test._Scripts
{
	public class OrientationManagerTest : MonoBehaviour
	{
		[SerializeField] private Text uiText;

		public void OnOrientationChange(OrientationManager.Orientation orientation)
		{
			uiText.text = orientation.ToString();
		}
	}
}
