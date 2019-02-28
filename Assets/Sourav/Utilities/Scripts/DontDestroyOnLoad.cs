using UnityEngine;

namespace Sourav.Utilities.Scripts
{
	public class DontDestroyOnLoad : MonoBehaviour 
	{

		// Use this for initialization
		void Start () 
		{
			DontDestroyOnLoad(this);
		}
	}
}
