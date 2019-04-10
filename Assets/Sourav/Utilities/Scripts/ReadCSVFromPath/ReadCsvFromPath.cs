using System.Collections.Generic;
using UnityEngine;

namespace Sourav.Utilities.Scripts.ReadCSVFromPath
{
	public class ReadCsvFromPath : MonoBehaviour
	{
		[SerializeField] private string Path;
		[SerializeField] private List<Line> lines;
		
#if ODIN
		[Sirenix.OdinInspector.Button()]
#else
		[Sourav.Utilities.Scripts.Attributes.Button()]
#endif
		public void PopulateData()
		{
			string path = Application.dataPath + "/" + Path;
			
			ReadCsv rcsv = new ReadCsv();
			rcsv.ReadFromCsv(path);
			lines = rcsv.GetLines();
		}

		public List<Line> GetLines()
		{
			return lines;
		}
	}
}
