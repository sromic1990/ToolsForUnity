using Sourav.Utilities.Scripts.Attributes;
using UnityEngine;

namespace Sourav.Test._Scripts
{
	public class ReadonlyTest : MonoBehaviour 
	{
		[ReadOnly]
		public string field = "This should be readonly";

		// Use this for initialization
		void Start () 
		{
			field = "This should be readonly";
		}
	
		// Update is called once per frame
		void Update () {
		
		}
	}
}
