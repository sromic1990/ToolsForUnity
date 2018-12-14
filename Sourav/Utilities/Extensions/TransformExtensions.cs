using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sourav.Utilities.Extensions
{
	public static class TransformExtensions
	{
		public static void DeleteAllChildren(this Transform transform)
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				GameObject.Destroy(transform.GetChild(i).gameObject);
			}
		}
	}
}
