using UnityEngine;

namespace Sourav.Utilities.Scripts.Utilities
{
	[RequireComponent(typeof(DontDestroyOnLoad))]
	public class SingletonObject : MonoBehaviour
	{
		public SingletonTypes type;
	}
}
