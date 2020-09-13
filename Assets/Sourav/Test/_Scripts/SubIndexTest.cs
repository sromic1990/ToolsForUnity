using System.Collections.Generic;
using Sourav.Extensions;
using UnityEngine;

namespace Sourav.Test._Scripts
{
	public class SubIndexTest : MonoBehaviour
	{

		[SerializeField] private string word;
		[SerializeField] private string subword;

		[SerializeField] private List<int> indices;

		[Sourav.Utilities.Scripts.Attributes.Button()]
		private void Test()
		{
			indices = word.GetSubWordIndices(subword);
		}
	}
}
